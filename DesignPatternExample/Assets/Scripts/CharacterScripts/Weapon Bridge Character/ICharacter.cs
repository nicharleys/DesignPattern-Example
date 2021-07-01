using UnityEngine;

public abstract class ICharacter : MonoBehaviour {
    protected ICharacterAttr Attribute = null;
    protected string AttrName = null;

    private IWeapon _weapon = null;

    public ICharacterAttr GetAttribute() {
        return Attribute;
    }
    public void SetWeapon(IWeapon theWeapon) {
        _weapon = theWeapon;
        _weapon.SetOwner(this);
    }
    public IWeapon GetWeapon() {
        return _weapon;
    }
    public virtual void SetCharacterAttr(ICharacterAttr characterAttr) {
        Attribute = characterAttr;
        Attribute.InitAttr();
        AttrName = Attribute.GetAttrName();
    }
    protected void SetWeaponAtkPlusValue(int value) {
        _weapon.SetAtkPlusValue(value);
    }
    public int GetWeaponAtkValue() {
        return _weapon.GetAtkValue();
    }
    public virtual void SearchWeapons() { }
    public virtual void ThrowAttack() { }
    public abstract void Attack();
    public void UnderAttack(ICharacter theAttacker) {
        Attribute.CalDmgValue(theAttacker);
    }
}
