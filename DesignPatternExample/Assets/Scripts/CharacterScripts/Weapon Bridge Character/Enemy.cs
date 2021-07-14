using DesignPatternExample.Character.CharacterSetting;
using DesignPatternExample.CharacterAI.StatePattern;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : ICharacter {
    private NavMeshAgent _agent;
    private TPController _tpCtr;
    private Animator _anim;
    private ICharacterAI _characterAI = null;
    private EnemyAttr _enemyAttr = null;

    private float _seeRange = 20;
    private float _attackRange = 12f;
    private float _safeRange = 5f;
    private float _seeAngle = 120;
    void Start() {
        Initialize(new EnemyAttrStrategy());
    }
    void FixedUpdate() {
        CharacterUpdate(_enemyAttr);
    }
    internal override void CharacterAttrInit() {
        _enemyAttr = new EnemyAttr(100, "Enemy", 30);
        _enemyAttr.SetAttStrategy(AttrStrategy);
        SetCharacterAttr(_enemyAttr);
    }
    internal override void SettingInit() {
        _agent = GetComponent<NavMeshAgent>();
        _tpCtr = GetComponent<TPController>();
        _anim = GetComponent<Animator>();
        _agent.updatePosition = true;
        _agent.updateRotation = false;
        _anim.SetBool("IsFighting", false);

        _characterAI = new EnemyAI(this, _agent, _tpCtr, _anim);
    }
    internal override void LoopProcess() {
        _characterAI.Update();
    }
    internal override void RunDeadProcess() {
        gameObject.GetComponent<Animator>().SetTrigger("Dead");
        _agent.speed = 0;
    }
    private void OnDrawGizmos() {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, _seeRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _attackRange);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, _safeRange);

        Gizmos.color = Color.white;
        Vector3 p1 = Quaternion.Euler(0, _seeAngle / 2, 0) * transform.forward * _seeRange + transform.position;
        Vector3 p2 = Quaternion.Euler(0, -_seeAngle / 2, 0) * transform.forward * _seeRange + transform.position;
        Gizmos.DrawLine(transform.position, p1);
        Gizmos.DrawLine(transform.position, p2);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(_characterAI.DestPosition, 0.2f);
    }
}