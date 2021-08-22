using DesignPatternExample.Character.CharacterSetting;

public abstract class CharacterAttrAbstract {
    protected CharacterBaseAttr BaseAttr = null;
    protected float NowHp = 0;
    protected AttrStrategyAbstract AttrStrategy = null;
    protected void SetBaseAttr(CharacterBaseAttr theBaseAttr) {
        BaseAttr = theBaseAttr;
    }
    public CharacterBaseAttr GetBaseAttr() {
        return BaseAttr;
    }
    public void SetAttStrategy(AttrStrategyAbstract theAttrStrategy) {
        AttrStrategy = theAttrStrategy;
    }
    public AttrStrategyAbstract GetAttrStrategy() {
        return AttrStrategy;
    }
    public virtual float GetMaxHP() {
        return BaseAttr.GetMaxHP();
    }
    public virtual string GetAttrName() {
        return BaseAttr.GetAttrName();
    }
    public float GetNowHp() {
        return NowHp;
    }
    public void FullNowHP() {
        NowHp = GetMaxHP();
    }
    public virtual void InitAttr() {
        AttrStrategy.Initialize(this);
        FullNowHP();
    }
    public float GetAtkPlusValue() {
        return AttrStrategy.GetAtkPlusValue(this);
    }
    public void CalDmgValue(CharacterAbstract attacker) {
        float atkValue = attacker.GetWeaponAtkValue();
        atkValue -= AttrStrategy.GetDmgDescValue(this);
        NowHp -= atkValue;
    }
}
