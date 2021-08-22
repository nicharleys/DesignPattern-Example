using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterFactory : CharacterFactoryAbstract {
    private CharacterBuilderSystem _builderDirector = new CharacterBuilderSystem(GameFunction.Instance);
    public override PlayerAbstract CreatePlayer(PlayerType playerType, int lv, Vector3 spawnPosition) {
        PlayerBuilderParam playerParam = new PlayerBuilderParam();
        switch(playerType) {
            case PlayerType.A:
                playerParam.Character = new PlayerA();
                break;
            case PlayerType.B:
                playerParam.Character = new PlayerB();
                break;
            case PlayerType.C:
                playerParam.Character = new PlayerC();
                break;
            default:
                Debug.LogWarning("CreatePlayer:無法建立[" + playerType + "]");
            return null;
        }
        playerParam.SpqwnPosition = spawnPosition;
        playerParam.Lv = lv;

        PlayerBuilder playerBuilder = new PlayerBuilder();
        playerBuilder.SetBuildParam(playerParam);

        _builderDirector.Construct(playerBuilder);
        return playerParam.Character as PlayerAbstract;
    }
    public override EnemyAbstract CreateEnemy(EnemyType enemyType, int lv, Vector3 spawnPosition) {
        EnemyBuilderParam enemyParam = new EnemyBuilderParam();
        switch(enemyType) {
            case EnemyType.A:
                enemyParam.Character = new EnemyA();
                break;
            case EnemyType.B:
                enemyParam.Character = new EnemyB();
                break;
            case EnemyType.C:
                enemyParam.Character = new EnemyC();
                break;
            default:
                Debug.LogWarning("CreatePlayer:無法建立[" + enemyType + "]");
                return null;
        }
        enemyParam.SpqwnPosition = spawnPosition;

        EnemyBuilder enemyBuilder = new EnemyBuilder();
        enemyBuilder.SetBuildParam(enemyParam);

        _builderDirector.Construct(enemyBuilder);
        return enemyParam.Character as EnemyAbstract;
    }
}
