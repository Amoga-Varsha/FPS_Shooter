using System.Collections;
using UnityEngine;

public abstract class WeaponBase : MonoBehaviour
{
    [Header("General Settings")]
    public float damage = 10f;
    public float fireRate = 0.2f;
    public float range = 100f;

    [Header("References")]
    public LayerMask hitLayers;

    protected bool isFiring;

    public void TryFire(Camera cam)
    {
        if (!isFiring)
        {
            StartCoroutine(FireRoutine(cam));
        }
    }

    protected virtual IEnumerator FireRoutine(Camera cam)
    {
        isFiring = true;
        Fire(cam);
        yield return new WaitForSeconds(fireRate);
        isFiring = false;
    }

    protected virtual void Fire(Camera cam)
    {
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        
        if (Physics.Raycast(ray, out RaycastHit hit, range, hitLayers))
        {
            Debug.Log($"Hit {hit.collider.name}, dealing {damage} damage");
            // Add damage logic here
        }
        else
        {
            Debug.Log("Missed.");
        }

        // Debug ray for visualization
        Debug.DrawRay(ray.origin, ray.direction * range, Color.red, 0.1f);
    }
}
