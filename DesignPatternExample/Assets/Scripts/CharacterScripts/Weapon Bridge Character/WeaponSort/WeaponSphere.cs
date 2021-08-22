using UnityEngine;

public class WeaponSphere : WeaponAbstract {
    public WeaponSphere() { }

    protected override void RecoveryConfirmObj() {
        ScatterObjects scatterObjects = GameObject.Find("ObjectPool").GetComponent<ScatterObjects>();
        Recovery(scatterObjects.SpherePool);
    }

    protected override void WeaponSetting() {
        //AtkValue = 10;
    }
}
