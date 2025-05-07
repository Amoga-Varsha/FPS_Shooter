using UnityEngine;

public class Destructible : MonoBehaviour
{
    [Header("Destructible Settings")]
    public float explosionRadius = 5f;
    public int explosionDamage = 50;
    public GameObject explosionEffectPrefab;
    public AudioClip explosionSound;

    private bool hasExploded = false;

    public void TakeDamage()
    {
        if (!hasExploded)
        {
            Explode();
        }
    }

    private void Explode()
    {
        hasExploded = true;

        
        if (explosionSound != null)
        {
            AudioSource.PlayClipAtPoint(explosionSound, transform.position);
        }

        
        if (explosionEffectPrefab != null)
        {
            Instantiate(explosionEffectPrefab, transform.position, Quaternion.identity);
        }

        
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);

        foreach (Collider nearbyObject in colliders)
        {
            
            EnemyAI enemy = nearbyObject.GetComponent<EnemyAI>();
            if (enemy != null)
            {
                enemy.TakeDamage(explosionDamage);
            }

            
            PlayerHealth playerHealth = nearbyObject.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(explosionDamage);
            }

            
            Destructible destructible = nearbyObject.GetComponent<Destructible>();
            if (destructible != null && destructible != this) 
            {
                destructible.TakeDamage();
            }
        }

        
        Destroy(gameObject, 0.1f);
    }

    private void OnDrawGizmosSelected()
    {
        
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
