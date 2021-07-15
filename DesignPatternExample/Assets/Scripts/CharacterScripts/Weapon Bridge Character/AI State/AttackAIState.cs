using DesignPatternExample.Character.CharacterSetting;
using UnityEngine;
using UnityEngine.AI;
public class AttackAIState : IAIState {
    private bool _isAttacking = false;
    public AttackAIState() {
        IsInited = false;
    }
    protected override void Initialize() {
        CharacterAI.Anim.SetBool("IsFighting", true);
    }
    protected override void InLoopExecute(ICharacter theCharacter) {
        IsActionInit = false;
    }
    protected override void InTimeAction(ICharacter theCharacter) {
        if(CharacterAI.AttackTarget == null) {
            ChangeAIState(new MoveAIState());
            return;
        }
        float aiToPlayerDistance = Vector3.Distance(theCharacter.transform.position, CharacterAI.AttackTarget.transform.position);
        if(aiToPlayerDistance > AttackRange + 0.5f) {
            CharacterAI.AttackTarget = null;
            ChangeAIState(new CloseAIState());
        }
        else {
            if(CharacterAI.Anim.IsInTransition(0))
                return;
            SafeRangeAction(aiToPlayerDistance, theCharacter);
        }
    }
    private void SafeRangeAction(float aiToPlayerDistance, ICharacter theCharacter) {
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
    private void BackTo(ICharacter theCharacter) {
        if(!IsActionInit) {
            IsActionInit = true;
            CharacterAI.Agent.speed = 1;
            Vector3 backVector = ( theCharacter.transform.position - CharacterAI.AttackTarget.transform.position ).normalized;
            float backVectorLength = ( SafeRange - Vector3.Distance(theCharacter.transform.position, CharacterAI.AttackTarget.transform.position) ) / 2 + SafeRange;
            Vector3 backPosition = backVector * backVectorLength + theCharacter.transform.position;
            if(NavMesh.SamplePosition(backPosition, out NavMeshHit hit, backVectorLength - 0.5f, -1)) {
                CharacterAI.DestPosition = hit.position;
            }
        }
        else {
            if(CharacterAI.Agent.remainingDistance < CharacterAI.Agent.stoppingDistance) {
                CharacterAI.IsStateEnter = false;
                CharacterAI.Anim.SetTrigger("Exit");
            }
        }

    }
    private void Attack(ICharacter theCharacter) {
        if(!IsActionInit) {
            IsActionInit = true;
            CharacterAI.Agent.speed = 0;
        }
        if(LookAt(theCharacter) && theCharacter.IsThrowingThing == false && _isAttacking == true) {
            _isAttacking = false;
            theCharacter.ThrowAttack();
        }
    }
    private bool LookAt(ICharacter theCharacter) {
        Vector3 aiToPlayerVector = CharacterAI.AttackTarget.transform.position - theCharacter.transform.position;
        Quaternion lookRot = Quaternion.LookRotation(aiToPlayerVector);
        Quaternion finalRotation = Quaternion.Euler(0, lookRot.eulerAngles.y, 0);
        theCharacter.transform.rotation = Quaternion.Lerp(theCharacter.transform.rotation, finalRotation, 0.2f);

        Vector3 aiVector = Vector3.ProjectOnPlane(theCharacter.transform.forward, Vector3.up);
        Vector3 targetVector = Vector3.ProjectOnPlane(CharacterAI.AttackTarget.transform.position - theCharacter.transform.position, Vector3.up);
        return Vector3.Angle(aiVector, targetVector) < 0.01f && true;
    }
}
