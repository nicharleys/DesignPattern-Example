public class PlayerAttr : CharacterAttrAbstract {
    protected int CharacterLv = 0;
    protected float ExtraMaxHP;
    public PlayerAttr() {}
    public void SetPlayerAttr(CharacterBaseAttr theBaseAttr) {
        base.SetBaseAttr(theBaseAttr);
        CharacterLv = 1;
    }
    public void SetPlayerLv(int lv) {
        CharacterLv = lv;
    }
    public int GetPlayerLv() {
        return CharacterLv;
    }
    public override float GetMaxHP() {
        return base.GetMaxHP() + ExtraMaxHP;
    }
    public void AddExtraMaxHP(float extraMaxHp) {
        ExtraMaxHP = extraMaxHp;
    }
}
