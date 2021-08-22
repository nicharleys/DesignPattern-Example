using UnityEngine;

public class WeaponCapsule : WeaponAbstract {
    public WeaponCapsule() { }

    protected override void RecoveryConfirmObj() {
        ScatterObjects scatterObjects = GameObject.Find("ObjectPool").GetComponent<ScatterObjects>();
        Recovery(scatterObjects.CapsulePool);
    }

    protected override void WeaponSetting() {
        //AtkValue = 5;
    }

}
