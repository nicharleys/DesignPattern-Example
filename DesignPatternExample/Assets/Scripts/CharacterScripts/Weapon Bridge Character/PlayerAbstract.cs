using DesignPatternExample.Character.CharacterSetting;
using UnityEngine;
public enum PlayerType {
    Null = 0,
    A = 1,
    B = 2,
    C = 3,
    Max,
}
public abstract class PlayerAbstract : CharacterAbstract {
    private PlayerAttr _playerAttr = null;
    private Transform _cam;
    private bool _isJump;
    internal override void Initialize() {
        PlayerMoveInit();
        CharacterInit(new PlayerAttrStrategy());
    }
    internal override void Update() {
        CheckJump();
    }
    internal override void FixedUpdate() {
        UpdateCharacter(_playerAttr);
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
    private void SearchWeapons() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if(Input.GetMouseButton(0) && Physics.Raycast(ray, out hit)) {
            if(hit.transform.gameObject.layer == LayerMask.NameToLayer("Thing") && IsGettingThing == false && hit.transform.GetComponent<WeaponAbstract>().GetWeaponOwner() == null) {
                ChangeWeaponInHand(hit.transform.gameObject);
                SetWeaponAtkPlusValue(10);
            }
        }
    }
    private void PlayerMoveInit() {
        _cam = Camera.main.transform;
        Cursor.lockState = CursorLockMode.Confined;
    }
    private void PlayerMove() {
        Vector3 move = _cam.forward * Input.GetAxis("Vertical") + _cam.right * Input.GetAxis("Horizontal");
        move = Vector3.Scale(move, new Vector3(1, 0, 1)).normalized;
        if(Input.GetKeyDown(KeyCode.LeftShift)) {
            move *= 0.5f;
        }
        TpCtr.Move(move, _isJump);
        _isJump = false;
    }
    private void CheckJump() {
        if(Input.GetButtonDown("Jump") && _playerAttr.GetNowHp() > 0) {
            _isJump = true;
        }
    }
}
