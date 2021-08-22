public class SceneStateBase {
    private string _stateName = "SceneState";
    public string StateName {
        get {
            return _stateName;
        }
        set {
            _stateName = value;
        }
    }
    protected SceneStateContext StateContext;
    public SceneStateBase(SceneStateContext theContext) {
        StateContext = theContext;
    }
    public virtual void StateBegin() { }
    public virtual void StateUpdate() { }
    public virtual void StateEnd() { }
    public override string ToString() {
        return string.Format("[SceneStateBase: stateName={0}]", StateName);
    }

}
