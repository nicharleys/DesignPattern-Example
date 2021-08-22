using UnityEngine;

public class WeaponCube : WeaponAbstract {
    public WeaponCube() { }

    protected override void RecoveryConfirmObj() {
        ScatterObjects scatterObjects = GameObject.Find("ObjectPool").GetComponent<ScatterObjects>();
        Recovery(scatterObjects.CubePool);
    }

    protected override void WeaponSetting() {
        //AtkValue = 10;
    }
}
