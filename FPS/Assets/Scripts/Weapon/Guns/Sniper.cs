using UnityEngine;

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

    protected override void Start()
    {
        base.Start();
        playerCamera = Camera.main;
        normalFOV = playerCamera.fieldOfView;
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

        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out RaycastHit hit, range))
        {
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
        }
        else
        {
            playerCamera.fieldOfView = normalFOV;
        }
    }
    
}
