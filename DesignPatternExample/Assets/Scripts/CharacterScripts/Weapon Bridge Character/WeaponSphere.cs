using UnityEngine;

public class WeaponSphere : IWeapon {
    public WeaponSphere() { }
    protected override void WeaponSetting() {
        AtkValue = 10;
    }
}
