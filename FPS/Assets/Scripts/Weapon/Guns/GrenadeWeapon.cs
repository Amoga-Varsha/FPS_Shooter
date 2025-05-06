using UnityEngine;
using System.Collections;
using System;

public class GrenadeWeapon : WeaponBase
{
    [Header("Grenade Settings")]
    public GameObject grenadePrefab; 
    public float throwForce = 10f; 
    public float explosionDelay = 5f; 
    public float throwDelay = 2f; 
    public AudioClip explosionSound; 

    private bool isThrowing = false;

    protected override void Start()
    {
        base.Start();
    }

    public override void TryFire(Camera cam)
    {
        if (isReloading || isThrowing)
            return;

        base.TryFire(cam);
    }

    protected override void Fire(Camera cam)
    {
    
        StartCoroutine(DelayedThrow(cam));
    }

    private IEnumerator DelayedThrow(Camera cam)
    {
        isThrowing = true;

        yield return new WaitForSeconds(throwDelay); 

        if (grenadePrefab == null)
        {
            Debug.LogWarning("Grenade prefab is not assigned!");
            isThrowing = false;
            yield break;
        }

        EquipUnarmed();
        GameObject grenade = Instantiate(grenadePrefab, cam.transform.position + cam.transform.forward, Quaternion.identity);

        Rigidbody rb = grenade.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.AddForce(cam.transform.forward * throwForce, ForceMode.VelocityChange);
        }

        StartCoroutine(HandleExplosion(grenade));

        isThrowing = false;
    }

    private IEnumerator HandleExplosion(GameObject grenade)
    {
        yield return new WaitForSeconds(explosionDelay);

        if (grenade != null)
        {
            if (explosionSound != null)
            {
                AudioSource.PlayClipAtPoint(explosionSound, grenade.transform.position);
            }
            Destroy(grenade);
        }
    }

    private void EquipUnarmed()
    {
        WeaponInventory inventory = FindFirstObjectByType<WeaponInventory>();
        if (inventory != null)
        {
            inventory.EquipWeapon(0); 
        }
        else
        {
            Debug.LogWarning("WeaponInventory not found! Cannot switch to Unarmed.");
        }
    }

    public override void StartReload()
    {
        return;
    }
}
