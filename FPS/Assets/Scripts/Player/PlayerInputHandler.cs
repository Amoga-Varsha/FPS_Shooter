using UnityEngine;

public class PlayerInputHandler : MonoBehaviour
{
    public PlayerAnimationController animationController;
    private Camera mainCamera;
    private WeaponInventory weaponInventory; // Access inventory

    void Start()
    {
        mainCamera = Camera.main;
        weaponInventory = FindFirstObjectByType<WeaponInventory>(); // Find inventory once
    }

    void Update()
    {
        animationController.SetRunning(Input.GetKey(KeyCode.W));

        if (Input.GetKeyDown(KeyCode.Space))
        {
            animationController.PlayJump();
        }

        if (Input.GetMouseButton(0))
        {
            animationController.PlayShoot();
            WeaponBase equippedWeapon = weaponInventory.GetEquippedWeapon(); // Get weapon dynamically
            equippedWeapon?.TryFire(mainCamera);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            animationController.PlayReload();
            WeaponBase equippedWeapon = weaponInventory.GetEquippedWeapon();
            equippedWeapon?.StartReload();
        }
    }
}
