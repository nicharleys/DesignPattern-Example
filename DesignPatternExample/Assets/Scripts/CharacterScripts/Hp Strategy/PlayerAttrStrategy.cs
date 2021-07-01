using UnityEngine;

public class PlayerAttrStrategy : IAttrStrategy
{
    public override void Initialize(ICharacterAttr theCharacterAttr) {
        PlayerAttr playerAttr = theCharacterAttr as PlayerAttr;
        if(playerAttr == null) {
            Debug.LogError("Check your PlayerAttr.SetAttStrategy is placed PlayerAttrStrategy");
            return;
        }
        int extraMaxHp = 0;
        int playerLv = playerAttr.GetPlayerLv();
        if(playerLv > 0)
            extraMaxHp = ( playerLv - 1 ) * 2;
        playerAttr.AddExtraMaxHp(extraMaxHp);
    }
    public override float GetAtkPlusValue(ICharacterAttr theCharacterAttr) {
        return 0;
    }
    public override float GetDmgDescValue(ICharacterAttr theCharacterAttr) {
        PlayerAttr playerAttr = theCharacterAttr as PlayerAttr;
        if(playerAttr == null)
            return 0;
        return ( playerAttr.GetPlayerLv() - 1 ) * 2;
    }
}
