public class CharacterBuilderSystem : GameSystemAbstract
{
    private int _gameObjectID = 0;
    private GameFunction _gameFunction;
    public CharacterBuilderSystem(GameFunction theFunction) : base(theFunction) {
        _gameFunction = theFunction;
    }
    public void Construct(CharacterBuilderAbstruct theBuilder) {
        theBuilder.LoadAsset(++_gameObjectID);
        theBuilder.AddOnClickScript();
        theBuilder.SetCharacterAttr();
        theBuilder.AddAI();

        theBuilder.AddCharacterSystem(_gameFunction);
    }
}
