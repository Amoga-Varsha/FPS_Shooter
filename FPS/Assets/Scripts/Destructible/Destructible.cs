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

        // Play Sound
        if (explosionSound != null)
        {
            AudioSource.PlayClipAtPoint(explosionSound, transform.position);
        }

        // Play Particle Effect
        if (explosionEffectPrefab != null)
        {
            Instantiate(explosionEffectPrefab, transform.position, Quaternion.identity);
        }

        // Find nearby objects
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);

        foreach (Collider nearbyObject in colliders)
        {
            // Damage Enemy
            EnemyAI enemy = nearbyObject.GetComponent<EnemyAI>();
            if (enemy != null)
            {
                enemy.TakeDamage(explosionDamage);
            }

            // Damage Player
            PlayerHealth playerHealth = nearbyObject.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(explosionDamage);
            }

            // Trigger Other Destructibles
            Destructible destructible = nearbyObject.GetComponent<Destructible>();
            if (destructible != null && destructible != this) // prevent self-triggering
            {
                destructible.TakeDamage();
            }
        }

        // Destroy this object after a short delay to let particle/sound finish
        Destroy(gameObject, 0.1f);
    }

    private void OnDrawGizmosSelected()
    {
        // Optional: visualize explosion radius
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
