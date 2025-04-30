using UnityEngine;

public class AKM : WeaponBase
{
    [Header("AKM Settings")]
    public int damage = 20;
    public float fireRange = 100f;

    protected override void Fire(Camera cam)
    {
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        if (Physics.Raycast(ray, out RaycastHit hit, fireRange))
        {
            //Debug.Log($"Hit object: {hit.collider.name}");

            // -- Commented out until you implement EnemyHealth! --
            if (hit.collider.CompareTag("Enemy"))
             {
                Debug.Log("Enemy hit!");

            //     EnemyHealth enemyHealth = hit.collider.GetComponent<EnemyHealth>();
            //     if (enemyHealth != null)
            //     {
            //         enemyHealth.TakeDamage(damage);
            //     }
            }
        }
    }
}
