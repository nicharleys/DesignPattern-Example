using UnityEngine;

public class WeaponCapsule : IWeapon {
    public WeaponCapsule() { }

    protected override void RecoveryConfirmObj2() {
        ScatterObjects scatterObjects = GameObject.Find("ObjectPool").GetComponent<ScatterObjects>();
        Recovery(scatterObjects.CapsulePool);
    }

    protected override void WeaponSetting() {
        AtkValue = 5;
    }

}
