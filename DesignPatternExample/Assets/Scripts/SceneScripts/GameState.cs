public class GameState : ISceneState {
    public GameState(SceneStateContext theContext) : base(theContext) {
        this.StateName = "GameState";
    }
    public override void StateBegin() {
        GameFunction.Instance.Initinal();
    }
    public override void StateEnd() {
        GameFunction.Instance.Release();
    }
    public override void StateUpdate() {
        InputProcess();
        GameFunction.Instance.Update();
        if(GameFunction.Instance.GetCloseGameBool()) {
            m_Context.SetState(new MenuState(m_Context), "MenuScene");
        }
    }
    private void InputProcess() {

    }
}
