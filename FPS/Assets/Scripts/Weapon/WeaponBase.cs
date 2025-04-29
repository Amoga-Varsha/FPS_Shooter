using System.Collections;
using UnityEngine;

public abstract class WeaponBase : MonoBehaviour
{
    [Header("General Settings")]
    public float damage = 10f;
    public float fireRate = 0.2f;
    public float range = 100f;

    [Header("References")]
    public Transform firePoint; // The muzzle or raycast start point
    public LayerMask hitLayers;

    protected bool isFiring;

    public void TryFire()
    {
        if (!isFiring)
        {
            StartCoroutine(FireRoutine());
        }
    }

    protected virtual IEnumerator FireRoutine()
    {
        isFiring = true;
        Fire(); // Do the actual firing logic
        yield return new WaitForSeconds(fireRate);
        isFiring = false;
    }

    protected virtual void Fire()
{
    if (firePoint == null)
    {
        Debug.LogWarning("FirePoint is not assigned.");
        return;
    }

    Ray ray = new Ray(firePoint.position, firePoint.forward);
    Debug.DrawRay(firePoint.position, firePoint.forward * range, Color.red, 1f); // <--- Debug Line

    if (Physics.Raycast(ray, out RaycastHit hit, range, hitLayers))
    {
        Debug.Log($"Hit {hit.collider.name}, dealing {damage} damage");
        // Add damage logic here
    }
    else
    {
        Debug.Log("Missed.");
    }
}

}
