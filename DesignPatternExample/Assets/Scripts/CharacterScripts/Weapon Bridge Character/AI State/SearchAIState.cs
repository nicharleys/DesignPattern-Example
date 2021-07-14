using DesignPatternExample.Character.CharacterSetting;
using UnityEngine;
using UnityEngine.AI;

public class SearchAIState : IAIState {
    private bool _isStartSearch = false;
    public SearchAIState() {
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
        if(aiToPlayerDistance > SeeRange + 0.5f) {
            CharacterAI.AttackTarget = null;
            CharacterAI.ChangeAIState(new MoveAIState());
        }
        else {
            if(CharacterAI.Anim.IsInTransition(0))
                return;
            if(theCharacter.BotHandPos.childCount == 0 && !theCharacter.IsThrowingThing) {
                RandomWalk(theCharacter);
                SearchWeapon(theCharacter);
            }
        }
    }
    private void RandomWalk(ICharacter theCharacter) {
        if(!IsActionInit) {
            IsActionInit = true;
            CharacterAI.Agent.speed = 1;
            _isStartSearch = true;
            Vector3 randomVector = new Vector3(Random.Range(-48, 48), theCharacter.transform.position.y, Random.Range(-48, 48)).normalized;
            float randommVectorLength = AttackRange + SafeRange;
            Vector3 firstRandomPos = randomVector * randommVectorLength + theCharacter.transform.position;
            if(NavMesh.SamplePosition(firstRandomPos, out NavMeshHit hit, randommVectorLength - 0.5f, -1)) {
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
    private void SearchWeapon(ICharacter theCharacter) {
        if(_isStartSearch == true) {
            Collider[] colliders = Physics.OverlapSphere(theCharacter.transform.position, SeeRange, 1 << LayerMask.NameToLayer("Thing"));
            if(colliders.Length != 0) {
                if(colliders[0].GetComponent<IWeapon>().GetWeaponOwner() == null && theCharacter.IsGettingThing == false) {
                    _isStartSearch = false;
                    theCharacter.ChangeWeaponInHand(colliders[0].transform.gameObject);
                    ChangeAIState(new CloseAIState());
                }
            }
        }
    }
    public override void RemoveTarget(ICharacter theCharacter) {
        throw new System.NotImplementedException();
    }
}
