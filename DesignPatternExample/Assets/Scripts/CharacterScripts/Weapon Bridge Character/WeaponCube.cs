using UnityEngine;

public class WeaponCube : IWeapon {
    private float _secondTime;
    private bool _isTouchingObj = false;
    private AudioSource _weaponAudio;
    private bool _isHpCut = false;
    public WeaponCube() { }
    void Awake() {
        _weaponAudio = gameObject.GetComponent<AudioSource>();
        SetWeaponSetting(gameObject, _weaponAudio);
    }
    void FixedUpdate() {
        if(gameObject.transform.position.y < 0 && IsThrowing == true) {
            RecoveryConfirmObj();
        }
        if(_isTouchingObj == true) {
            TimeCount();
        }
        RunOverLifeTime();
    }
    private void OnCollisionEnter(Collision theCollision) {
        if(WeaponOwner != null) {
            Attack(theCollision.gameObject.GetComponent<ICharacter>());
            _isTouchingObj = true;
        }
    }
    private void TimeCount() {
        _secondTime += Time.deltaTime;
    }
    private void RunOverLifeTime() {
        if(_secondTime >= 1.2f) {
            _isTouchingObj = false;
            _secondTime = 0;
            _isHpCut = false;
            OverLifeTime();
        }
    }
    public override void Attack(ICharacter target) {
        ShowCollisionEffect();
        ShowSoundEffect();
        if(target != null && _isHpCut == false) {
            _isHpCut = true;
            target.CharacterHp -= 10;
        }
    }
}
