public class ISceneState {
    private string _stateName = "ISceneState";
    public string StateName {
        get {
            return _stateName;
        }
        set {
            _stateName = value;
        }
    }
    protected SceneStateContext StateContext;
    public ISceneState(SceneStateContext theContext) {
        StateContext = theContext;
    }
    public virtual void StateBegin() { }
    public virtual void StateUpdate() { }
    public virtual void StateEnd() { }
    public override string ToString() {
        return string.Format("[I_SceneState: stateName={0}]", StateName);
    }

}
