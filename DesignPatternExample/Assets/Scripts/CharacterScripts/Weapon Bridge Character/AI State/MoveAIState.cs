using DesignPatternExample.Character.CharacterSetting;
using UnityEngine;

public class MoveAIState : IAIState {
    private float _seeAngle = 120;
    private float _lastHp;
    public MoveAIState() {
        IsInited = false;
    }
    protected override void Initialize() {
        CharacterAI.Anim.SetBool("IsFighting", false);
    }
    protected override void InLoopExecute(ICharacter theCharacter) {
        _lastHp = theCharacter.GetAttribute().GetNowHp();
        if(Random.Range(0, 100) % 2 == 0) {
            CharacterAI.Agent.speed = 0;
        }
        else {
            CharacterAI.Agent.speed = 0.5f;
            CharacterAI.DestPosition = Vector3.ProjectOnPlane(Random.onUnitSphere, Vector3.up) * 10 + theCharacter.transform.position;
        }
    }
    protected override void InTimeAction(ICharacter theCharacter) {
        Collider[] colliders = Physics.OverlapSphere(theCharacter.transform.position, SeeRange, 1 << LayerMask.NameToLayer("Player"));
        if(_lastHp != theCharacter.GetAttribute().GetNowHp()) {
            CharacterAI.AttackTarget = colliders[0];
            ActionToGetThing(theCharacter);
        }
        for(int i = 0; i < colliders.Length; i++) {
            if(Vector3.Angle(theCharacter.transform.forward, colliders[i].transform.position - theCharacter.transform.position) < _seeAngle / 2) {
                CharacterAI.AttackTarget = colliders[0];
                ActionToGetThing(theCharacter);
            }
        }
    }
    private void ActionToGetThing(ICharacter theCharacter) {
        if(theCharacter.IsGettingThing == false) {
            ChangeAIState(new SearchAIState());
        }
        else {
            ChangeAIState(new CloseAIState());
        }
    }
    public override void RemoveTarget(ICharacter theCharacter) {
    }
}
