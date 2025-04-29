using System.Collections;
using UnityEngine;

public abstract class WeaponBase : MonoBehaviour
{
    [Header("General Settings")]
    public float damage = 10f;
    public float fireRate = 0.2f;
    public float range = 100f;

    [Header("References")]
    public Transform firePoint; // Where the raycast starts (e.g., barrel)
    public LayerMask hitLayers;

    protected bool isFiring;

    // Public method to trigger fire (can be called from input)
    public void TryFire()
    {
        if (!isFiring)
        {
            StartCoroutine(FireRoutine());
        }
    }

    // Base coroutine handles fire timing
    protected virtual IEnumerator FireRoutine()
    {
        isFiring = true;

        Fire(); // Call virtual method

        yield return new WaitForSeconds(fireRate);
        isFiring = false;
    }

    // Virtual fire logic to override
    protected virtual void Fire()
    {
        Ray ray = new Ray(firePoint.position, firePoint.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, range, hitLayers))
        {
            Debug.Log($"Hit {hit.collider.name}, dealing {damage} damage");
            // Optional: Damage logic here
        }
        else
        {
            Debug.Log("Missed.");
        }
    }
}
