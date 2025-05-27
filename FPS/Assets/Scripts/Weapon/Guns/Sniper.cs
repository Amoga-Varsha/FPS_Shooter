using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SniperRifle : WeaponBase
{
    [Header("Sniper Settings")]
    public int damage = 100;
    public float range = 300f;

    [Header("Scope Settings")]
    public float scopedFOV = 20f;
    private float normalFOV;
    private bool isScoped = false;

    private Camera playerCamera;

    public AudioSource audioSource;
    public AudioClip fireSound;

    [Header("Bullet Trail Settings")]
    public Transform muzzlePoint;
    public TrailRenderer bulletTrailPrefab;

    [Header("UI")]
    public RawImage scopeOverlay; 

    protected override void Start()
    {
        base.Start();
        playerCamera = Camera.main;
        normalFOV = playerCamera.fieldOfView;

        if (scopeOverlay != null)
            scopeOverlay.enabled = false; 
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            ToggleScope();
        }
    }

    private void ToggleScope()
    {
        isScoped = !isScoped;

        if (isScoped)
        {
            playerCamera.fieldOfView = scopedFOV;
            if (scopeOverlay != null)
                scopeOverlay.enabled = true;
        }
        else
        {
            playerCamera.fieldOfView = normalFOV;
            if (scopeOverlay != null)
                scopeOverlay.enabled = false;
        }
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

        Vector3 targetPoint;
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, range))
        {
            targetPoint = hit.point;

            if (hit.transform.CompareTag("Enemy"))
            {
                Debug.Log("Sniper Hit: " + hit.transform.name);
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

        while (Vector3.Distance(trail.transform.position, targetPoint) > 0.1f)
        {
            trail.transform.position = Vector3.MoveTowards(trail.transform.position, targetPoint, bulletSpeed * Time.deltaTime);
            yield return null;
        }

        trail.transform.position = targetPoint;
        Destroy(trail.gameObject, trail.time);
    }
}
