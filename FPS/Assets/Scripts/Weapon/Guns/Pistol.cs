using UnityEngine;

public class Pistol : WeaponBase
{
    [Header("Pistol Settings")]
    public int damage = 15;
    public float range = 50f;

    protected override void Fire(Camera cam)
    {
        if (cam == null)
        {
            Debug.LogWarning("Camera not assigned for firing!");
            return;
        }

        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out RaycastHit hit, range))
        {
            if (hit.transform.CompareTag("Enemy"))
            {
                Debug.Log("Pistol Hit: " + hit.transform.name);
                EnemyAI enemyHealth = hit.transform.GetComponent<EnemyAI>();
                if (enemyHealth != null)
                {
                    enemyHealth.TakeDamage(damage);
                }
            }
        }
    }
}
