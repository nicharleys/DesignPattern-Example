using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AttrFactoryAbstruct 
{
    public abstract PlayerAttr GetPlayerAttr(int attrID);
    public abstract EnemyAttr GetEnemyAttr(int attrID);
    public abstract WeaponAttr GetWeaponAttr(int attrID);
}
