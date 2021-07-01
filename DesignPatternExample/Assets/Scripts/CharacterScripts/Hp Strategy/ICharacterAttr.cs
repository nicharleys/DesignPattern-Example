public abstract class ICharacterAttr
{
    protected float MaxHp = 0;
    protected float NowHp = 0;
    protected string AttrName = "";
    protected int CharacterLv = 0;
    protected float CirtRate = 0;
    protected float ExtraMaxHp;
    protected IAttrStrategy AttrStrategy = null;
    public float GetNowHp() {
        return NowHp;
    }
    public virtual float GetMaxHp() {
        return MaxHp;
    }
    public string GetAttrName() {
        return AttrName;
    }
    public void SetAttStrategy(IAttrStrategy theAttrStrategy) {
        AttrStrategy = theAttrStrategy;
    }
    public virtual void InitAttr() {
        AttrStrategy.Initialize(this);
    }
    public float GetAtkPlusValue() {
        return AttrStrategy.GetAtkPlusValue(this);
    }
    public void CalDmgValue(ICharacter attacker) {
        float atkValue = attacker.GetWeaponAtkValue();
        atkValue -= AttrStrategy.GetDmgDescValue(this);
        NowHp -= atkValue;
    }
}
