using System;
using System.Collections.Generic;
using UnityEngine;

public class WeaponInventory : MonoBehaviour
{
    public static Action<WeaponData> OnWeaponEquipped;

    [SerializeField] private List<WeaponData> collectedWeapons = new List<WeaponData>();
    [SerializeField] private WeaponData startingWeapon; // <-- ADD THIS

    private int currentIndex = -1;
    private WeaponBase equippedWeapon;

    void Start()
    {
        if (startingWeapon != null)
        {
            collectedWeapons.Add(startingWeapon);
            EquipWeapon(0); // Equip starting weapon at start
        }
    }

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

    public void SetEquippedWeapon(WeaponBase weapon)
    {
        equippedWeapon = weapon;
    }

    public WeaponBase GetEquippedWeapon()
    {
        return equippedWeapon;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) EquipWeapon(0);
        if (Input.GetKeyDown(KeyCode.Alpha2)) EquipWeapon(1);
        if (Input.GetKeyDown(KeyCode.Alpha3)) EquipWeapon(2);
    }
}
