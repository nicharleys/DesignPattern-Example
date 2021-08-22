using UnityEngine;
using DesignPatternExample.CharacterAI.StatePattern;
using DesignPatternExample.Character.CharacterSetting;
public abstract class AIStateAbstract
{
    protected float ActionEndTime;
    protected bool IsActionInit = false;

    protected float SeeRange = 20;
    protected float AttackRange = 12f;
    protected float SafeRange = 5f;

    protected bool IsInited = false;

    protected CharacterAIAbstract CharacterAI = null;

    public AIStateAbstract(){}
    public void SetCharacterAI(CharacterAIAbstract theCharacterAI) {
        CharacterAI = theCharacterAI;
    }
    public void Update(CharacterAbstract theCharacter) {
        OnceInit();
        TimeLoopAction(theCharacter);
    }
    private void OnceInit() {
        if(!IsInited) {
            IsInited = true;
            Initialize();
        }
    }
    private void TimeLoopAction(CharacterAbstract theCharacter) {
        CheckAttackTargetHp(theCharacter);
        if(!CharacterAI.IsStateEnter) {
            CharacterAI.IsStateEnter = true;
            ActionEndTime = Time.time + 1.5f;
            InLoopExecute(theCharacter);
        }
        if(Time.time > ActionEndTime) {
            CharacterAI.IsStateEnter = false;
            CharacterAI.Character.Anim.SetTrigger("Exit");
        }
        else {
            InTimeAction(theCharacter);
        }
    }
    protected void ChangeAIState(AIStateAbstract theAIState) {
        CharacterAI.ChangeAIState(theAIState);
        CharacterAI.IsStateEnter = false;
    }
    private void CheckAttackTargetHp(CharacterAbstract theCharacter) {
        if(CharacterAI.AttackTarget == null) {
            if(theCharacter.GetWeapon() != null)
                theCharacter.ChangeWeaponOutHand();
            return;
        }
        if(CharacterAI.AttackTarget.GetComponent<CharacterAbstract>().GetAttribute().GetNowHp() <= 0) {
            CharacterAI.AttackTarget = null;
            ChangeAIState(new MoveAIState());
        }
    }
    protected abstract void Initialize();
    protected abstract void InLoopExecute(CharacterAbstract theCharacter);
    protected abstract void InTimeAction(CharacterAbstract theCharacter);
}
