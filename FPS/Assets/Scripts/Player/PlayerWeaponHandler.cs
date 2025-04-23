using UnityEngine;

public class PlayerWeaponHandler : MonoBehaviour
{
    public Animator animator;
    public WeaponData currentWeapon;

    void Start()
    {
        EquipWeapon(currentWeapon); 
    }

    public void EquipWeapon(WeaponData weapon)
    {
        currentWeapon = weapon;
        animator.avatar = weapon.weaponAvatar;
        animator.runtimeAnimatorController = weapon.overrideController;

        Debug.Log("Equipped: " + weapon.weaponName);
    }
}
