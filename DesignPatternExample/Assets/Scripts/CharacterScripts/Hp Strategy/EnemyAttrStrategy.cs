using UnityEngine;

public class EnemyAttrStrategy : IAttrStrategy {
    public override void Initialize(ICharacterAttr theCharacterAttr) {}
    public override float GetAtkPlusValue(ICharacterAttr theCharacterAttr) {
        EnemyAttr enemyAttr = theCharacterAttr as EnemyAttr;
        if(enemyAttr == null) {
            Debug.LogError("Check your EnemyAttr.SetAttStrategy is placed EnemyAttrStrategy");
            return 0;
        }
        int RandValue = Random.Range(0, 100);
        if(enemyAttr.GetCirtRate() >= RandValue) {
            enemyAttr.CutDownCirtRate();
            return enemyAttr.GetMaxHp() * 5;
        }
        return 0;
    }
    public override float GetDmgDescValue(ICharacterAttr theCharacterAttr) {
        return 0;
    }
}
