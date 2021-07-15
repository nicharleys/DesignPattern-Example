using UnityEngine;

public class WeaponSphere : IWeapon {
    public WeaponSphere() { }

    protected override void RecoveryConfirmObj2() {
        ScatterObjects scatterObjects = GameObject.Find("ObjectPool").GetComponent<ScatterObjects>();
        Recovery(scatterObjects.SpherePool);
    }

    protected override void WeaponSetting() {
        AtkValue = 10;
    }
}
