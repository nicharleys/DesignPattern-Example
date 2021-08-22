using DesignPatternExample.Character.CharacterSetting;
using UnityEngine;
using UnityEngine.AI;
public class AttackAIState : AIStateAbstract {
    private bool _isAttacking = false;
    public AttackAIState() {
        IsInited = false;
    }
    protected override void Initialize() {
        CharacterAI.Character.Anim.SetBool("IsFighting", true);
    }
    protected override void InLoopExecute(CharacterAbstract theCharacter) {
        IsActionInit = false;
    }
    protected override void InTimeAction(CharacterAbstract theCharacter) {
        if(CharacterAI.AttackTarget == null) {
            ChangeAIState(new MoveAIState());
            return;
        }
        float aiToPlayerDistance = Vector3.Distance(theCharacter.Character.transform.position, CharacterAI.AttackTarget.transform.position);
        if(aiToPlayerDistance > AttackRange + 0.5f) {
            CharacterAI.AttackTarget = null;
            ChangeAIState(new CloseAIState());
        }
        else {
            if(CharacterAI.Character.Anim.IsInTransition(0))
                return;
            SafeRangeAction(aiToPlayerDistance, theCharacter);
        }
    }
    private void SafeRangeAction(float aiToPlayerDistance, CharacterAbstract theCharacter) {
        if(!theCharacter.IsGettingThing) {
            ChangeAIState(new SearchAIState());
            return;
        }
        if(!theCharacter.IsThrowingThing && theCharacter.BotHandPos.childCount != 0){
            if(aiToPlayerDistance < SafeRange) {
                BackTo(theCharacter);
            }
            else {
                _isAttacking = true;
                Attack(theCharacter);
            }
        }
    }
    private void BackTo(CharacterAbstract theCharacter) {
        if(!IsActionInit) {
            IsActionInit = true;
            CharacterAI.Character.Agent.speed = 1;
            Vector3 backVector = ( theCharacter.Character.transform.position - CharacterAI.AttackTarget.transform.position ).normalized;
            float backVectorLength = ( SafeRange - Vector3.Distance(theCharacter.Character.transform.position, CharacterAI.AttackTarget.transform.position) ) / 2 + SafeRange;
            Vector3 backPosition = backVector * backVectorLength + theCharacter.Character.transform.position;
            if(NavMesh.SamplePosition(backPosition, out NavMeshHit hit, backVectorLength - 0.5f, -1)) {
                CharacterAI.DestPosition = hit.position;
            }
        }
        else {
            if(CharacterAI.Character.Agent.remainingDistance < CharacterAI.Character.Agent.stoppingDistance) {
                CharacterAI.IsStateEnter = false;
                CharacterAI.Character.Anim.SetTrigger("Exit");
            }
        }

    }
    private void Attack(CharacterAbstract theCharacter) {
        if(!IsActionInit) {
            IsActionInit = true;
            CharacterAI.Character.Agent.speed = 0;
        }
        if(LookAt(theCharacter) && theCharacter.IsThrowingThing == false && _isAttacking == true) {
            _isAttacking = false;
            theCharacter.ThrowAttack();
        }
    }
    private bool LookAt(CharacterAbstract theCharacter) {
        Vector3 aiToPlayerVector = CharacterAI.AttackTarget.transform.position - theCharacter.Character.transform.position;
        Quaternion lookRot = Quaternion.LookRotation(aiToPlayerVector);
        Quaternion finalRotation = Quaternion.Euler(0, lookRot.eulerAngles.y, 0);
        theCharacter.Character.transform.rotation = Quaternion.Lerp(theCharacter.Character.transform.rotation, finalRotation, 0.2f);

        Vector3 aiVector = Vector3.ProjectOnPlane(theCharacter.Character.transform.forward, Vector3.up);
        Vector3 targetVector = Vector3.ProjectOnPlane(CharacterAI.AttackTarget.transform.position - theCharacter.Character.transform.position, Vector3.up);
        return Vector3.Angle(aiVector, targetVector) < 0.01f && true;
    }
}
