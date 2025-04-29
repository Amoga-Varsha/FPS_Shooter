using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    public float pickupRange = 2f;
    public Animator playerAnimator; 
    private string currentWeaponName = "Unarmed";
    
    private GameObject currentWeaponObject; 

    void Update()
    {
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, pickupRange))
        {
            if (hit.collider.CompareTag("Weapon"))
            {
                Debug.Log("Weapon Detected: " + hit.collider.name);

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
        if (currentWeaponObject != null)
        {
            Destroy(currentWeaponObject);
        }

        Vector3 spawnPosition = playerAnimator.transform.position + playerAnimator.transform.forward * -0.2f + playerAnimator.transform.up * -0.05f;
       
        currentWeaponObject = Instantiate(holder.weaponData.weaponPrefab, spawnPosition, playerAnimator.transform.rotation);
        
        currentWeaponObject.transform.SetParent(playerAnimator.transform);
        
        playerAnimator.avatar = holder.weaponData.weaponAvatar;
        
        playerAnimator.runtimeAnimatorController = holder.weaponData.weaponAnimatorOverride;
        
        currentWeaponName = holder.weaponData.weaponName;

        Debug.Log("Picked up weapon: " + currentWeaponName);

        Destroy(weaponObject);
    }
    else
    {
        Debug.LogWarning("No WeaponData assigned on this weapon!");
    }
}

}
