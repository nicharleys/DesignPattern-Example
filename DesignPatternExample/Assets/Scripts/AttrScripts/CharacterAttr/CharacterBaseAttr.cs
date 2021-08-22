public class CharacterBaseAttr
{
    private int _maxHP;
    private string _attrName;

    public CharacterBaseAttr(int maxHP, string attrName) {
        _maxHP = maxHP;
        _attrName = attrName;
    }
    public int GetMaxHP() {
        return _maxHP;
    }
    public string GetAttrName() {
        return _attrName;
    }
}
public class EnemyBaseAttr : CharacterBaseAttr {
    public float VarCritRate;
    public EnemyBaseAttr(int maxHP, int critRate, string attrName) : base(maxHP, attrName) {
        VarCritRate = critRate;
    }
    public virtual float GetVarCritRate() {
        return VarCritRate;
    }
}
