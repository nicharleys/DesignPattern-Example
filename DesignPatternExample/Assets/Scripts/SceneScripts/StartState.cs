public class StartState : ISceneState {
    public StartState(SceneStateContext theContext) : base(theContext) {
        this.StateName = "StartState";
    }
    public override void StateBegin() {
        //Initinal();
    }
    public override void StateUpdate() {
        m_Context.SetState(new MenuState(m_Context), "MenuScene");
    }
}
