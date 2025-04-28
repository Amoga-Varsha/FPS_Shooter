using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    public float pickupRange = 2f;
    public Animator playerAnimator; // Reference to player's Animator
    private string currentWeaponName = "Unarmed";
    
    private GameObject currentWeaponObject; // Reference to the current weapon model (arms + weapon)
    public GameObject transformlcation;

    void Update()
    {
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, pickupRange))
        {
            if (hit.collider.CompareTag("Weapon"))
            {
                Debug.Log("Weapon Detected: " + hit.collider.name);

                if (Input.GetKeyDown(KeyCode.E)) // Pick up the weapon when 'E' is pressed
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
        // If there's an existing weapon equipped, destroy it
        if (currentWeaponObject != null)
        {
            Destroy(currentWeaponObject);
        }

        // Calculate the spawn position with an offset (slightly behind and lower)
        Vector3 spawnPosition = playerAnimator.transform.position + playerAnimator.transform.forward * -0.2f + playerAnimator.transform.up * -0.05f;
        
        // Instantiate the new weapon prefab (which includes arms and gun)
        currentWeaponObject = Instantiate(holder.weaponData.weaponPrefab, spawnPosition, playerAnimator.transform.rotation);
        
        // Set the new weapon's parent to the player's Animator
        currentWeaponObject.transform.SetParent(playerAnimator.transform);

        // Set the new avatar for the weapon (if necessary)
        playerAnimator.avatar = holder.weaponData.weaponAvatar;

        // Set the new RuntimeAnimatorController to handle animations for this weapon
        playerAnimator.runtimeAnimatorController = holder.weaponData.weaponAnimatorOverride;

        // Update the current weapon name
        currentWeaponName = holder.weaponData.weaponName;

        Debug.Log("Picked up weapon: " + currentWeaponName);

        // Destroy the weapon pickup object in the world
        Destroy(weaponObject);
    }
    else
    {
        Debug.LogWarning("No WeaponData assigned on this weapon!");
    }
}

}
