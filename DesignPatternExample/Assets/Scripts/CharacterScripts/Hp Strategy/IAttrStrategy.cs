using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IAttrStrategy
{
    public abstract void Initialize(ICharacterAttr theCharacterAttr);
    public abstract float GetAtkPlusValue(ICharacterAttr theCharacterAttr);
    public abstract float GetDmgDescValue(ICharacterAttr theCharacterAttr);
}
