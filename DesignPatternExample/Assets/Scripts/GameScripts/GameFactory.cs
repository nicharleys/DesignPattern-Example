using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameFactory
{
    private static CharacterFactoryAbstract _characterFactery = null;

    public static CharacterFactoryAbstract GetCharacterFactery() {
        if(_characterFactery == null) {
            _characterFactery = new CharacterFactory();
        }
        return _characterFactery;
    }
}
