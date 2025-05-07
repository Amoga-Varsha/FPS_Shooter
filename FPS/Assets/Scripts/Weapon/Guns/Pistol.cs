using UnityEngine;

public class Pistol : WeaponBase
{
    [Header("Pistol Settings")]
    public int damage = 15;
    public float range = 50f;

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

                Destructible destructible = hit.collider.GetComponent<Destructible>();
                if (destructible != null)
                {
                    destructible.TakeDamage();
                }
            }
        }
    }
}
