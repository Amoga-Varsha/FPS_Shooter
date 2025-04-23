using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    public GameObject weaponPrefab;

    private bool isPlayerInRange = false;
    private Transform playerWeaponHolder;

    void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            PickupWeapon();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
            playerWeaponHolder = other.transform.Find("WeaponHolder");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
        }
    }

    void PickupWeapon()
    {
        if (playerWeaponHolder == null) return;

        GameObject pickedWeapon = Instantiate(weaponPrefab, playerWeaponHolder.position, playerWeaponHolder.rotation);
        pickedWeapon.transform.SetParent(playerWeaponHolder);
        Destroy(gameObject); 
    }
}
