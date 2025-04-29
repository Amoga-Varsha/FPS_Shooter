using UnityEngine;

public class PlayerInputHandler : MonoBehaviour
{
    public PlayerAnimationController animationController;
    public WeaponBase currentWeapon;

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
            currentWeapon?.TryFire();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            animationController.PlayReload();
        }
    }

    public void SetCurrentWeapon(WeaponBase newWeapon)
    {
        currentWeapon = newWeapon;
    }
}
