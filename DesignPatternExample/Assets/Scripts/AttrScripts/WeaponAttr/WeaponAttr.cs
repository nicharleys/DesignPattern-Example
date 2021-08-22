public class WeaponAttr
{
    protected float Atk = 0;
    protected string AttrName = "";
    public WeaponAttr(float atkValue, string attrName) {
        Atk = atkValue;
        AttrName = attrName;
    }
    public virtual float GetAtkValue() {
        return Atk;
    }
}
