using System;
using System.Collections.Generic;
using UnityEngine;

public class WeaponInventory : MonoBehaviour
{
    public static Action<WeaponData> OnWeaponEquipped;

    [SerializeField] private List<WeaponData> collectedWeapons = new List<WeaponData>();
    private int currentIndex = -1;

    public void AddWeapon(WeaponData newWeapon)
    {
        if (!collectedWeapons.Contains(newWeapon))
        {
            collectedWeapons.Add(newWeapon);
            EquipWeapon(collectedWeapons.Count - 1); 
        }
    }

    public void EquipWeapon(int index)
    {
        if (index >= 0 && index < collectedWeapons.Count)
        {
            currentIndex = index;
            OnWeaponEquipped?.Invoke(collectedWeapons[index]); 
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) EquipWeapon(0);
        if (Input.GetKeyDown(KeyCode.Alpha2)) EquipWeapon(1);
        if (Input.GetKeyDown(KeyCode.Alpha3)) EquipWeapon(2);
    }
}
