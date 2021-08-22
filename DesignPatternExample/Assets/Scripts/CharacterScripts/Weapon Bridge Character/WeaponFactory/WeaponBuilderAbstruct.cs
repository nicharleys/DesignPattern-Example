using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBuilderParamAbstruct {

    public WeaponAbstract Weapon = null;
    public int AttrID;
    public string AssetName;
    public string IconSpriteName;
}
public abstract class WeaponBuilderAbstruct {
    public abstract void SetBuildParam(WeaponBuilderParamAbstruct theParam);
    public abstract void LoadAsset(int gameObjectID);
    public abstract void SetWeaponAttr();
    public abstract void AddWeaponSystem(GameFunction theGameFunction);
}
