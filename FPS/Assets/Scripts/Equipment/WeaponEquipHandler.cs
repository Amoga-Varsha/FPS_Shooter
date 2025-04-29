using UnityEngine;

public class WeaponEquipHandler : MonoBehaviour
{
    public Animator playerAnimator;
    public Transform weaponParent; 
    private GameObject currentWeaponModel;

    void OnEnable()
    {
        WeaponInventory.OnWeaponEquipped += HandleEquip;
    }

    void OnDisable()
    {
        WeaponInventory.OnWeaponEquipped -= HandleEquip;
    }

    void HandleEquip(WeaponData data)
    {
        if (currentWeaponModel != null)
            Destroy(currentWeaponModel);

        currentWeaponModel = Instantiate(data.weaponPrefab, weaponParent.position + weaponParent.forward * -0.2f + weaponParent.up*-0.05f, weaponParent.rotation, weaponParent);

        playerAnimator.avatar = data.weaponAvatar;
        playerAnimator.runtimeAnimatorController = data.weaponAnimatorOverride;
    }
}
