using UnityEngine;
using System.Collections;

public class Shotgun : WeaponBase
{
    [Header("Shotgun Settings")]
    public int damagePerPellet = 10;
    public int pelletCount = 8;
    public float spreadAngle = 5f;
    public float range = 30f;

    [Header("Audio")]
    public AudioSource audioSource; 
    public AudioClip fireSound;

    [Header("Effects")]
    public TrailRenderer trailPrefab;
    public Transform muzzlePoint;

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
                0f
            ) * 0.01f;

            Vector3 rayOrigin = cam.transform.position;
            if (Physics.Raycast(rayOrigin, spreadDirection.normalized, out RaycastHit hit, range))
            {
                if (hit.transform.CompareTag("Enemy"))
                {
                    Debug.Log("Shotgun Pellet Hit: " + hit.transform.name);
                    EnemyAI enemyHealth = hit.transform.GetComponent<EnemyAI>();
                    if (enemyHealth != null)
                    {
                        enemyHealth.TakeDamage(damagePerPellet);
                    }

                    Destructible destructible = hit.collider.GetComponent<Destructible>();
                    if (destructible != null)
                    {
                        destructible.TakeDamage();
                    }
                }

                if (trailPrefab != null && muzzlePoint != null)
                {
                    TrailRenderer trail = Instantiate(trailPrefab, muzzlePoint.position, Quaternion.identity);
                    StartCoroutine(AnimateTrail(trail, hit.point));
                }
            }
        }
    }

    private IEnumerator AnimateTrail(TrailRenderer trail, Vector3 targetPoint)
    {
        float bulletSpeed = 150f;
        Vector3 start = trail.transform.position;

        while (Vector3.Distance(trail.transform.position, targetPoint) > 0.1f)
        {
            trail.transform.position = Vector3.MoveTowards(trail.transform.position, targetPoint, bulletSpeed * Time.deltaTime);
            yield return null;
        }

        trail.transform.position = targetPoint;
        Destroy(trail.gameObject, trail.time);
    }
}
