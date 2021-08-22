using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBuilderSystem : GameSystemAbstract
{
    private int _gameObjectID = 0;
    private GameFunction _gameFunction;
    public WeaponBuilderSystem(GameFunction theFunction) : base(theFunction) {
        _gameFunction = theFunction;
    }
    public void Construct(WeaponBuilderAbstruct theBuilder) {
        theBuilder.LoadAsset(++_gameObjectID);
        theBuilder.SetWeaponAttr();

        theBuilder.AddWeaponSystem(_gameFunction);
    }
}
