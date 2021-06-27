using UnityEngine;
public abstract class IWeapon : MonoBehaviour {
    protected int m_iAtkPlusValue = 0;
    protected int m_iAtkValue = 0;
    protected ICharacter m_WeaponOwner = null;

    protected GameObject m_GameObject = null;
    protected AudioSource m_Audio = null;
    protected bool IsThrowing = false;

    public void SetOwner(ICharacter theCharacter) {
        m_WeaponOwner = theCharacter;
    }
    public void ChangeThrowState() {
        IsThrowing = true;
    }
    public ICharacter GetWeaponOwner() {
        return m_WeaponOwner;
    }
    public void RecoveryConfirmObj() {
        ScatterObjects m_scatterObjects = GameObject.Find("ObjectPool").GetComponent<ScatterObjects>();
        switch(m_GameObject.name) {
            case "Cube(Clone)":
                Recovery(m_scatterObjects.m_CubePool);
                return;
            case "Sphere(Clone)":
                Recovery(m_scatterObjects.m_SpherePool);
                return;
            case "Capsule(Clone)":
                Recovery(m_scatterObjects.m_CapsulePool);
                return;
            default:
                return;
        }
    }
    protected void SetWeaponSetting(GameObject theGameObject, AudioSource theAudio) {
        m_GameObject = theGameObject;
        m_Audio = theAudio;
    }
    protected void Initialize() {
        m_GameObject.transform.GetChild(0).gameObject.SetActive(true);
        m_GameObject.transform.GetChild(1).gameObject.SetActive(false);
        m_GameObject.transform.GetComponent<Rigidbody>().isKinematic = false;
        m_GameObject.transform.GetComponent<Collider>().isTrigger = false;
        m_GameObject.gameObject.layer = Constants.THING_LAYER;
        m_Audio.Stop();
        m_WeaponOwner = null;
        IsThrowing = false;
    }
    protected void ShowSoundEffect() {
        m_Audio.Play();
    }
    protected void ShowCollisionEffect() {
        m_GameObject.gameObject.layer = Constants.DEFAULT_LAYER;
        m_GameObject.transform.GetComponent<Rigidbody>().isKinematic = true;
        m_GameObject.transform.GetChild(0).gameObject.SetActive(false);
        m_GameObject.transform.GetChild(1).gameObject.SetActive(true);
    }
    protected void OverLifeTime() {
        m_GameObject.transform.GetChild(0).gameObject.SetActive(true);
        m_GameObject.transform.GetChild(1).gameObject.SetActive(false);
        m_GameObject.transform.GetComponent<Rigidbody>().isKinematic = false;
        m_GameObject.transform.GetComponent<Collider>().isTrigger = false;
        RecoveryConfirmObj();
    }
    protected void Recovery(ObjectPool objectPool) {
        ScatterObjects m_scatterObjects = GameObject.Find("ObjectPool").GetComponent<ScatterObjects>();
        Vector3 randomScreenPos = new Vector3(Random.Range(-m_scatterObjects.screenPosRange.x, m_scatterObjects.screenPosRange.x), 1, Random.Range(-m_scatterObjects.screenPosRange.z, m_scatterObjects.screenPosRange.z));
        Quaternion pfbRotation = Quaternion.Euler(0, Random.Range(-180, 180), 0);
        objectPool.Recovery(m_GameObject, randomScreenPos, pfbRotation);
        Initialize();
    }
    public abstract void Attack(ICharacter theTarget);
}
