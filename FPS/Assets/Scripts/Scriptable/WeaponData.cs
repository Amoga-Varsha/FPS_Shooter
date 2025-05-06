using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Weapon Data")]
public class WeaponData : ScriptableObject
{
    public string weaponName;
    public GameObject weaponPrefab;   

    public AnimatorOverrideController weaponAnimatorOverride; 
    public Avatar weaponAvatar; 

    [Header("UI")]
    public Sprite weaponIcon;
}
