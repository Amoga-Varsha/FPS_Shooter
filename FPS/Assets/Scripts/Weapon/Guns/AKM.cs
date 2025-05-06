using UnityEngine;

public class AKM : WeaponBase
{
    [Header("AKM Settings")]
    public int damage = 25;
    public float range = 100f;
    //public ParticleSystem muzzleFlash;
    //public GameObject impactEffect;

    protected override void Fire(Camera cam)
    {
        // if (muzzleFlash != null)
        //     muzzleFlash.Play();

        if (cam == null)
        {
            Debug.LogWarning("Camera not assigned for firing!");
            return;
        }

        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out RaycastHit hit, range))
        {
            

            // Check if the hit object is tagged as "Enemy"
            if (hit.transform.CompareTag("Enemy"))
            {
                Debug.Log("AKM Hit: " + hit.transform.name);
                // Apply damage if EnemyHealth script exists
                EnemyAI enemyHealth = hit.transform.GetComponent<EnemyAI>();
                if (enemyHealth != null)
                {
                    enemyHealth.TakeDamage(damage);
                }

                
                
            }

            // Instantiate impact effect at hit point
            // if (impactEffect != null)
            // {
            //     Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            // }
        }
    }
}
