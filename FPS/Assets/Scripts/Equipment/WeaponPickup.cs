using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    public float pickupRange = 2f;
    private GameObject currentWeaponObject; 

    void Update()
    {
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, pickupRange))
        {
            if (hit.collider.CompareTag("Weapon"))
            {
                if (Input.GetKeyDown(KeyCode.E)) 
                {
                    PickupWeapon(hit.collider.gameObject);
                }
            }
        }
    }

    void PickupWeapon(GameObject weaponObject)
    {
        WeaponHolder holder = weaponObject.GetComponent<WeaponHolder>();
        if (holder != null && holder.weaponData != null)
        {
            WeaponInventory inventory = FindFirstObjectByType<WeaponInventory>();
            if (inventory.GetWeaponCount() >= 3) 
            {
                Debug.Log("Inventory full! Cannot pick up weapon.");
                return; 
            }

            inventory.AddWeapon(holder.weaponData);

            GameObject weaponInstance = Instantiate(holder.weaponData.weaponPrefab);

            weaponInstance.transform.SetParent(Camera.main.transform);
            weaponInstance.transform.localPosition = Vector3.zero;
            weaponInstance.transform.localRotation = Quaternion.identity;
            weaponInstance.transform.localScale = Vector3.one; 

            weaponInstance.SetActive(true);

            currentWeaponObject = weaponInstance;

            WeaponBase weaponBase = weaponInstance.GetComponent<WeaponBase>();
            if (weaponBase != null)
            {
                inventory.SetEquippedWeapon(weaponBase);
            }
            else
            {
                Debug.LogWarning("WeaponBase not found on picked up weapon: " + weaponObject.name);
            }

            Destroy(weaponObject);
        }
        else
        {
            Debug.LogWarning("WeaponHolder or WeaponData missing on: " + weaponObject.name);
        }
    }
}
