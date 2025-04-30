using UnityEngine;

public class AKM : WeaponBase
{
    protected override void Fire(Camera cam)
    {
        base.Fire(cam);
        // Add AKM-specific effects like muzzle flash, sound, etc.
        Debug.Log("AKM fired!");
    }
}
