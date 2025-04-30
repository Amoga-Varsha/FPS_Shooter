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
                //Debug.Log("Weapon Detected: " + hit.collider.name);

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
        // Add weapon to inventory
        WeaponInventory inventory = FindFirstObjectByType<WeaponInventory>();
        inventory.AddWeapon(holder.weaponData);

        // Instantiate the weaponPrefab as a new object
        GameObject weaponInstance = Instantiate(holder.weaponData.weaponPrefab);

        // Set the weapon instance under the Camera (or player's hand transform)
        weaponInstance.transform.SetParent(Camera.main.transform);
        weaponInstance.transform.localPosition = Vector3.zero;
        weaponInstance.transform.localRotation = Quaternion.identity;
        weaponInstance.transform.localScale = Vector3.one; // Optional: Reset scale

        // Activate it
        weaponInstance.SetActive(true);

        // Update the current weapon object
        currentWeaponObject = weaponInstance;

        // Get the WeaponBase component
        WeaponBase weaponBase = weaponInstance.GetComponent<WeaponBase>();
        if (weaponBase != null)
        {
            inventory.SetEquippedWeapon(weaponBase);
        }
        else
        {
            Debug.LogWarning("WeaponBase not found on picked up weapon: " + weaponObject.name);
        }

        // Destroy the pickup object
        Destroy(weaponObject);
    }
    else
    {
        Debug.LogWarning("WeaponHolder or WeaponData missing on: " + weaponObject.name);
    }
}






}
