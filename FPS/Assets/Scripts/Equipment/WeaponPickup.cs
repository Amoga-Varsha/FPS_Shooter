using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    public float pickupRange = 2f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            TryPickupWeapon();
        }
    }

    void TryPickupWeapon()
    {
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, pickupRange) && hit.collider.CompareTag("Weapon"))
        {
            WeaponHolder holder = hit.collider.GetComponent<WeaponHolder>();
            if (holder != null && holder.weaponData != null)
            {
                WeaponInventory inventory = FindFirstObjectByType<WeaponInventory>();
                if (inventory == null)
                {
                    Debug.LogWarning("WeaponInventory not found!");
                    return;
                }

                if (inventory.GetWeaponCount() >= 3)
                {
                    Debug.Log("Inventory full! Cannot pick up weapon.");
                    return;
                }

                inventory.AddWeapon(holder.weaponData);

                Destroy(hit.collider.gameObject); 
            }
            else
            {
                Debug.LogWarning("WeaponHolder or WeaponData missing on: " + hit.collider.name);
            }
        }
    }
}
