using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    public float pickupRange = 2f;
    public Animator playerAnimator; // Reference to player's Animator
    private string currentWeaponName = "Unarmed";

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
            // ✅ Set Avatar
            playerAnimator.avatar = holder.weaponData.weaponAvatar;

            // ✅ Set RuntimeAnimatorController (Override Controller)
            playerAnimator.runtimeAnimatorController = holder.weaponData.weaponOverrideController;

            // Update current weapon name
            currentWeaponName = holder.weaponData.weaponName;

            Debug.Log("Picked up weapon: " + currentWeaponName);

            // Destroy the weapon in the world
            Destroy(weaponObject);
        }
        else
        {
            Debug.LogWarning("No WeaponData assigned on this weapon!");
        }
    }
}
