using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Weapon Data")]
public class WeaponData : ScriptableObject
{
    public string weaponName;
    public GameObject weaponPrefab;    // The weapon prefab (with arms)
    //public GameObject weaponModel;     // The weapon model (if separate from arms)

    public AnimatorOverrideController weaponAnimatorOverride; // AnimatorOverrideController for the weapon
    public Avatar weaponAvatar; // Avatar specific to the weapon
}
