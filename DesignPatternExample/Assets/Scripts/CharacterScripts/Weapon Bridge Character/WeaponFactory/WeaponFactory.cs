using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponFactory : WeaponFactoryAbstruct
{
    private WeaponBuilderSystem _builderDirector = new WeaponBuilderSystem(GameFunction.Instance);
    public override WeaponAbstract CreatePrototyWeapon(WeaponType weaponType) {
        PrototypeWeaponBuilderParam weaponParam = new PrototypeWeaponBuilderParam();
        switch(weaponType) {
            case WeaponType.Cube:
                weaponParam.Weapon = new WeaponCube();
                break;
            case WeaponType.Sphere:
                weaponParam.Weapon = new WeaponSphere();
                break;
            case WeaponType.Capsule:
                weaponParam.Weapon = new WeaponCapsule();
                break;
            default:
                Debug.LogWarning("CreateWeapon:無法建立[" + weaponType + "]");
                return null;
        }
        PrototypeWeaponBuilder weaponBuilder = new PrototypeWeaponBuilder();
        weaponBuilder.SetBuildParam(weaponParam);

        _builderDirector.Construct(weaponBuilder);
        return weaponParam.Weapon;
    }
}
