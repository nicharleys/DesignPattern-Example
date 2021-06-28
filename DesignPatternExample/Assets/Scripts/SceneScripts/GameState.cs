public class GameState : ISceneState {
    public GameState(SceneStateContext theContext) : base(theContext) {
        StateName = "GameState";
    }
    public override void StateBegin() {
        GameFunction.Instance.Initialize();
    }
    public override void StateEnd() {
        GameFunction.Instance.Release();
    }
    public override void StateUpdate() {
        InputProcess();
        GameFunction.Instance.Update();
        if(GameFunction.Instance.GetCloseGameBool()) {
            StateContext.SetState(new MenuState(StateContext), "MenuScene");
        }
    }
    private void InputProcess() {

    }
}
