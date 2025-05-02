using System.Collections;
using UnityEngine;
using System; // for Action (events)

public abstract class WeaponBase : MonoBehaviour
{
    [Header("Weapon Settings")]
    public float fireRate = 0.1f;   // Seconds between each shot
    public float reloadTime = 2f;   // Seconds to reload
    public int maxAmmoInMag = 30;   // Max bullets in magazine
    public int totalAmmo = 150;     // Total reserve ammo
    public int ammoPerShot = 1;     // Ammo used per shot

    protected int currentAmmoInMag;
    protected bool canFire = true;
    protected bool isReloading = false;

    // Event to notify when ammo changes (for UI)
    //public event Action<int, int> OnAmmoChanged;

    protected virtual void Start()
    {
        currentAmmoInMag = maxAmmoInMag;
        // Fire event to initialize UI (commented if UI not ready)
        // OnAmmoChanged?.Invoke(currentAmmoInMag, totalAmmo);

        Debug.Log($"[WeaponBase] Ammo Initialized: {currentAmmoInMag}/{totalAmmo}");
    }

    public virtual void TryFire(Camera cam)
    {
        if (!canFire || isReloading || currentAmmoInMag <= 0)
        {
            if (currentAmmoInMag <= 0)
                Debug.Log("[WeaponBase] Can't fire: No Ammo! Reload needed.");
            return;
        }

        StartCoroutine(FireCoroutine(cam));
    }

    private IEnumerator FireCoroutine(Camera cam)
    {
        canFire = false;

        Fire(cam);

        currentAmmoInMag -= ammoPerShot;

        Debug.Log($"[WeaponBase] Fired! Ammo Left: {currentAmmoInMag}/{totalAmmo}");

        // Notify UI about ammo change
        // OnAmmoChanged?.Invoke(currentAmmoInMag, totalAmmo);

        yield return new WaitForSeconds(fireRate);

        canFire = true;
    }

    public virtual void StartReload()
    {
        if (isReloading || currentAmmoInMag == maxAmmoInMag || totalAmmo <= 0)
        {
            Debug.Log("[WeaponBase] Cannot reload: Either already full or no reserve ammo!");
            return;
        }

        StartCoroutine(ReloadCoroutine());
    }

    private IEnumerator ReloadCoroutine()
{
    isReloading = true;

    Debug.Log("[WeaponBase] Reloading...");

    // Optional: Play reload animation trigger here

    yield return new WaitForSeconds(reloadTime);

    int neededAmmo = maxAmmoInMag - currentAmmoInMag;
    int ammoToReload = Mathf.Min(neededAmmo, totalAmmo);

    if (ammoToReload > 0)
    {
        currentAmmoInMag += ammoToReload;
        totalAmmo -= ammoToReload;
    }
    else
    {
        Debug.Log("[WeaponBase] No reserve ammo left to reload!");
    }

    Debug.Log($"[WeaponBase] Reload Complete. Ammo: {currentAmmoInMag}/{totalAmmo}");

    // Fire the event AFTER setting the ammo
    // OnAmmoChanged?.Invoke(currentAmmoInMag, totalAmmo);

    isReloading = false;
}

    // Abstract method for different guns to implement their own firing logic
    protected abstract void Fire(Camera cam);
}
