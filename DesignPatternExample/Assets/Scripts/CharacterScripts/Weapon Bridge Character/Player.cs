using UnityEngine;
using UnityEngine.UI;

public class Player : ICharacter {
    private PlayerAttr _playerAttr = null;
    void Start() {
        Initialize(new PlayerAttrStrategy());
    }
    void FixedUpdate() {
        CharacterUpdate(_playerAttr);
    }
    public override void CharacterAttrInit() {
        _playerAttr = new PlayerAttr(100, "Player");
        _playerAttr.SetAttStrategy(AttrStrategy);
        SetCharacterAttr(_playerAttr);
    }
    public override void SettingInit() {
        _playerAttr.SetPlayerLv(1);
    }
    public override void LoopProcess() {
        SearchWeapons();
        if(HitThing != null && IsThrowingThing == false && Input.GetKeyDown(KeyCode.LeftControl)) {
            ThrowAttack();
        }
    }
    public override void SearchWeapons() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if(Input.GetMouseButton(0) && Physics.Raycast(ray, out hit)) {
            if(hit.transform.gameObject.layer == Constants.THING_LAYER && IsGettingThing == false && hit.transform.GetComponent<IWeapon>().GetWeaponOwner() == null) {
                ChangeWeaponInHand(hit.transform.gameObject);
                SetWeaponAtkPlusValue(10);
            }
        }
    }
    public override void RunDeadProcess() {
        gameObject.GetComponent<Animator>().SetTrigger("Dead");
    }
}
