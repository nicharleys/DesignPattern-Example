using DesignPatternExample.Character.CharacterSetting;
using UnityEngine;
public class Player : ICharacter {
    private PlayerAttr _playerAttr = null;

    private Transform _cam;
    private TPController _tpController;
    private bool _isJump;

    void Start() {
        PlayerMoveInit();
        Initialize(new PlayerAttrStrategy());
    }
    void Update() {
        CheckJump();
    }
    void FixedUpdate() {
        CharacterUpdate(_playerAttr);
    }
    internal override void CharacterAttrInit() {
        _playerAttr = new PlayerAttr(1, "Player");
        _playerAttr.SetAttStrategy(AttrStrategy);
        SetCharacterAttr(_playerAttr);
    }
    internal override void SettingInit() {
        _playerAttr.SetPlayerLv(1);
    }
    internal override void LoopProcess() {
        PlayerMove();
        SearchWeapons();
        if(HitThing != null && IsThrowingThing == false && Input.GetKeyDown(KeyCode.LeftControl)) {
            ThrowAttack();
        }
    }
    public void SearchWeapons() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if(Input.GetMouseButton(0) && Physics.Raycast(ray, out hit)) {
            if(hit.transform.gameObject.layer == LayerMask.NameToLayer("Thing") && IsGettingThing == false && hit.transform.GetComponent<IWeapon>().GetWeaponOwner() == null) {
                ChangeWeaponInHand(hit.transform.gameObject);
                SetWeaponAtkPlusValue(10);
            }
        }
    }
    private void PlayerMoveInit() {
        _cam = Camera.main.transform;
        _tpController = GetComponent<TPController>();
        Cursor.lockState = CursorLockMode.Confined;
    }
    private void PlayerMove() {
        Vector3 move = _cam.forward * Input.GetAxis("Vertical") + _cam.right * Input.GetAxis("Horizontal");
        move = Vector3.Scale(move, new Vector3(1, 0, 1)).normalized;
        if(Input.GetKeyDown(KeyCode.LeftShift)) {
            move *= 0.5f;
        }
        _tpController.Move(move, _isJump);
        _isJump = false;
    }
    private void CheckJump() {
        if(Input.GetButtonDown("Jump") && _playerAttr.GetNowHp() > 0) {
            _isJump = true;
        }
    }
    internal override void RunDeadProcess() {
        gameObject.GetComponent<Animator>().SetTrigger("Dead");
        ChangeWeaponOutHand();
    }
}
