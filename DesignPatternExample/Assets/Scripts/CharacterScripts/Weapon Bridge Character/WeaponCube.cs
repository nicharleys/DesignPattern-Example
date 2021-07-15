using UnityEngine;

public class WeaponCube : IWeapon {
    public WeaponCube() { }

    protected override void RecoveryConfirmObj2() {
        ScatterObjects scatterObjects = GameObject.Find("ObjectPool").GetComponent<ScatterObjects>();
        Recovery(scatterObjects.CubePool);
    }

    protected override void WeaponSetting() {
        AtkValue = 10;
    }
}
