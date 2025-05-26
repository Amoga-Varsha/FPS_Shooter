using UnityEngine;

public class PlayerInputHandler : MonoBehaviour
{
    public PlayerAnimationController animationController;

    private Camera mainCamera;
    private WeaponInventory weaponInventory;
    private WeaponBase equippedWeapon;

    void Start()
    {
        mainCamera = Camera.main;
        weaponInventory = FindFirstObjectByType<WeaponInventory>();
    }

    void Update()
    {
        equippedWeapon = weaponInventory.GetEquippedWeapon();
        if (equippedWeapon == null) return;

        HandleMovement();
        HandleJump();
        HandleFire();
        HandleReload();
    }

    void HandleMovement()
    {
        animationController.SetRunning(Input.GetKey(KeyCode.W));
    }

    void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            animationController.PlayJump();
        }
    }

    void HandleFire()
    {
        if (Input.GetMouseButton(0))
        {
            equippedWeapon.TryFireWithAnimation(mainCamera, animationController);
        }
    }

    void HandleReload()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            animationController.PlayReload();
            equippedWeapon.StartReload();
        }
    }
}
