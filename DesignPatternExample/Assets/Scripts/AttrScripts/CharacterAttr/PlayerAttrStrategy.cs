using UnityEngine;

public class PlayerAttrStrategy : AttrStrategyAbstract
{
    public override void Initialize(CharacterAttrAbstract theCharacterAttr) {
        PlayerAttr playerAttr = theCharacterAttr as PlayerAttr;
        if(playerAttr == null) {
            Debug.LogError("Check your PlayerAttr.SetAttStrategy is placed PlayerAttrStrategy");
            return;
        }
        int extraMaxHp = 0;
        int playerLv = playerAttr.GetPlayerLv();
        if(playerLv > 0)
            extraMaxHp = ( playerLv - 1 ) * 2;
        playerAttr.AddExtraMaxHP(extraMaxHp);
    }
    public override float GetAtkPlusValue(CharacterAttrAbstract theCharacterAttr) {
        return 0;
    }
    public override float GetDmgDescValue(CharacterAttrAbstract theCharacterAttr) {
        PlayerAttr playerAttr = theCharacterAttr as PlayerAttr;
        if(playerAttr == null)
            return 0;
        return ( playerAttr.GetPlayerLv() - 1 ) * 2;
    }
}
