using DesignPatternExample.Character.CharacterSetting;
using UnityEngine;
using UnityEngine.AI;

namespace DesignPatternExample.CharacterAI.StatePattern {
    public abstract class CharacterAIAbstract {
        protected AIStateAbstract AIState = null;
        internal CharacterAbstract Character { get; set; }
        internal Vector3 DestPosition { get; set; }
        internal Collider AttackTarget { get; set; }
        internal bool IsStateEnter { get; set; }

        internal CharacterAIAbstract(CharacterAbstract theCharacter) {
            Character = theCharacter;
        }
        internal virtual void ChangeAIState(AIStateAbstract theAIState) {
            AIState = theAIState;
            AIState.SetCharacterAI(this);
        }
        internal void Update() {
            Character.Agent.SetDestination(DestPosition);
            if(!Character.Agent.pathPending && Character.Agent.remainingDistance < Character.Agent.stoppingDistance) {
                Character.TpCtr.Move(Vector3.zero, false);
            }
            else {
                Character.TpCtr.Move(Character.Agent.desiredVelocity, false);
            }
            AIState.Update(Character);
        }
    }
}