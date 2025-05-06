using UnityEngine;

public class Melee : WeaponBase
{
    [Header("Melee Settings")]
    public int damage = 40;
    public float attackRange = 2f;

    public AudioSource audioSource; 
    public AudioClip fireSound;

    protected override void Start()
    {
        base.Start();
        isMeleeWeapon = true; 
    }

    protected override void Fire(Camera cam)
    {
        
        if (audioSource != null && fireSound != null)
        {
            audioSource.PlayOneShot(fireSound);
        }

        
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
