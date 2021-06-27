using UnityEngine;
using UnityEngine.UI;

public class IPlayer : ICharacter {
    public Transform botHand;
    private GameObject HitThing = null;
    private bool m_bGetThing = false;
    private bool m_bThrowThing = false;
    private bool _isdied = false;

    [SerializeField] private Image _CharacterHpUi;
    //public override void Initizal() { 

    //}
    //public override void Update() { 

    //}
    //public override void Release() { 

    //}
    void Start() {
        CharacterHp = 100f;
    }
    void FixedUpdate() {
        if(CharacterHp != 0) {
            Attack();
        }
        else {
            if(_isdied == false) {
                _isdied = true;
                Dead();
            }
        }
        _CharacterHpUi.fillAmount = Mathf.Lerp(_CharacterHpUi.fillAmount, CharacterHp / 100f, 0.1f);
    }
    public override void Attack() {
        SearchWeapons();
        ThrowAttack();
    }
    public override void UnderAttack(ICharacter theAttacker) {
        throw new System.NotImplementedException();
    }
    public override void SearchWeapons() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if(Input.GetMouseButton(0) && Physics.Raycast(ray, out hit)) {
            if(hit.transform.gameObject.layer == Constants.THING_LAYER && m_bGetThing == false && hit.transform.GetComponent<IWeapon>().GetWeaponOwner() == null) {
                m_bGetThing = true;
                HitThing = hit.transform.gameObject;

                SetWeapon(HitThing.GetComponent<IWeapon>());

                HitThing.GetComponent<Rigidbody>().isKinematic = true;
                HitThing.GetComponent<Collider>().isTrigger = true;
                HitThing.transform.SetParent(botHand);
                HitThing.transform.localPosition = Vector3.zero;
                HitThing.transform.localRotation = Quaternion.identity;
            }
        }
    }
    public override void ThrowAttack() {
        if(HitThing != null && m_bThrowThing == false && Input.GetKeyDown(KeyCode.LeftControl)) {
            m_bThrowThing = true;
            gameObject.GetComponent<Animator>().SetTrigger("Throw");
        }
    }
    public override void Dead() {
        gameObject.GetComponent<Animator>().SetTrigger("Dead");

    }
    public void PlayerThrow() {
        HitThing.transform.SetParent(null);
        HitThing.GetComponent<Rigidbody>().isKinematic = false;
        HitThing.GetComponent<Collider>().isTrigger = false;
        HitThing.GetComponent<Rigidbody>().AddForce(gameObject.transform.forward * 1000);
        HitThing.GetComponent<IWeapon>().ChangeThrowState();
        HitThing = null;
        m_bGetThing = false;
        m_bThrowThing = false;
    }
}
