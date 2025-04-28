using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Weapon Data")]
public class WeaponData : ScriptableObject
{
    public string weaponName;
    public Avatar weaponAvatar;
    public RuntimeAnimatorController weaponOverrideController; // <- Important
}
