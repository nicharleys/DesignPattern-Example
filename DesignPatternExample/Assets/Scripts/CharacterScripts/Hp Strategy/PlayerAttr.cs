public class PlayerAttr : ICharacterAttr
{
    private PlayerAttr() { }
    public PlayerAttr(float maxHp, string attrName) {
        MaxHp = maxHp;
        NowHp = maxHp;
        AttrName = attrName;
    }
    public void SetPlayerLv(int lv) {
        CharacterLv = lv;
    }
    public int GetPlayerLv() {
        return CharacterLv;
    }
    public void AddExtraMaxHp(float extraMaxHp) {
        ExtraMaxHp = extraMaxHp;
    }
    public override float GetMaxHp() {
        return base.GetMaxHp() + ExtraMaxHp;
    }
}
