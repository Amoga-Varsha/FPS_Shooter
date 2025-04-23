using UnityEngine;

[CreateAssetMenu(fileName = "NewWeapon", menuName = "Weapons/WeaponData")]
public class WeaponData : ScriptableObject
{
    public string weaponName;
    public Avatar weaponAvatar;
    public AnimatorOverrideController overrideController;
    //public Sprite weaponIcon;
    public int damage;
    public float fireRate;
    // Add other things like ammo, etc.
}
