using DesignPatternExample.Character.CharacterSetting;
using UnityEngine;
public abstract class IWeapon : MonoBehaviour {
    protected float AtkPlusValue = 0;
    protected float AtkValue = 0;
    protected ICharacter WeaponOwner = null;

    protected GameObject GameObject = null;
    protected AudioSource Audio = null;
    protected bool IsThrowing = false;

    protected float SecondTime;
    protected bool IsTouchingObj = false;
    protected bool IsHpCut = false;

    public void SetOwner(ICharacter theCharacter) {
        WeaponOwner = theCharacter;
    }
    public void ChangeThrowState() {
        IsThrowing = true;
    }
    public ICharacter GetWeaponOwner() {
        return WeaponOwner;
    }
    public float GetAtkValue() {
        return AtkValue;
    }
    public void SetAtkPlusValue(int value) {
        AtkPlusValue = value;
    }
    public void RunWeaponAwake(GameObject theGameObject, AudioSource  theAudio) {
        SetWeaponSetting(theGameObject, theAudio);
        WeaponSetting();
    }
    public void RunWeaponUpdate(GameObject theGameObject, float lifeTime) {
        if(theGameObject.transform.position.y < 0 && IsThrowing == true) {
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
    public void RunWeaponCollision(Collision theCollision) {
        if(WeaponOwner != null) {
            ShowCollisionEffect();
            ShowSoundEffect();
            ICharacter target = theCollision.gameObject.GetComponent<ICharacter>();
            if(target != null && IsHpCut == false) {
                IsHpCut = true;
                target.GetAttribute().CalDmgValue(WeaponOwner);
            }
            IsTouchingObj = true;
        }
    }
    public void RecoveryConfirmObj() {
        ScatterObjects scatterObjects = GameObject.Find("ObjectPool").GetComponent<ScatterObjects>();
        switch(GameObject.name) {
            case "Cube(Clone)":
                Recovery(scatterObjects.CubePool);
                return;
            case "Sphere(Clone)":
                Recovery(scatterObjects.SpherePool);
                return;
            case "Capsule(Clone)":
                Recovery(scatterObjects.CapsulePool);
                return;
            default:
                return;
        }
    }
    public void SetWeaponSetting(GameObject theObject, AudioSource theAudio) {
        GameObject = theObject;
        Audio = theAudio;
    }
    private void ShowSoundEffect() {
        Audio.Play();
    }
    private void Initialize() {
        GameObject.transform.GetChild(0).gameObject.SetActive(true);
        GameObject.transform.GetChild(1).gameObject.SetActive(false);
        GameObject.transform.GetComponent<Rigidbody>().isKinematic = false;
        GameObject.transform.GetComponent<Collider>().isTrigger = false;
        GameObject.gameObject.layer = Constants.THING_LAYER;
        Audio.Stop();
        WeaponOwner = null;
        IsThrowing = false;
    }
    private void ShowCollisionEffect() {
        GameObject.gameObject.layer = Constants.DEFAULT_LAYER;
        GameObject.transform.GetComponent<Rigidbody>().isKinematic = true;
        GameObject.transform.GetChild(0).gameObject.SetActive(false);
        GameObject.transform.GetChild(1).gameObject.SetActive(true);
    }
    private void OverLifeTime() {
        GameObject.transform.GetChild(0).gameObject.SetActive(true);
        GameObject.transform.GetChild(1).gameObject.SetActive(false);
        GameObject.transform.GetComponent<Rigidbody>().isKinematic = false;
        GameObject.transform.GetComponent<Collider>().isTrigger = false;
        RecoveryConfirmObj();
    }
    private void Recovery(ObjectPool thePool) {
        ScatterObjects scatterObjects = GameObject.Find("ObjectPool").GetComponent<ScatterObjects>();
        Vector3 randomScreenPos = new Vector3(Random.Range(-scatterObjects.screenPosRange.x, scatterObjects.screenPosRange.x), 1, Random.Range(-scatterObjects.screenPosRange.z, scatterObjects.screenPosRange.z));
        Quaternion pfbRotation = Quaternion.Euler(0, Random.Range(-180, 180), 0);
        thePool.Recovery(GameObject, randomScreenPos, pfbRotation);
        Initialize();
    }
    protected abstract void WeaponSetting();
}
