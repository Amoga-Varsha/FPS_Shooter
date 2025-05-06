using UnityEngine;

public class Shotgun : WeaponBase
{
    [Header("Shotgun Settings")]
    public int damagePerPellet = 10;
    public int pelletCount = 8;
    public float spreadAngle = 5f;
    public float range = 30f;

    public AudioSource audioSource; 
    public AudioClip fireSound;

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

        for (int i = 0; i < pelletCount; i++)
        {
            Vector3 spreadDirection = cam.transform.forward;
            spreadDirection += new Vector3(
                Random.Range(-spreadAngle, spreadAngle),
                Random.Range(-spreadAngle, spreadAngle),
                0
            ) * 0.01f; 

            if (Physics.Raycast(cam.transform.position, spreadDirection.normalized, out RaycastHit hit, range))
            {
                if (hit.transform.CompareTag("Enemy"))
                {
                    Debug.Log("Shotgun Pellet Hit: " + hit.transform.name);
                    EnemyAI enemyHealth = hit.transform.GetComponent<EnemyAI>();
                    if (enemyHealth != null)
                    {
                        enemyHealth.TakeDamage(damagePerPellet);
                    }
                }
            }
        }
    }
}
