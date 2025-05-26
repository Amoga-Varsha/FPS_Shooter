using UnityEngine;
using System.Collections;

public class GrenadeBehaviour : MonoBehaviour
{
    public float explosionDelay = 5f;
    public float explosionRadius = 5f;
    public int explosionDamage = 50;
    public AudioClip explosionSound;
    public GameObject explosionParticlePrefab;

    private void Start()
    {
        StartCoroutine(ExplodeAfterDelay());
    }

    private IEnumerator ExplodeAfterDelay()
    {
        yield return new WaitForSeconds(explosionDelay);

        // Play sound
        if (explosionSound != null)
            AudioSource.PlayClipAtPoint(explosionSound, transform.position);

        // Play explosion effect
        if (explosionParticlePrefab != null)
            Instantiate(explosionParticlePrefab, transform.position, Quaternion.identity);

        // Deal damage
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider nearbyObject in colliders)
        {
            if (nearbyObject.TryGetComponent(out EnemyAI enemy))
                enemy.TakeDamage(explosionDamage);

            if (nearbyObject.TryGetComponent(out PlayerHealth playerHealth))
                playerHealth.TakeDamage(explosionDamage);
        }

        // Destroy grenade
        Destroy(gameObject);
    }
}
