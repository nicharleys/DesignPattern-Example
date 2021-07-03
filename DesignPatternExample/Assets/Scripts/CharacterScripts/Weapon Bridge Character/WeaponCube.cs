using UnityEngine;

public class WeaponCube : IWeapon {
    public WeaponCube() { }
    protected override void WeaponSetting() {
        AtkValue = 10;
    }
}
