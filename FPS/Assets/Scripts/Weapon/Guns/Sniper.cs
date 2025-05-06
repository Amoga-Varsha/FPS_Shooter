using UnityEngine;

public class Sniper : WeaponBase
{
    [Header("Sniper Settings")]
    public int damage = 100;
    public float range = 300f;

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
                Debug.Log("Sniper Hit: " + hit.transform.name);
                EnemyAI enemyHealth = hit.transform.GetComponent<EnemyAI>();
                if (enemyHealth != null)
                {
                    enemyHealth.TakeDamage(damage);
                }
            }
        }
    }
}
