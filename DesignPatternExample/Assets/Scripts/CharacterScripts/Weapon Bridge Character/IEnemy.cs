using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class IEnemy : ICharacter {
    public Transform botHand;
    private GameObject HitThing = null;
    private bool m_bThrowThing = false;

    public LayerMask player;
    public NavMeshAgent agent;
    public float seeRange = 20;
    public float attackRange = 12.5f;
    public float safekRange = 5f;
    public float seeAngle = 120;

    private Vector3 m_DestPosition;
    private Transform m_AttackTarget;
    private TPController m_TpController;
    private Animator m_Animator;

    private float m_fNoFightAcEndTime;
    private float m_fFightAcEndTime;
    private bool m_bNoFightEnterInit = false;
    private bool m_bFightEnterInit = false;
    private bool m_bActionInit = false;
    private bool m_bSearchingWeapon = false;
    private FightState FightState;

    [SerializeField] private Image _CharacterHpUi;
    //public override void Initizal() {

    //}
    //public override void Update() {

    //}
    //public override void Release() {

    //}
    void Start() {
        m_TpController = GetComponent<TPController>();
        m_Animator = GetComponent<Animator>();
        agent.updatePosition = true;
        agent.updateRotation = false;
        CharacterHp = 100f;
    }
    void FixedUpdate() {
        if(CharacterHp != 0) {
            agent.SetDestination(m_DestPosition);
            if(agent.remainingDistance > agent.stoppingDistance) {
                m_TpController.Move(agent.desiredVelocity, false);
            }
            else {
                m_TpController.Move(Vector3.zero, false);
            }
            if(m_Animator.GetBool("IsFighting") == false) {
                NoFightStateUpdate();
            }
            if(m_Animator.GetBool("IsFighting") == true) {
                FightStateUpdate();
            }
            SearchWeapons();
        }
        else {
            Dead();
        }
        _CharacterHpUi.fillAmount = Mathf.Lerp(_CharacterHpUi.fillAmount, CharacterHp / 100f, 0.1f);
        Debug.Log(FightState);
        //if(m_Animator.GetBool("Dead"))
        //    agent.speed = 0;
    }
    public override void Attack() {
        ThrowAttack();
    }
    public override void UnderAttack(ICharacter theAttacker) {
        throw new System.NotImplementedException();
    }
    public override void SearchWeapons() {
        if(m_bSearchingWeapon == true) {
            Collider[] colliders = Physics.OverlapSphere(transform.position, seeRange, 1 << LayerMask.NameToLayer("Thing"));
            if(colliders.Length != 0) {
                if(botHand.childCount == 0 && colliders[0].GetComponent<IWeapon>().GetWeaponOwner() == null) {
                    HitThing = colliders[0].transform.gameObject;

                    SetWeapon(HitThing.GetComponent<IWeapon>());

                    HitThing.GetComponent<Rigidbody>().isKinematic = true;
                    HitThing.GetComponent<Collider>().isTrigger = true;
                    HitThing.transform.SetParent(botHand);
                    HitThing.transform.localPosition = Vector3.zero;
                    HitThing.transform.localRotation = Quaternion.identity;
                    m_bSearchingWeapon = false;
                }
            }
        }
    }
    public override void ThrowAttack() {
        if(botHand.childCount != 0 && m_bThrowThing == false) {
            m_bThrowThing = true;
            gameObject.GetComponent<Animator>().SetTrigger("Throw");
        }
    }
    public void EnemyThrow() {
        HitThing.transform.SetParent(null);
        HitThing.GetComponent<Rigidbody>().isKinematic = false;
        HitThing.GetComponent<Collider>().isTrigger = false;
        HitThing.GetComponent<Rigidbody>().AddForce(gameObject.transform.forward * 1000);
        HitThing = null;
        if(!m_Animator.GetCurrentAnimatorStateInfo(0).IsName("Enemy Throwing")) {
            m_bThrowThing = false;
        }
    }
    public void NoFightStateUpdate() {
        if(!m_bNoFightEnterInit) {
            m_bNoFightEnterInit = true;
            m_fNoFightAcEndTime = Time.time + 3;
            if(UnityEngine.Random.Range(0, 100) % 2 == 0) {
                agent.speed = 0;
            }
            else {
                agent.speed = 0.5f;
                m_DestPosition = Vector3.ProjectOnPlane(UnityEngine.Random.onUnitSphere, Vector3.up) * 10 + transform.position;
            }
        }
        if(Time.time > m_fNoFightAcEndTime) {
            m_bNoFightEnterInit = false;
            m_Animator.SetTrigger("Exit");
        }
        else {
            Collider[] colliders = Physics.OverlapSphere(transform.position, seeRange, player);
            for(int i = 0; i < colliders.Length; i++) {
                if(Vector3.Angle(transform.forward, colliders[i].transform.position - transform.position) < seeAngle / 2) {
                    m_AttackTarget = colliders[0].transform;
                    m_Animator.SetBool("IsFighting", true);
                    break;
                }
                else {
                    if(colliders[i].GetComponent<Animator>().GetFloat("Speed") > 0.75f) {
                        m_AttackTarget = colliders[0].transform;
                        m_Animator.SetBool("IsFighting", true);
                        break;
                    }
                }
            }
        }
    }
    public void FightStateUpdate() {
        if(!m_bFightEnterInit) {
            m_bFightEnterInit = true;
            m_fFightAcEndTime = Time.time + 1.5f;
            m_bActionInit = false;
        }
        if(Time.time > m_fFightAcEndTime && ( FightState == FightState.LookAt || FightState == FightState.Close )) {
            m_bFightEnterInit = false;
            m_Animator.SetTrigger("Exit");
        }
        else {
            if(m_AttackTarget != null) {
                float aiToPlayerDistance = Vector3.Distance(transform.position, m_AttackTarget.position);
                if(aiToPlayerDistance > seeRange + 0.5f) {
                    m_AttackTarget = null;
                    m_Animator.SetBool("IsFighting", false);
                }
                else {
                    if(m_Animator.IsInTransition(0))
                        return;
                    if(botHand.childCount == 0) {
                        FightState = FightState.Search;
                    }
                    else {
                        if(aiToPlayerDistance < attackRange) {
                            if(aiToPlayerDistance < safekRange) {
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
                    switch(FightState) {
                        case FightState.Search:
                            if(!m_bActionInit) {
                                m_bActionInit = true;
                                Debug.Log(m_bActionInit);
                                Vector3 randomVector = new Vector3(UnityEngine.Random.Range(-48, 48), transform.position.y, UnityEngine.Random.Range(-48, 48)).normalized;
                                float vectorLength = attackRange + safekRange;
                                Vector3 backPosition = randomVector * vectorLength + transform.position;
                                agent.speed = 0;
                                if(NavMesh.SamplePosition(backPosition, out NavMeshHit hit, vectorLength - 0.5f, -1)) {
                                    m_DestPosition = hit.position;
                                    agent.speed = 1;
                                    m_bSearchingWeapon = true;
                                }
                                else {
                                    Vector3 randomPosition = new Vector3(UnityEngine.Random.Range(-48.0f, 48.0f), transform.position.y, UnityEngine.Random.Range(-48.0f, 48.0f));
                                    m_DestPosition = randomPosition;
                                    agent.speed = 1;
                                    m_bSearchingWeapon = false;
                                }
                            }
                            else {
                                if(agent.remainingDistance < agent.stoppingDistance) {
                                    m_bFightEnterInit = false;
                                    m_Animator.SetTrigger("Exit");
                                }
                            }
                            break;
                        case FightState.LookAt:
                            if(!m_bActionInit) {
                                m_bActionInit = true;
                                agent.speed = 0;
                            }
                            LookAtPlayer();
                            break;
                        case FightState.Close:
                            if(!m_bActionInit) {
                                m_bActionInit = true;
                                agent.speed = 0.5f;
                            }
                            Vector3 forwardVector = (m_AttackTarget.position - transform.position).normalized;
                            float forwardVectorLength = ( attackRange - safekRange ) / 2 + safekRange;
                            Vector3 forwardPosition = forwardVector * forwardVectorLength + transform.position;
                            m_DestPosition = forwardPosition;
                            break;
                        case FightState.Attack:
                            if(!m_bActionInit) {
                                m_bActionInit = true;
                                agent.speed = 0;
                            }
                            if(LookAtPlayer()) {
                                ThrowAttack();
                            }
                            break;
                        case FightState.Back:
                            if(!m_bActionInit) {
                                m_bActionInit = true;
                                Vector3 backVector = ( transform.position - m_AttackTarget.position ).normalized;
                                float vectorLength = ( attackRange - safekRange ) / 2 + safekRange;
                                Vector3 backPosition = backVector * vectorLength + transform.position;
                                agent.speed = 0;
                                if(NavMesh.SamplePosition(backPosition, out NavMeshHit hit, vectorLength - 0.5f, -1)) {
                                    m_DestPosition = hit.position;
                                    agent.speed = 1;
                                }
                                else {
                                    m_DestPosition = m_AttackTarget.position;
                                    FightState = FightState.Attack;
                                    m_bActionInit = false;
                                }
                            }
                            else {
                                if(agent.remainingDistance < agent.stoppingDistance) {
                                    m_bFightEnterInit = false;
                                    m_Animator.SetTrigger("Exit");
                                }
                            }
                            break;
                    }
                }
            }
        }
    }
    private bool LookAtPlayer() {
        Vector3 aiToPlayerVector = m_AttackTarget.position - transform.position;
        Quaternion rot = Quaternion.LookRotation(aiToPlayerVector);
        Quaternion finalRotation = Quaternion.Euler(0, rot.eulerAngles.y, 0);
        transform.rotation = Quaternion.Lerp(transform.rotation, finalRotation, 0.1f);

        Vector3 v1 = Vector3.ProjectOnPlane(transform.forward, Vector3.up);
        Vector3 v2 = Vector3.ProjectOnPlane(m_AttackTarget.position - transform.position, Vector3.up);
        if(Vector3.Angle(v1, v2) < 0.01f) {
            return true;
        }
        else {
            return false;
        }
    }
    public override void Dead() {
        gameObject.GetComponent<Animator>().SetTrigger("Dead");

    }
    private void OnDrawGizmos() {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, seeRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, safekRange);

        Gizmos.color = Color.white;
        Vector3 p1 = Quaternion.Euler(0, seeAngle / 2, 0) * transform.forward * seeRange + transform.position;
        Vector3 p2 = Quaternion.Euler(0, -seeAngle / 2, 0) * transform.forward * seeRange + transform.position;
        Gizmos.DrawLine(transform.position, p1);
        Gizmos.DrawLine(transform.position, p2);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(m_DestPosition, 0.2f);
    }
}
public enum FightState { LookAt, Close, Attack, Back, Search }