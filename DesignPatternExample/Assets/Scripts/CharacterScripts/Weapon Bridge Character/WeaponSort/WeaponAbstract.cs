using DesignPatternExample.Character.CharacterSetting;
using UnityEngine;
public enum WeaponType {
    Null = 0,
    Cube = 1,
    Sphere = 2,
    Capsule = 3,
    Max,
}
public abstract class WeaponAbstract : MonoBehaviour {
    protected float AtkPlusValue = 0;
    protected WeaponAttr WeaponAttr = null;
    protected CharacterAbstract WeaponOwner = null;

    protected GameObject WeaponObject = null;
    protected AudioSource Audio = null;
    protected bool IsThrowing = false;

    protected float SecondTime;
    protected bool IsTouchingObj = false;
    protected bool IsHpCut = false;

    public void SetOwner(CharacterAbstract theCharacter) {
        WeaponOwner = theCharacter;
    }
    public void SetWeaponAttr(WeaponAttr theWeaponAttr) {
        WeaponAttr = theWeaponAttr;
    }
    public void ChangeThrowState() {
        IsThrowing = true;
    }
    public CharacterAbstract GetWeaponOwner() {
        return WeaponOwner;
    }
    public void SetAtkPlusValue(int value) {
        AtkPlusValue = value;
    }
    public float GetAtkValue() {
        return WeaponAttr.GetAtkValue() + AtkPlusValue;
    }
    public void WeaponInit(GameObject theGameObject) {
        SetGameObject(theGameObject);
        WeaponSetting();
    }
    public void WeaponUpdate(float lifeTime) {
        if(WeaponObject.transform.position.y < 0 && IsThrowing == true) {
            RecoveryConfirmObj();
        }
        if(IsTouchingObj == true) {
            SecondTime += Time.deltaTime;
        }
        if(SecondTime >= lifeTime) {
            IsTouchingObj = false;
            SecondTime = 0;
            IsHpCut = false;
            OverLifeTime();
        }
    }
    public void WeaponCollision(Collision theCollision) {
        if(WeaponOwner != null) {
            ShowCollisionEffect();
            ShowSoundEffect();
            CharacterAbstract target = theCollision.gameObject.GetComponent<CharacterAbstract>();
            if(target != null && target != WeaponOwner && IsHpCut == false) {
                IsHpCut = true;
                target.GetAttribute().CalDmgValue(WeaponOwner);
            }
            IsTouchingObj = true;
        }
    }
    public void SetGameObject(GameObject theWeaponObject) {
        WeaponObject = theWeaponObject;
        Audio = theWeaponObject.GetComponent<AudioSource>();
    }
    private void ShowSoundEffect() {
        Audio.Play();
    }
    private void ShowCollisionEffect() {
        WeaponObject.gameObject.layer = Constants.DEFAULT_LAYER;
        WeaponObject.transform.GetComponent<Rigidbody>().isKinematic = true;
        WeaponObject.transform.GetChild(0).gameObject.SetActive(false);
        WeaponObject.transform.GetChild(1).gameObject.SetActive(true);
    }
    private void OverLifeTime() {
        WeaponObject.transform.GetChild(0).gameObject.SetActive(true);
        WeaponObject.transform.GetChild(1).gameObject.SetActive(false);
        WeaponObject.transform.GetComponent<Rigidbody>().isKinematic = false;
        WeaponObject.transform.GetComponent<Collider>().isTrigger = false;
        RecoveryConfirmObj();
    }
    protected void Recovery(ObjectPool thePool) {
        ScatterObjects scatterObjects = GameObject.Find("ObjectPool").GetComponent<ScatterObjects>();
        Vector3 randomScreenPos = new Vector3(Random.Range(-scatterObjects.screenPosRange.x, scatterObjects.screenPosRange.x), 1, Random.Range(-scatterObjects.screenPosRange.z, scatterObjects.screenPosRange.z));
        Quaternion pfbRotation = Quaternion.Euler(0, Random.Range(-180, 180), 0);
        thePool.Recovery(WeaponObject, randomScreenPos, pfbRotation);
        Initialize();
    }
    private void Initialize() {
        WeaponObject.transform.GetChild(0).gameObject.SetActive(true);
        WeaponObject.transform.GetChild(1).gameObject.SetActive(false);
        WeaponObject.transform.GetComponent<Rigidbody>().isKinematic = false;
        WeaponObject.transform.GetComponent<Collider>().isTrigger = false;
        WeaponObject.gameObject.layer = Constants.THING_LAYER;
        Audio.Stop();
        WeaponOwner = null;
        IsThrowing = false;
    }
    protected abstract void WeaponSetting();
    protected abstract void RecoveryConfirmObj();
}
