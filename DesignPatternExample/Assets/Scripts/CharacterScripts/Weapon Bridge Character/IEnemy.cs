using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class IEnemy : ICharacter {
    [SerializeField] private Transform _botHandPos;
    [SerializeField] private LayerMask _player;
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private Image _CharacterHpUi;

    private float _seeRange = 20;
    private float _attackRange = 12f;
    private float _safeRange = 5f;
    private float _seeAngle = 120;

    private Vector3 _destPosition;
    private Transform _attackTarget;
    private TPController _tpController;
    private Animator _animator;

    private GameObject _hitThing = null;
    private float _actionEndTime;
    private bool _isThrowingThing = false;
    private bool _iNoFightEnter = false;
    private bool _isFightEnter = false;
    private bool _isActionInit = false;
    private bool _isSearchingWeapon = false;
    private bool _isdead = false;
    private FightState FightState;

    //public override void Initizal() {

    //}
    //public override void Update() {

    //}
    //public override void Release() {

    //}
    void Start() {
        _tpController = GetComponent<TPController>();
        _animator = GetComponent<Animator>();
        _agent.updatePosition = true;
        _agent.updateRotation = false;
        CharacterHp = 100f;
        _animator.SetBool("IsFighting", false);
    }
    void FixedUpdate() {
        if(CharacterHp != 0) {
            MoveSetting();
            if(_animator.GetBool("IsFighting") == false) {
                NoFightStateUpdate();
            }
            if(_animator.GetBool("IsFighting") == true) {
                FightStateUpdate();
            }
        }
        else {
            if(_isdead == false) {
                _isdead = true;
                Dead();
            }
        }
        _CharacterHpUi.fillAmount = Mathf.Lerp(_CharacterHpUi.fillAmount, CharacterHp / 100f, 0.1f);
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
        if(Time.time > _actionEndTime && ( FightState == FightState.LookAt || FightState == FightState.Close )) {
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
                    if(_botHandPos.childCount == 0 && !_isThrowingThing) {
                        FightState = FightState.Search;
                    }
                    else {
                        if(aiToPlayerDistance < _attackRange) {
                            if(aiToPlayerDistance < _safeRange) {
                                FightState = FightState.Back;
                            }
                            else {
                                FightState = FightState.Attack;
                            }
                        }
                        else {
                            if(UnityEngine.Random.Range(0, 100) % 2 == 0) {
                                FightState = FightState.LookAt;
                            }
                            else {
                                FightState = FightState.Close;
                            }
                        }
                    }
                    FightStateChange();
                }
            }
        }
    }
    private void FightStateChange() {
        switch(FightState) {
            case FightState.Search:
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
            case FightState.LookAt:
                if(!_isActionInit) {
                    _isActionInit = true;
                    _agent.speed = 0;
                }
                LookAtPlayer();
                break;
            case FightState.Close:
                if(!_isActionInit) {
                    _isActionInit = true;
                    _agent.speed = 0.5f;
                }
                Vector3 forwardVector = ( _attackTarget.position - transform.position ).normalized;
                float forwardVectorLength = Vector3.Distance(transform.position, _attackTarget.position) - _attackRange;
                Vector3 forwardPosition = forwardVector * forwardVectorLength + transform.position;
                _destPosition = forwardPosition;
                break;
            case FightState.Attack:
                if(!_isActionInit) {
                    _isActionInit = true;
                }
                if(LookAtPlayer() && _botHandPos.childCount != 0 && _isThrowingThing == false) {
                    ThrowAttack();
                }
                break;
            case FightState.Back:
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
                if(_botHandPos.childCount == 0 && colliders[0].GetComponent<IWeapon>().GetWeaponOwner() == null) {
                    _hitThing = colliders[0].transform.gameObject;

                    SetWeapon(_hitThing.GetComponent<IWeapon>());

                    _hitThing.GetComponent<Rigidbody>().isKinematic = true;
                    _hitThing.GetComponent<Collider>().isTrigger = true;
                    _hitThing.transform.SetParent(_botHandPos);
                    _hitThing.transform.localPosition = Vector3.zero;
                    _hitThing.transform.localRotation = Quaternion.identity;
                    _isSearchingWeapon = false;
                }
            }
        }
    }
    public override void ThrowAttack() {
        _isThrowingThing = true;
        gameObject.GetComponent<Animator>().SetTrigger("Throw");
    }
    public void EnemyThrow() {
        _hitThing.transform.SetParent(null);
        _hitThing.GetComponent<Rigidbody>().isKinematic = false;
        _hitThing.GetComponent<Collider>().isTrigger = false;
        _hitThing.GetComponent<Rigidbody>().AddForce(gameObject.transform.forward * 1000);
        _hitThing.GetComponent<IWeapon>().ChangeThrowState();
        _hitThing = null;
        _isThrowingThing = false;
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
    public override void Dead() {
        gameObject.GetComponent<Animator>().SetTrigger("Dead");
        _agent.speed = 0;
    }
    public override void Attack() {
        ThrowAttack();
    }
    public override void UnderAttack(ICharacter theAttacker) {
        throw new System.NotImplementedException();
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
public enum FightState { LookAt, Close, Attack, Back, Search }