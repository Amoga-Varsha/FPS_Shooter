using System.Collections;
using UnityEngine;
using System; 

public abstract class WeaponBase : MonoBehaviour
{
    [Header("Weapon Settings")]
    public float fireRate = 0.1f;   
    public float reloadTime = 2f;   
    public int maxAmmoInMag = 30;   
    public int totalAmmo = 150;     
    public int ammoPerShot = 1;     

    protected int currentAmmoInMag;
    protected bool canFire = true;
    protected bool isReloading = false;

    public static event Action<int, int> OnAmmoChanged; 

    protected void NotifyAmmoChanged()
    {
        OnAmmoChanged?.Invoke(currentAmmoInMag, totalAmmo);
    }


    protected virtual void Start()
    {
        currentAmmoInMag = maxAmmoInMag;
        NotifyAmmoChanged();

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

        NotifyAmmoChanged();

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

    NotifyAmmoChanged();

    isReloading = false;
}
    protected abstract void Fire(Camera cam);
}
