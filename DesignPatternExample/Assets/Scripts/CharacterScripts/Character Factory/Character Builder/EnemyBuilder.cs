using UnityEngine;
public class EnemyBuilderParam : CharacterBuilderParamAbstruct {
    public EnemyBuilderParam() { }
}


public class EnemyBuilder : CharacterBuilderAbstruct {
    private EnemyBuilderParam _builderParam = null;
    public override void SetBuildParam(CharacterBuilderParamAbstruct theParam) {
        _builderParam = theParam as EnemyBuilderParam;
    }
    public override void LoadAsset(int gameObjectID) {
        //AssetFactoryAbstract assetFactory = GameFactory.GetAssetFactery();
        //GameObject enemyGameObject = AssetFactory.LoadEnemy(_builderParam.Character.GetAssetName);
        GameObject enemyGameObject = null;
        enemyGameObject.transform.position = _builderParam.SpqwnPosition;
        enemyGameObject.gameObject.name = string.Format("Enemy[{0}]", gameObjectID);
        _builderParam.Character.SetGameObject(enemyGameObject);
    }
    public override void AddOnClickScript() {
    }
    public override void SetCharacterAttr() {
        //AttrFactory attrFactory = GameFactory.GetAttrFactery();
        //EnemyAttr enemyAttr = attrFactory.GetPlayerAttr(_builderParam.Character.GetAttrID);
        //enemyAttr.SetAttStrategy(new EnemyAttrStrategy());
        //_builderParam.Character.SetCharacterAttr(enemyAttr);
    }
    public override void AddAI() {
        EnemyAI enemyAI = new EnemyAI(_builderParam.Character);
        _builderParam.Character.SetAI(enemyAI);
    }

    public override void AddCharacterSystem(GameFunction theGameFunction) {
        //theGameFunction.AddEnemy(_builderParam.Character as EnemyAbstract);
    }
}
