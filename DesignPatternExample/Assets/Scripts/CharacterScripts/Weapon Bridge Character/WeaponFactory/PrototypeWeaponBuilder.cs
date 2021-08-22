using UnityEngine;

public class PrototypeWeaponBuilderParam : WeaponBuilderParamAbstruct {
    public PrototypeWeaponBuilderParam() { }
}
public class PrototypeWeaponBuilder : WeaponBuilderAbstruct {
    private PrototypeWeaponBuilderParam _builderParam = null;
    public override void SetBuildParam(WeaponBuilderParamAbstruct theParam) {
        _builderParam = theParam as PrototypeWeaponBuilderParam;
    }
    public override void LoadAsset(int gameObjectID) {
        GameObject prototypeGameObject = null;
        prototypeGameObject.gameObject.name = string.Format("PrototypeWeapon[{0}]", gameObjectID);
        _builderParam.Weapon.SetGameObject(prototypeGameObject);
    }
    public override void SetWeaponAttr() {
        throw new System.NotImplementedException();
    }
    public override void AddWeaponSystem(GameFunction theGameFunction) {
        throw new System.NotImplementedException();
    }
}
