using UnityEngine;
using DesignPatternExample.CharacterAI.StatePattern;
using DesignPatternExample.Character.CharacterSetting;
public abstract class IAIState
{
    protected float ActionEndTime;
    protected bool IsActionInit = false;

    protected float SeeRange = 20;
    protected float AttackRange = 12f;
    protected float SafeRange = 5f;

    protected bool IsInited = false;

    protected ICharacterAI CharacterAI = null;

    public IAIState(){}
    public void SetCharacterAI(ICharacterAI theCharacterAI) {
        CharacterAI = theCharacterAI;
    }
    public void Update(ICharacter theCharacter) {
        OnceInit();
        TimeLoopAction(theCharacter);
    }
    private void OnceInit() {
        if(!IsInited) {
            IsInited = true;
            Initialize();
        }
    }
    private void TimeLoopAction(ICharacter theCharacter) {
        CheckAttackTargetHp(theCharacter);
        if(!CharacterAI.IsStateEnter) {
            CharacterAI.IsStateEnter = true;
            ActionEndTime = Time.time + 1.5f;
            InLoopExecute(theCharacter);
        }
        if(Time.time > ActionEndTime) {
            CharacterAI.IsStateEnter = false;
            CharacterAI.Anim.SetTrigger("Exit");
        }
        else {
            InTimeAction(theCharacter);
        }
    }
    protected void ChangeAIState(IAIState theAIState) {
        CharacterAI.ChangeAIState(theAIState);
        CharacterAI.IsStateEnter = false;
    }
    private void CheckAttackTargetHp(ICharacter theCharacter) {
        if(CharacterAI.AttackTarget == null) {
            if(theCharacter.GetWeapon() != null)
                theCharacter.ChangeWeaponOutHand();
            return;
        }
        if(CharacterAI.AttackTarget.GetComponent<ICharacter>().GetAttribute().GetNowHp() <= 0) {
            CharacterAI.AttackTarget = null;
            ChangeAIState(new MoveAIState());
        }
    }
    protected abstract void Initialize();
    protected abstract void InLoopExecute(ICharacter theCharacter);
    protected abstract void InTimeAction(ICharacter theCharacter);
}
