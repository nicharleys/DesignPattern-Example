using DesignPatternExample.Character.CharacterSetting;
using UnityEngine;
public class CloseAIState : AIStateAbstract {
    public CloseAIState() {
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
        if(aiToPlayerDistance > SeeRange + 0.5f) {
            CharacterAI.AttackTarget = null;
            ChangeAIState(new MoveAIState());
        }
        else {
            if(CharacterAI.Character.Anim.IsInTransition(0))
                return;
            ActionInRange(aiToPlayerDistance, theCharacter);
        }
    }
    private void ActionInRange(float aiToPlayerDistance, CharacterAbstract theCharacter) {
        if(aiToPlayerDistance >= AttackRange) {
            RandomAction(theCharacter);
        }
        else {
            ChangeAIState(new AttackAIState());
        }
    }
    private void RandomAction(CharacterAbstract theCharacter) {
        if(Random.Range(0, 100) % 2 == 0) {
            SetAgentSpeed(0f);
            LookAt(theCharacter);
        }
        else {
            SetAgentSpeed(0.5f);
            CloseTo(theCharacter);
        }
    }
    private void SetAgentSpeed(float theSpeed) {
        if(!IsActionInit) {
            IsActionInit = true;
            CharacterAI.Character.Agent.speed = theSpeed;
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
    private void CloseTo(CharacterAbstract theCharacter) {
        Vector3 forwardVector = ( CharacterAI.AttackTarget.transform.position - theCharacter.Character.transform.position ).normalized;
        float forwardVectorLength = Vector3.Distance(theCharacter.Character.transform.position, CharacterAI.AttackTarget.transform.position) - SafeRange;
        Vector3 forwardPosition = forwardVector * forwardVectorLength + theCharacter.Character.transform.position;
        CharacterAI.DestPosition = forwardPosition;
    }
}
