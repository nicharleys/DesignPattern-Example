public class GameState : ISceneState {
    public GameState(SceneStateContext theContext) : base(theContext) {
        this.StateName = "GameState";
    }
    public override void StateBegin() {
        GameFunction.Instance.Initinal();
    }
    public override void StateUpdate() {
        InputProcess();
        GameFunction.Instance.Update();
        if(GameFunction.Instance.GetCloseGameBool() == true) {
            m_Context.SetState(new MenuState(m_Context), "MenuScene");
        }
    }
    public override void StateEnd() {
        GameFunction.Instance.Release();
    }
    private void InputProcess() {

    }
}
