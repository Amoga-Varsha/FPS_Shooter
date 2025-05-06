using UnityEngine;

public class Melee : WeaponBase
{
    [Header("Melee Settings")]
    public int damage = 40;
    public float attackRange = 2f; // very close

    protected override void Fire(Camera cam)
    {
        if (cam == null)
        {
            Debug.LogWarning("Camera not assigned for firing!");
            return;
        }

        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out RaycastHit hit, attackRange))
        {
            if (hit.transform.CompareTag("Enemy"))
            {
                Debug.Log("Melee Hit: " + hit.transform.name);
                EnemyAI enemyHealth = hit.transform.GetComponent<EnemyAI>();
                if (enemyHealth != null)
                {
                    enemyHealth.TakeDamage(damage);
                }
            }
        }
    }
}
