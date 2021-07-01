public class EnemyAttr : ICharacterAttr {
    private EnemyAttr() { }
    public EnemyAttr(float maxHp, string attrName, float cirtRate) {
        MaxHp = maxHp;
        NowHp = maxHp;
        AttrName = attrName;
        CirtRate = cirtRate;
    }
    public float GetCirtRate() {
        return CirtRate;
    }
    public void CutDownCirtRate() {
        CirtRate -= CirtRate / 2;
    }
}
