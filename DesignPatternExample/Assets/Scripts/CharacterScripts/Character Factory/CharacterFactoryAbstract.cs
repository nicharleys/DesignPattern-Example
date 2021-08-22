using UnityEngine;

public abstract class CharacterFactoryAbstract {
    public abstract PlayerAbstract CreatePlayer(PlayerType playerType, int lv, Vector3 spawnPosition);
    public abstract EnemyAbstract CreateEnemy(EnemyType enemyType, int lv, Vector3 spawnPosition);
}
