using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Enemy : ICharacter {
    [SerializeField] private LayerMask _player;
    [SerializeField] private NavMeshAgent _agent;

    private float _seeRange = 20;
    private float _attackRange = 12f;
    private float _safeRange = 5f;
    private float _seeAngle = 120;

    private Vector3 _destPosition;
    private Transform _attackTarget;
    private TPController _tpController;
    private Animator _animator;

    private float _actionEndTime;
    private bool _iNoFightEnter = false;
    private bool _isFightEnter = false;
    private bool _isActionInit = false;
    private bool _isSearchingWeapon = false;
    private EnemyAttr _enemyAttr = null;
    private AiFightState FightState;

    void Start() {
        Initialize(new EnemyAttrStrategy());
    }
    void FixedUpdate() {
        CharacterUpdate(_enemyAttr);
    }
    public override void CharacterAttrInit() {
        _enemyAttr = new EnemyAttr(100, "Enemy", 30);
        _enemyAttr.SetAttStrategy(AttrStrategy);
        SetCharacterAttr(_enemyAttr);
    }
    public override void SettingInit() {
        _tpController = GetComponent<TPController>();
        _animator = GetComponent<Animator>();
        _agent.updatePosition = true;
        _agent.updateRotation = false;
        _animator.SetBool("IsFighting", false);
    }
    public override void LoopProcess() {
        MoveSetting();
        if(_animator.GetBool("IsFighting") == false) {
            NoFightStateUpdate();
        }
        if(_animator.GetBool("IsFighting") == true) {
            FightStateUpdate();
        }
    }
    private void MoveSetting() {
        _agent.SetDestination(_destPosition);
        if(_agent.remainingDistance > _agent.stoppingDistance) {
            _tpController.Move(_agent.desiredVelocity, false);
        }
        else {
            _tpController.Move(Vector3.zero, false);
        }
    }
    public void NoFightStateUpdate() {
        if(!_iNoFightEnter) {
            _iNoFightEnter = true;
            _actionEndTime = Time.time + 1.5f;
            if(UnityEngine.Random.Range(0, 100) % 2 == 0) {
                _agent.speed = 0;
            }
            else {
                _agent.speed = 0.5f;
                _destPosition = Vector3.ProjectOnPlane(UnityEngine.Random.onUnitSphere, Vector3.up) * 10 + transform.position;
            }
        }
        if(Time.time > _actionEndTime) {
            _iNoFightEnter = false;
            _animator.SetTrigger("Exit");
        }
        else {
            Collider[] colliders = Physics.OverlapSphere(transform.position, _seeRange, _player);
            for(int i = 0; i < colliders.Length; i++) {
                if(Vector3.Angle(transform.forward, colliders[i].transform.position - transform.position) < _seeAngle / 2) {
                    _attackTarget = colliders[0].transform;
                    _animator.SetBool("IsFighting", true);
                    break;
                }
                else {
                    if(colliders[i].GetComponent<Animator>().GetFloat("Speed") > 0.75f) {
                        _attackTarget = colliders[0].transform;
                        _animator.SetBool("IsFighting", true);
                        break;
                    }
                }
            }
        }
    }
    public void FightStateUpdate() {
        if(!_isFightEnter) {
            _isFightEnter = true;
            _actionEndTime = Time.time + 1.5f;
            _isActionInit = false;
        }
        if(Time.time > _actionEndTime && ( FightState == AiFightState.LookAt || FightState == AiFightState.Close )) {
            _isFightEnter = false;
            _animator.SetTrigger("Exit");
        }
        else {
            if(_attackTarget != null) {
                float aiToPlayerDistance = Vector3.Distance(transform.position, _attackTarget.position);
                if(aiToPlayerDistance > _seeRange + 0.5f) {
                    _attackTarget = null;
                    _animator.SetBool("IsFighting", false);
                }
                else {
                    if(_animator.IsInTransition(0))
                        return;
                    if(BotHandPos.childCount == 0 && !IsThrowingThing) {
                        FightState = AiFightState.Search;
                    }
                    else {
                        if(aiToPlayerDistance < _attackRange) {
                            if(aiToPlayerDistance < _safeRange) {
                                FightState = AiFightState.Back;
                            }
                            else {
                                FightState = AiFightState.Attack;
                            }
                        }
                        else {
                            if(UnityEngine.Random.Range(0, 100) % 2 == 0) {
                                FightState = AiFightState.LookAt;
                            }
                            else {
                                FightState = AiFightState.Close;
                            }
                        }
                    }
                    FightStateChange();
                }
            }
            else {
                _animator.SetBool("IsFighting", false);

            }
        }
    }
    private void FightStateChange() {
        switch(FightState) {
            case AiFightState.Search:
                if(!_isActionInit) {
                    _isActionInit = true;
                    _agent.speed = 1;
                    _isSearchingWeapon = true;
                    Vector3 randomVector = new Vector3(UnityEngine.Random.Range(-48, 48), transform.position.y, UnityEngine.Random.Range(-48, 48)).normalized;
                    float randommVectorLength = _attackRange + _safeRange;
                    Vector3 firstRandomPos = randomVector * randommVectorLength + transform.position;
                    if(NavMesh.SamplePosition(firstRandomPos, out NavMeshHit hit, randommVectorLength - 0.5f, -1)) {
                        _destPosition = hit.position;
                    }
                }
                else {
                    if(_agent.remainingDistance < _agent.stoppingDistance) {
                        _isFightEnter = false;
                        _animator.SetTrigger("Exit");
                    }
                }
                SearchWeapons();
                break;
            case AiFightState.LookAt:
                if(!_isActionInit) {
                    _isActionInit = true;
                    _agent.speed = 0;
                }
                LookAtPlayer();
                break;
            case AiFightState.Close:
                if(!_isActionInit) {
                    _isActionInit = true;
                    _agent.speed = 0.5f;
                }
                Vector3 forwardVector = ( _attackTarget.position - transform.position ).normalized;
                float forwardVectorLength = Vector3.Distance(transform.position, _attackTarget.position) - _attackRange;
                Vector3 forwardPosition = forwardVector * forwardVectorLength + transform.position;
                _destPosition = forwardPosition;
                break;
            case AiFightState.Attack:
                if(!_isActionInit) {
                    _isActionInit = true;
                }
                if(LookAtPlayer() && BotHandPos.childCount != 0 && IsThrowingThing == false) {
                    ThrowAttack();
                }
                break;
            case AiFightState.Back:
                if(!_isActionInit) {
                    _isActionInit = true;
                    Vector3 backVector = ( transform.position - _attackTarget.position ).normalized;
                    float backVectorLength = ( _safeRange - Vector3.Distance(transform.position, _attackTarget.position) ) / 2 + _safeRange;
                    Vector3 backPosition = backVector * backVectorLength + transform.position;
                    if(NavMesh.SamplePosition(backPosition, out NavMeshHit hit, backVectorLength - 0.5f, -1)) {
                        _agent.speed = 1;
                        _destPosition = hit.position;
                    }
                }
                else {
                    if(_agent.remainingDistance < _agent.stoppingDistance) {
                        _isFightEnter = false;
                        _animator.SetTrigger("Exit");
                    }
                }
                break;
        }
    }
    public override void SearchWeapons() {
        if(_isSearchingWeapon == true) {
            Collider[] colliders = Physics.OverlapSphere(transform.position, _seeRange, 1 << LayerMask.NameToLayer("Thing"));
            if(colliders.Length != 0) {
                if(BotHandPos.childCount == 0 && colliders[0].GetComponent<IWeapon>().GetWeaponOwner() == null && IsGettingThing == false) {
                    _isSearchingWeapon = false;
                    ChangeWeaponInHand(colliders[0].transform.gameObject);
                }
            }
        }
    }
    private bool LookAtPlayer() {
        Vector3 aiToPlayerVector = _attackTarget.position - transform.position;
        Quaternion rot = Quaternion.LookRotation(aiToPlayerVector);
        Quaternion finalRotation = Quaternion.Euler(0, rot.eulerAngles.y, 0);
        transform.rotation = Quaternion.Lerp(transform.rotation, finalRotation, 0.2f);

        Vector3 v1 = Vector3.ProjectOnPlane(transform.forward, Vector3.up);
        Vector3 v2 = Vector3.ProjectOnPlane(_attackTarget.position - transform.position, Vector3.up);
        if(Vector3.Angle(v1, v2) < 0.01f) {
            return true;
        }
        else {
            return false;
        }
    }
    public override void RunDeadProcess() {
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
        Gizmos.DrawWireSphere(_destPosition, 0.2f);
    }
}