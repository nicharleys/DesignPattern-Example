using UnityEngine;
public class PlayerBuilderParam : CharacterBuilderParamAbstruct {
    public int Lv = 0;
    public PlayerBuilderParam() { }
}
public class PlayerBuilder : CharacterBuilderAbstruct {
    private PlayerBuilderParam _builderParam = null;
    public override void SetBuildParam(CharacterBuilderParamAbstruct theParam) {
        _builderParam = theParam as PlayerBuilderParam;
    }
    public override void LoadAsset(int gameObjectID) {
        //AssetFactoryAbstract assetFactory = GameFactory.GetAssetFactery();
        //GameObject playerGameObject = AssetFactory.LoadPlayer(_builderParam.Character.GetAssetName);
        GameObject playerGameObject = null;
        playerGameObject.transform.position = _builderParam.SpqwnPosition;
        playerGameObject.gameObject.name = string.Format("Player[{0}]", gameObjectID);
        _builderParam.Character.SetGameObject(playerGameObject);
    }
    public override void AddOnClickScript() {
        //PlayerOnClick script = _builderParam.Character.GetGameObject().AddComponent<PlayerOnClick>();
        //script.Player = _builderParam.Character as PlayerAbstract;
    }
    public override void SetCharacterAttr() {
        //AttrFactory attrFactory = GameFactory.GetAttrFactery();
        //PlayerAttr playerAttr = attrFactory.GetPlayerAttr(_builderParam.Character.GetAttrID);
        //playerAttr.SetAttStrategy(new PlayerAttrStrategy());
        //playerAttr.SetPlayerLv(_builderParam.Lv);
        //_builderParam.Character.SetCharacterAttr(playerAttr);
    }
    public override void AddAI() {
    }
    public override void AddCharacterSystem(GameFunction theGameFunction) {
        //theGameFunction.AddPlayer(_builderParam.Character as PlayerAbstract);
    }
}
