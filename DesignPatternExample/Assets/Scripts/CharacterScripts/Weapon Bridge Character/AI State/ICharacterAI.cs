using DesignPatternExample.Character.CharacterSetting;
using UnityEngine;
using UnityEngine.AI;

namespace DesignPatternExample.CharacterAI.StatePattern {
    public abstract class ICharacterAI {
        protected IAIState AIState = null;

        protected internal ICharacter Character { get; set; }
        protected internal NavMeshAgent Agent { get; set; }
        protected internal TPController TpCtr { get; set; }
        protected internal Animator Anim { get; set; }

        protected internal Vector3 DestPosition { get; set; }
        protected internal Collider AttackTarget { get; set; }
        protected internal bool IsStateEnter { get; set; }

        internal ICharacterAI(ICharacter theCharacter) {
            Character = theCharacter;
        }
        internal virtual void ChangeAIState(IAIState theAIState) {
            AIState = theAIState;
            AIState.SetCharacterAI(this);
        }
        internal void Update() {
            Agent.SetDestination(DestPosition);
            if(!Agent.pathPending && Agent.remainingDistance < Agent.stoppingDistance) {
                TpCtr.Move(Vector3.zero, false);
            }
            else {
                TpCtr.Move(Agent.desiredVelocity, false);
            }
            AIState.Update(Character);
        }
    }
}