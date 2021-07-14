using DesignPatternExample.Character.CharacterSetting;
using UnityEngine;
public class Player : ICharacter {
    private PlayerAttr _playerAttr = null;
    void Start() {
        Initialize(new PlayerAttrStrategy());
    }
    void FixedUpdate() {
        CharacterUpdate(_playerAttr);
    }
    internal override void CharacterAttrInit() {
        _playerAttr = new PlayerAttr(100, "Player");
        _playerAttr.SetAttStrategy(AttrStrategy);
        SetCharacterAttr(_playerAttr);
    }
    internal override void SettingInit() {
        _playerAttr.SetPlayerLv(1);
    }
    internal override void LoopProcess() {
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
    internal override void RunDeadProcess() {
        gameObject.GetComponent<Animator>().SetTrigger("Dead");
    }
}
