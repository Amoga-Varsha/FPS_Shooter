using System;
using System.Collections;
using UnityEngine;

public abstract class WeaponBase : MonoBehaviour
{
    [Header("Weapon Settings")]
    public int maxAmmo = 30;
    public int ammoPerShot = 1;
    public float reloadTime = 2f;
    public float fireRate = 0.1f;

    protected int currentAmmo;
    protected bool isReloading = false;
    protected bool canFire = true;

    public static event Action<int, int> OnAmmoChanged; // current, max

    protected virtual void Start()
    {
        currentAmmo = maxAmmo;
        OnAmmoChanged?.Invoke(currentAmmo, maxAmmo);
    }

    public void TryFire(Camera cam)
    {
        if (!canFire || isReloading || currentAmmo <= 0)
            return;
        Debug.Log($"Ammo: {currentAmmo}/{maxAmmo}");

        StartCoroutine(FireCoroutine(cam));
    }

    private IEnumerator FireCoroutine(Camera cam)
    {
        canFire = false;

        Fire(cam);

        currentAmmo -= ammoPerShot;
        OnAmmoChanged?.Invoke(currentAmmo, maxAmmo);

        yield return new WaitForSeconds(fireRate);

        canFire = true;
    }

    protected abstract void Fire(Camera cam);

    public void StartReload()
    {
        if (!isReloading && currentAmmo < maxAmmo)
            StartCoroutine(ReloadCoroutine());
    }

    private IEnumerator ReloadCoroutine()
    {
        isReloading = true;

        yield return new WaitForSeconds(reloadTime);

        currentAmmo = maxAmmo;
        OnAmmoChanged?.Invoke(currentAmmo, maxAmmo);

        isReloading = false;
    }
}
