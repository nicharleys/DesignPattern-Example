using UnityEngine;
public abstract class IWeapon : MonoBehaviour {
    protected int AtkPlusValue = 0;
    protected int AtkValue = 0;
    protected ICharacter WeaponOwner = null;

    protected GameObject GameObject = null;
    protected AudioSource Audio = null;
    protected bool IsThrowing = false;

    public void SetOwner(ICharacter theCharacter) {
        WeaponOwner = theCharacter;
    }
    public void ChangeThrowState() {
        IsThrowing = true;
    }
    public ICharacter GetWeaponOwner() {
        return WeaponOwner;
    }
    public int GetAtkValue() {
        return AtkValue;
    }
    public void SetAtkPlusValue(int value) {
        AtkPlusValue = value;
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
    protected void SetWeaponSetting(GameObject theObject, AudioSource theAudio) {
        GameObject = theObject;
        Audio = theAudio;
    }
    protected void ShowSoundEffect() {
        Audio.Play();
    }
    protected void Initialize() {
        GameObject.transform.GetChild(0).gameObject.SetActive(true);
        GameObject.transform.GetChild(1).gameObject.SetActive(false);
        GameObject.transform.GetComponent<Rigidbody>().isKinematic = false;
        GameObject.transform.GetComponent<Collider>().isTrigger = false;
        GameObject.gameObject.layer = Constants.THING_LAYER;
        Audio.Stop();
        WeaponOwner = null;
        IsThrowing = false;
    }
    protected void ShowCollisionEffect() {
        GameObject.gameObject.layer = Constants.DEFAULT_LAYER;
        GameObject.transform.GetComponent<Rigidbody>().isKinematic = true;
        GameObject.transform.GetChild(0).gameObject.SetActive(false);
        GameObject.transform.GetChild(1).gameObject.SetActive(true);
    }
    protected void OverLifeTime() {
        GameObject.transform.GetChild(0).gameObject.SetActive(true);
        GameObject.transform.GetChild(1).gameObject.SetActive(false);
        GameObject.transform.GetComponent<Rigidbody>().isKinematic = false;
        GameObject.transform.GetComponent<Collider>().isTrigger = false;
        RecoveryConfirmObj();
    }
    protected void Recovery(ObjectPool thePool) {
        ScatterObjects scatterObjects = GameObject.Find("ObjectPool").GetComponent<ScatterObjects>();
        Vector3 randomScreenPos = new Vector3(Random.Range(-scatterObjects.screenPosRange.x, scatterObjects.screenPosRange.x), 1, Random.Range(-scatterObjects.screenPosRange.z, scatterObjects.screenPosRange.z));
        Quaternion pfbRotation = Quaternion.Euler(0, Random.Range(-180, 180), 0);
        thePool.Recovery(GameObject, randomScreenPos, pfbRotation);
        Initialize();
    }
    public abstract void Attack(ICharacter theTarget);
}
