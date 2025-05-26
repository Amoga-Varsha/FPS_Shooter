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
    public GameObject explosionParticlePrefab;

    [Header("Explosion Damage Settings")]
    public float explosionRadius = 5f;
    public int explosionDamage = 50;        
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

    GameObject grenade = Instantiate(grenadePrefab, cam.transform.position + cam.transform.forward, Quaternion.identity);

    Rigidbody rb = grenade.GetComponent<Rigidbody>();
    if (rb != null)
    {
        rb.AddForce(cam.transform.forward * throwForce, ForceMode.VelocityChange);
    }

    // Unarm AFTER spawning and throwing the grenade
    EquipUnarmed();

    isThrowing = false;
}


    private void EquipUnarmed()
    {
        WeaponInventory inventory = FindFirstObjectByType<WeaponInventory>();
        if (inventory != null)
        {
            
            WeaponData myData = GetComponent<WeaponHolder>()?.weaponData;

            if (myData != null)
            {
                inventory.RemoveWeapon(myData);
            }

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
