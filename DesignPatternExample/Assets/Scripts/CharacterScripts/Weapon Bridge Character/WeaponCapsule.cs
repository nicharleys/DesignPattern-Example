using UnityEngine;

public class WeaponCapsule : IWeapon {
    public WeaponCapsule() { }
    protected override void WeaponSetting() {
        AtkValue = 5;
    }

}
