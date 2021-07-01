using UnityEngine;
using UnityEngine.UI;

public class Player : ICharacter {
    [SerializeField] private Transform _botHandPos;
    [SerializeField] private Image _characterHpUi;
    private GameObject _hitThing = null;
    private bool _isGettingThing = false;
    private bool _isThrowingThing = false;
    private bool _isdead = false;
    private PlayerAttr _playerAttr = null;
    void Start() {
        PlayerAttrStrategy playerAttrStrategy = new PlayerAttrStrategy();
        _playerAttr = new PlayerAttr(100, "Player");
        _playerAttr.SetAttStrategy(playerAttrStrategy);
        SetCharacterAttr(_playerAttr);
        _playerAttr.SetPlayerLv(1);
    }
    void FixedUpdate() {
        if(_playerAttr.GetNowHp() != 0) {
            Attack();
        }
        CheckHp();
        _characterHpUi.fillAmount = Mathf.Lerp(_characterHpUi.fillAmount, _playerAttr.GetNowHp() / _playerAttr.GetMaxHp(), 0.1f);
    }
    public override void Attack() {
        SearchWeapons();
        ThrowAttack();
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
    public void CheckHp() {
        if(Attribute.GetNowHp() <= 0) {
            if(_isdead == false) {
                _isdead = true;
                gameObject.GetComponent<Animator>().SetTrigger("Dead");
            }
        }
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
