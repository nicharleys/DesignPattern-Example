using DesignPatternExample.Character.CharacterSetting;
using DesignPatternExample.CharacterAI.StatePattern;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : ICharacterAI {
    public EnemyAI(ICharacter theCharacter, NavMeshAgent theAgent, TPController theTpCtr, Animator theAnim) : base(theCharacter) {
        Agent = theAgent;
        TpCtr = theTpCtr;
        Anim = theAnim;
        ChangeAIState(new MoveAIState());
    }
}
