using UnityEngine;

public class WeaponCapsule : IWeapon {
    private float m_fSecondTime;
    private bool m_bTouchObj = false;
    private AudioSource m_WeaponAudio;
    private bool _isHpCuting = false;
    public WeaponCapsule() { }
    void Awake() {
        m_WeaponAudio = gameObject.GetComponent<AudioSource>();
        SetWeaponSetting(gameObject, m_WeaponAudio);
    }
    void FixedUpdate() {
        if(gameObject.transform.position.y < 0 && IsThrowing == true) {
            RecoveryConfirmObj();
        }
        if(m_bTouchObj == true) {
            TimeCount();
        }
        RunOverLifeTime();
    }
    private void OnCollisionEnter(Collision collision) {
        if(m_WeaponOwner != null) {
            Attack(collision.gameObject.GetComponent<ICharacter>());
            m_bTouchObj = true;
        }
    }
    private void TimeCount() {
        m_fSecondTime += Time.deltaTime;
    }
    private void RunOverLifeTime() {
        if(m_fSecondTime >= 1.2f) {
            m_bTouchObj = false;
            m_fSecondTime = 0;
            _isHpCuting = false;
            OverLifeTime();
        }
    }
    public override void Attack(ICharacter theTarget) {
        ShowCollisionEffect();
        ShowSoundEffect();
        if(theTarget != null && _isHpCuting == false) {
            _isHpCuting = true;
            theTarget.CharacterHp -= 10;
        }
    }
}
