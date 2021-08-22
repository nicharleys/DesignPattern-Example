using UnityEngine;

public class EnemyAttrStrategy : AttrStrategyAbstract {
    public override void Initialize(CharacterAttrAbstract theCharacterAttr) {}
    public override float GetAtkPlusValue(CharacterAttrAbstract theCharacterAttr) {
        EnemyAttr enemyAttr = theCharacterAttr as EnemyAttr;
        if(enemyAttr == null) {
            Debug.LogError("Check your EnemyAttr.SetAttStrategy is placed EnemyAttrStrategy");
            return 0;
        }
        int RandValue = Random.Range(0, 100);
        if(enemyAttr.GetCirtRate() >= RandValue) {
            enemyAttr.CutDownCirtRate();
            return enemyAttr.GetMaxHP() * 5;
        }
        return 0;
    }
    public override float GetDmgDescValue(CharacterAttrAbstract theCharacterAttr) {
        return 0;
    }
}
