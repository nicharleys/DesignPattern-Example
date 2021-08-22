using DesignPatternExample.Character.CharacterSetting;
using DesignPatternExample.CharacterAI.StatePattern;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : CharacterAIAbstract {
    public EnemyAI(CharacterAbstract theCharacter) : base(theCharacter) {
        ChangeAIState(new MoveAIState());
    }
}
