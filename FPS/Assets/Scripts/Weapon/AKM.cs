using UnityEngine;

public class AKM : WeaponBase
{
    protected override void Fire()
    {
        base.Fire();
        // Add unique effects for rifle here: muzzle flash, sound, etc.
        Debug.Log("Rifle fired!");
    }
}
