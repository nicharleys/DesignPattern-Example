using UnityEngine;

public abstract class ICharacter : MonoBehaviour {
    private IWeapon _weapon = null;
    public float CharacterHp { get; set; }

    public void SetWeapon(IWeapon theWeapon) {
        _weapon = theWeapon;
        _weapon.SetOwner(this);
    }
    public IWeapon GetWeapon() {
        return _weapon;
    }
    protected void SetWeaponAtkPlusValue(int value) {
        //m_Weapon.SetAtkPlusValue(value);
    }
    public int GetAtkValue() {
        //return m_Weapon.GetAtkValue();
        return 1;
    }
    //public abstract void Initizal();
    //public abstract void Update();
    //public abstract void Release();
    public virtual void SearchWeapons() { }
    public virtual void ThrowAttack() { }
    public abstract void Attack();
    public abstract void UnderAttack(ICharacter theAttacker);
    public abstract void Dead();
}
