public class EnemyAttr : CharacterAttrAbstract {
    protected float CirtRate = 0;
    public EnemyAttr() { }
    public void SetEnemyAttr(EnemyBaseAttr theBaseAttr) {
        base.SetBaseAttr(theBaseAttr);
        CirtRate = theBaseAttr.GetVarCritRate();
    }
    public float GetCirtRate() {
        return CirtRate;
    }
    public void CutDownCirtRate() {
        CirtRate -= CirtRate / 2;
    }
}