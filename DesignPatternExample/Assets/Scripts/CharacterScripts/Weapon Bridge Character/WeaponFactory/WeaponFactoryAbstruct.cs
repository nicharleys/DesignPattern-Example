using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponFactoryAbstruct : MonoBehaviour
{
    public abstract WeaponAbstract CreatePrototyWeapon(WeaponType weaponType);
}
