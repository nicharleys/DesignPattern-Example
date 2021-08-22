using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttrFactory : AttrFactoryAbstruct
{
    private Dictionary<int, CharacterBaseAttr> _playerAttrCollection;
    private Dictionary<int, EnemyBaseAttr> _enemyAttrCollection;
    private Dictionary<int, WeaponAttr> _weaponAttrCollection;
    public AttrFactory() {
        InitPlayerAttr();
        InitEnemyAttr();
        InitWeaponAttr();
    }
    public void InitPlayerAttr() {
        _playerAttrCollection = new Dictionary<int, CharacterBaseAttr>();
        _playerAttrCollection.Add(1, new CharacterBaseAttr(30, "玩家等級A"));
        _playerAttrCollection.Add(2, new CharacterBaseAttr(60, "玩家等級B"));
        _playerAttrCollection.Add(3, new CharacterBaseAttr(90, "玩家等級C"));
    }
    public void InitEnemyAttr() {
        _enemyAttrCollection = new Dictionary<int, EnemyBaseAttr>();
        _enemyAttrCollection.Add(1, new EnemyBaseAttr(15, 30, "敵人等級A"));
        _enemyAttrCollection.Add(2, new EnemyBaseAttr(20, 50, "敵人等級B"));
        _enemyAttrCollection.Add(3, new EnemyBaseAttr(30, 100, "敵人等級C"));
    }
    public void InitWeaponAttr() {
        _weaponAttrCollection = new Dictionary<int, WeaponAttr>();
        _weaponAttrCollection.Add(1, new WeaponAttr(5, "WeaponCapsule"));
        _weaponAttrCollection.Add(2, new WeaponAttr(10, "WeaponShpere"));
        _weaponAttrCollection.Add(3, new WeaponAttr(15, "WeaponCube"));

    }
    public override PlayerAttr GetPlayerAttr(int attrID) {
        if(_playerAttrCollection.ContainsKey(attrID) == false) {
            Debug.LogWarning("GetPlayerAttr:AttrID["+ attrID +"]數值不存在");
            return null;
        }
        PlayerAttr newAttr = new PlayerAttr();
        newAttr.SetPlayerAttr(_playerAttrCollection[attrID]);
        return newAttr;
    }

    public override EnemyAttr GetEnemyAttr(int attrID) {
        if(_enemyAttrCollection.ContainsKey(attrID) == false) {
            Debug.LogWarning("GetEnemyAttr:AttrID[" + attrID + "]數值不存在");
            return null;
        }
        EnemyAttr newAttr = new EnemyAttr();
        newAttr.SetEnemyAttr(_enemyAttrCollection[attrID]);
        return newAttr;
    }
    public override WeaponAttr GetWeaponAttr(int attrID) {
        if(_weaponAttrCollection.ContainsKey(attrID) == false) {
            Debug.LogWarning("GetWeaponAttr:AttrID[" + attrID + "]數值不存在");
            return null;
        }
        //WeaponAttr沒有外部參數，因此直接回傳內部參數
        return _weaponAttrCollection[attrID];
    }
}
