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
        FindFirstObjectByType<WeaponInventory>().AddWeapon(holder.weaponData);
        Destroy(weaponObject);
    }
}


}
