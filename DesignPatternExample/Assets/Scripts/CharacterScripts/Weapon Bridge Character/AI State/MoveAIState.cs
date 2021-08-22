using DesignPatternExample.Character.CharacterSetting;
using UnityEngine;

public class MoveAIState : AIStateAbstract {
    private float _seeAngle = 120;
    private float _lastHp;
    public MoveAIState() {
        IsInited = false;
    }
    protected override void Initialize() {
        CharacterAI.Character.Anim.SetBool("IsFighting", false);
    }
    protected override void InLoopExecute(CharacterAbstract theCharacter) {
        _lastHp = theCharacter.GetAttribute().GetNowHp();
        if(Random.Range(0, 100) % 2 == 0) {
            CharacterAI.Character.Agent.speed = 0;
        }
        else {
            CharacterAI.Character.Agent.speed = 0.5f;
            CharacterAI.DestPosition = Vector3.ProjectOnPlane(Random.onUnitSphere, Vector3.up) * 10 + theCharacter.Character.transform.position;
        }
    }
    protected override void InTimeAction(CharacterAbstract theCharacter) {
        Collider[] colliders = Physics.OverlapSphere(theCharacter.Character.transform.position, SeeRange, 1 << LayerMask.NameToLayer("Player"));
        if(_lastHp != theCharacter.GetAttribute().GetNowHp()) {
            SearchAlivePlayer(colliders, theCharacter);
        }
        SearchAlivePlayer(colliders, theCharacter);
    }
    private void SearchAlivePlayer(Collider[] theColliders, CharacterAbstract theCharacter) {
        for(int i = 0; i < theColliders.Length; i++) {
            if(Vector3.Angle(theCharacter.Character.transform.forward, theColliders[i].transform.position - theCharacter.Character.transform.position) < _seeAngle / 2 && theColliders[i].GetComponent<CharacterAbstract>().GetAttribute().GetNowHp() > 0) {
                CharacterAI.AttackTarget = theColliders[i];
                ActionToGetThing(theCharacter);
            }
        }
    }
    private void ActionToGetThing(CharacterAbstract theCharacter) {
        if(theCharacter.IsGettingThing == false) {
            ChangeAIState(new SearchAIState());
        }
        else {
            ChangeAIState(new CloseAIState());
        }
    }
}
