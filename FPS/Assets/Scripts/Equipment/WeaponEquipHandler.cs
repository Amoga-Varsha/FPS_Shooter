using System.Collections.Generic;
using UnityEngine;

public class WeaponEquipHandler : MonoBehaviour
{
    public Animator playerAnimator;
    public Transform weaponParent;
    private GameObject currentWeaponModel;

    private Dictionary<WeaponData, GameObject> instantiatedWeapons = new Dictionary<WeaponData, GameObject>();

    private readonly Vector3 weaponOffset = new Vector3(0f, -0.05f, -0.2f);

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
        {
            currentWeaponModel.SetActive(false);
        }

        if (instantiatedWeapons.TryGetValue(data, out GameObject weaponModel))
        {
            weaponModel.SetActive(true);
            currentWeaponModel = weaponModel;
        }
        else
        {
            currentWeaponModel = Instantiate(data.weaponPrefab, weaponParent);
            currentWeaponModel.transform.localPosition = weaponOffset;
            currentWeaponModel.transform.localRotation = Quaternion.identity;
            currentWeaponModel.transform.localScale = Vector3.one;
            currentWeaponModel.SetActive(true);

            instantiatedWeapons[data] = currentWeaponModel;
        }

        WeaponBase weaponBase = currentWeaponModel.GetComponent<WeaponBase>();
        if (weaponBase != null)
        {
            WeaponInventory inventory = FindFirstObjectByType<WeaponInventory>();
            if (inventory != null)
            {
                inventory.SetEquippedWeapon(weaponBase);
            }
        }
        else
        {
            Debug.LogWarning("WeaponBase component missing on equipped weapon.");
        }

        playerAnimator.avatar = data.weaponAvatar;
        playerAnimator.runtimeAnimatorController = data.weaponAnimatorOverride;
    }
}
