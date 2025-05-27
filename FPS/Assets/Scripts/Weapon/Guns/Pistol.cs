using UnityEngine;
using System.Collections;

public class Pistol : WeaponBase
{
    [Header("Pistol Settings")]
    public int damage = 15;
    public float range = 50f;

    public AudioSource audioSource;
    public AudioClip fireSound;

    [Header("Bullet Trail Settings")]
    public Transform muzzlePoint;
    public TrailRenderer bulletTrailPrefab;

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

        Vector3 targetPoint;
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, range))
        {
            targetPoint = hit.point;

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
        else
        {
            targetPoint = ray.origin + ray.direction * range;
        }
        if (bulletTrailPrefab != null && muzzlePoint != null)
        {
            TrailRenderer trail = Instantiate(bulletTrailPrefab, muzzlePoint.position, Quaternion.identity);
            StartCoroutine(AnimateTrail(trail, targetPoint));
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
