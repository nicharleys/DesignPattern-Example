using UnityEngine;

public abstract class ICharacter : MonoBehaviour {
    private IWeapon m_Weapon = null;
    public float CharacterHp { get; set; }

    public void SetWeapon(IWeapon weapon) {
        m_Weapon = weapon;
        m_Weapon.SetOwner(this);
    }
    public IWeapon GetWeapon() {
        return m_Weapon;
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
