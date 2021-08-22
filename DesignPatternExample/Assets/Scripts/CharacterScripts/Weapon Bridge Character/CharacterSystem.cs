using DesignPatternExample.Character.CharacterSetting;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSystem : GameSystemAbstract
{
    private List<CharacterAbstract> _players = new List<CharacterAbstract>();
    private List<CharacterAbstract> _Enemys = new List<CharacterAbstract>();

    public CharacterSystem(GameFunction theFunction) : base(theFunction) {
        Initialize();
    }
    public override void Initialize() {
        InitCharacter();
    }
    public override void Update() {
        UpdateCharacter();
    }
    public override void FixedUpdate() {
        FixUpdateCharacter();
    }

    public void AddPlayer(PlayerAbstract thePlayer) {
        _players.Add(thePlayer);
    }
    public void RemovePlayer(PlayerAbstract thePlayer) {
        _players.Remove(thePlayer);
    }

    public void AddEnemy(EnemyAbstract theEnemy) {
        _players.Add(theEnemy);
    }
    public void RemoveEnemy(EnemyAbstract theEnemy) {
        _players.Remove(theEnemy);
    }
    public void RemoveCharacter() {
        RemoveCharacter(_players);
        RemoveCharacter(_Enemys);
    }
    public void RemoveCharacter(List<CharacterAbstract> theCharacters) {
        List<CharacterAbstract> CanRemoves = new List<CharacterAbstract>();
        foreach(CharacterAbstract character in theCharacters) {
            if(character.GetIsDead())
                continue;
            //if(character.GetIsRunDeadEvent() == true)
            if(character.GetCanRemove()) 
                CanRemoves.Add(character);
        }
        foreach(CharacterAbstract CanRemove in CanRemoves) {
            CanRemove.Release();
            theCharacters.Remove(CanRemove);
        }
    }
    private void InitCharacter() {
        foreach(CharacterAbstract character in _players)
            character.Initialize();
        foreach(CharacterAbstract character in _Enemys)
            character.Initialize();
    }
    private void UpdateCharacter() {
        foreach(CharacterAbstract character in _players)
            character.Update();
        foreach(CharacterAbstract character in _Enemys)
            character.Update();
    }
    private void FixUpdateCharacter() {
        foreach(CharacterAbstract character in _players)
            character.FixedUpdate();
        foreach(CharacterAbstract character in _Enemys)
            character.FixedUpdate();
    }
}
