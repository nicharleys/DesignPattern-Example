using DesignPatternExample.CharacterAI.StatePattern;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
namespace DesignPatternExample.Character.CharacterSetting {
    public abstract class CharacterAbstract {
        public GameObject Character = null;
        protected internal Transform BotHandPos;
        protected Image CharacterHpUi;
        protected internal TPController TpCtr = null;
        protected internal Animator Anim = null;
        protected internal NavMeshAgent Agent = null;
        protected CharacterAIAbstract CharacterAI = null;

        protected internal bool IsGettingThing = false;
        protected internal bool IsThrowingThing = false;

        protected bool IsDead = false;
        protected bool IsRunDeadEvent = false;
        protected bool CanRemove = false;
        protected float RemoveTime = 1.5f;

        protected AttrStrategyAbstract AttrStrategy = null;
        protected CharacterAttrAbstract Attribute = null;
        protected string AttrName = null;

        protected GameObject HitThing = null;
        private WeaponAbstract _weapon = null;
        public void SetGameObject(GameObject theCharacter) {
            Character = theCharacter;
            BotHandPos = theCharacter.transform.Find("BotHand");
            CharacterHpUi = theCharacter.transform.Find("HpBar").GetComponent<Image>();
            TpCtr = theCharacter.GetComponent<TPController>();
            Anim = theCharacter.GetComponent<Animator>();
            Agent = theCharacter.GetComponent<NavMeshAgent>();
        }
        internal CharacterAttrAbstract GetAttribute() {
            return Attribute;
        }
        internal WeaponAbstract GetWeapon() {
            return _weapon;
        }
        protected void SetWeaponAtkPlusValue(int value) {
            _weapon.SetAtkPlusValue(value);
        }
        internal float GetWeaponAtkValue() {
            return _weapon.GetAtkValue();
        }
        internal bool GetIsDead() {
            return IsDead;
        }
        internal bool GetIsRunDeadEvent() {
            return IsRunDeadEvent;
        }
        internal bool GetCanRemove() {
            return CanRemove;
        }
        internal virtual void SetCharacterAttr(CharacterAttrAbstract characterAttr) {
            Attribute = characterAttr;
            Attribute.InitAttr();
            AttrName = Attribute.GetAttrName();
        }
        internal virtual void SetAI(CharacterAIAbstract characterAI) {
            CharacterAI = characterAI;
        }
        protected void CharacterInit(AttrStrategyAbstract theAttrStrategy) {
            AttrStrategy = theAttrStrategy;
            CharacterAttrInit();
            SettingInit();
        }
        internal void Release() {

        }
        internal void UpdateCharacter(CharacterAttrAbstract theCharacterAttr) {
            if(theCharacterAttr.GetNowHp() > 0) {
                LoopProcess();
            }
            if(Attribute.GetNowHp() <= 0) {
                if(IsDead == false) {
                    IsDead = true;
                    RunDeadProcess();
                }
            }
            CharacterHpUi.fillAmount = Mathf.Lerp(CharacterHpUi.fillAmount, theCharacterAttr.GetNowHp() / theCharacterAttr.GetMaxHP(), 0.1f);
        }
        internal void ChangeWeaponInHand(GameObject hitThingObj) {
            IsGettingThing = true;
            HitThing = hitThingObj;
            _weapon = HitThing.GetComponent<WeaponAbstract>();
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
            HitThing.GetComponent<Rigidbody>().AddForce(Character.transform.forward * 1000);
            HitThing.GetComponent<WeaponAbstract>().ChangeThrowState();
            HitThing = null;
            IsGettingThing = false;
            IsThrowingThing = false;
        }
        internal void ThrowAttack() {
            IsThrowingThing = true;
            Character.GetComponent<Animator>().SetTrigger("Throw");
        }
        internal void ChangeWeaponOutHand() {
            if(HitThing == null)
                return;
            HitThing.transform.SetParent(null);
            HitThing.GetComponent<Rigidbody>().isKinematic = false;
            HitThing.GetComponent<Collider>().isTrigger = false;
            HitThing = null;
            _weapon.SetOwner(null);
            _weapon = null;
            IsGettingThing = false;
            IsThrowingThing = false;
        }
        internal void RunDeadProcess() {
            Character.GetComponent<Animator>().SetTrigger("Dead");
            ChangeWeaponOutHand();
            IsRunDeadEvent = true;
        }
        internal abstract void Initialize();
        internal abstract void SettingInit();
        internal abstract void CharacterAttrInit();
        internal abstract void Update();
        internal abstract void FixedUpdate();
        internal abstract void LoopProcess();
    }
}