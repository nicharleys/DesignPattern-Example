public class ISceneState {
    private string _StateName = "ISceneState";
    public string StateName {
        get {
            return _StateName;
        }
        set {
            _StateName = value;
        }
    }
    protected SceneStateContext m_Context;
    public ISceneState(SceneStateContext theContext) {
        m_Context = theContext;
    }
    public virtual void StateBegin() { }
    public virtual void StateUpdate() { }
    public virtual void StateEnd() { }
    public override string ToString() {
        return string.Format("[I_SceneState: stateName={0}]", StateName);
    }

}
