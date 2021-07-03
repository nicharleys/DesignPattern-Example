using UnityEngine;
using UnityEngine.UI;

public abstract class ICharacter : MonoBehaviour {
    [SerializeField] protected Transform BotHandPos;
    [SerializeField] protected Image CharacterHpUi;
    protected bool Isdead = false;
    protected bool IsGettingThing = false;
    protected bool IsThrowingThing = false;
    protected GameObject HitThing = null;

    protected IAttrStrategy AttrStrategy = null;
    protected ICharacterAttr Attribute = null;
    protected string AttrName = null;

    private IWeapon _weapon = null;

    public IWeapon GetWeapon() {
        return _weapon;
    }
    protected void SetWeaponAtkPlusValue(int value) {
        _weapon.SetAtkPlusValue(value);
    }
    public float GetWeaponAtkValue() {
        return _weapon.GetAtkValue();
    }
    public ICharacterAttr GetAttribute() {
        return Attribute;
    }
    public virtual void SetCharacterAttr(ICharacterAttr characterAttr) {
        Attribute = characterAttr;
        Attribute.InitAttr();
        AttrName = Attribute.GetAttrName();
    }
    protected void Initialize(IAttrStrategy theAttrStrategy) {
        AttrStrategy = theAttrStrategy;
        CharacterAttrInit();
        SettingInit();
    }
    protected void CharacterUpdate(ICharacterAttr theCharacterAttr) {
        if(theCharacterAttr.GetNowHp() != 0) {
            LoopProcess();
        }
        if(Attribute.GetNowHp() <= 0) {
            if(Isdead == false) {
                Isdead = true;
                RunDeadProcess();
            }
        }
        CharacterHpUi.fillAmount = Mathf.Lerp(CharacterHpUi.fillAmount, theCharacterAttr.GetNowHp() / theCharacterAttr.GetMaxHp(), 0.1f);
    }
    protected void ChangeWeaponInHand(GameObject hitThingObj) {
        IsGettingThing = true;
        HitThing = hitThingObj;
        _weapon = HitThing.GetComponent<IWeapon>();
        _weapon.SetOwner(this);
        HitThing.GetComponent<Rigidbody>().isKinematic = true;
        HitThing.GetComponent<Collider>().isTrigger = true;
        HitThing.transform.SetParent(BotHandPos);
        HitThing.transform.localPosition = Vector3.zero;
        HitThing.transform.localRotation = Quaternion.identity;
    }
    protected void ThrowThing() {
        HitThing.transform.SetParent(null);
        HitThing.GetComponent<Rigidbody>().isKinematic = false;
        HitThing.GetComponent<Collider>().isTrigger = false;
        HitThing.GetComponent<Rigidbody>().AddForce(gameObject.transform.forward * 1000);
        HitThing.GetComponent<IWeapon>().ChangeThrowState();
        HitThing = null;
        IsGettingThing = false;
        IsThrowingThing = false;
    }
    protected void ThrowAttack() {
        IsThrowingThing = true;
        gameObject.GetComponent<Animator>().SetTrigger("Throw");
    }
    public abstract void SettingInit();
    public abstract void CharacterAttrInit();
    public abstract void LoopProcess();
    public abstract void SearchWeapons();
    public abstract void RunDeadProcess();
}
