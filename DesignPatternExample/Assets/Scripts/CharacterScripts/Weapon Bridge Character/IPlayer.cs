using UnityEngine;
using UnityEngine.UI;

public class IPlayer : ICharacter {
    [SerializeField] private Transform _botHandPos;
    [SerializeField] private Image _characterHpUi;
    private GameObject _hitThing = null;
    private bool _isGettingThing = false;
    private bool _isThrowingThing = false;
    private bool _isdead = false;

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
            if(_isdead == false) {
                _isdead = true;
                Dead();
            }
        }
        _characterHpUi.fillAmount = Mathf.Lerp(_characterHpUi.fillAmount, CharacterHp / 100f, 0.1f);
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
            if(hit.transform.gameObject.layer == Constants.THING_LAYER && _isGettingThing == false && hit.transform.GetComponent<IWeapon>().GetWeaponOwner() == null) {
                _isGettingThing = true;
                _hitThing = hit.transform.gameObject;

                SetWeapon(_hitThing.GetComponent<IWeapon>());

                _hitThing.GetComponent<Rigidbody>().isKinematic = true;
                _hitThing.GetComponent<Collider>().isTrigger = true;
                _hitThing.transform.SetParent(_botHandPos);
                _hitThing.transform.localPosition = Vector3.zero;
                _hitThing.transform.localRotation = Quaternion.identity;
            }
        }
    }
    public override void ThrowAttack() {
        if(_hitThing != null && _isThrowingThing == false && Input.GetKeyDown(KeyCode.LeftControl)) {
            _isThrowingThing = true;
            gameObject.GetComponent<Animator>().SetTrigger("Throw");
        }
    }
    public override void Dead() {
        gameObject.GetComponent<Animator>().SetTrigger("Dead");

    }
    public void PlayerThrow() {
        _hitThing.transform.SetParent(null);
        _hitThing.GetComponent<Rigidbody>().isKinematic = false;
        _hitThing.GetComponent<Collider>().isTrigger = false;
        _hitThing.GetComponent<Rigidbody>().AddForce(gameObject.transform.forward * 1000);
        _hitThing.GetComponent<IWeapon>().ChangeThrowState();
        _hitThing = null;
        _isGettingThing = false;
        _isThrowingThing = false;
    }
}
