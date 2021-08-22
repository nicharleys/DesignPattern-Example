using DesignPatternExample.Character.CharacterSetting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public abstract class CharacterBuilderParamAbstruct {
    public CharacterAbstract Character = null;
    public Vector3 SpqwnPosition;
    public int AttrID;
    public string AssetName;
    public string IconSpriteName;
} 
public abstract class CharacterBuilderAbstruct
{
    public abstract void SetBuildParam(CharacterBuilderParamAbstruct theParam);
    public abstract void LoadAsset(int gameObjectID);
    public abstract void AddOnClickScript();
    public abstract void AddAI();
    public abstract void SetCharacterAttr();
    public abstract void AddCharacterSystem(GameFunction theGameFunction);
}
