public abstract class IGameSystem {
    protected GameFunction m_GameFunction = null;
    public IGameSystem(GameFunction Function) {
        m_GameFunction = Function;
    }
    public virtual void Initialize() { }
    public virtual void Release() { }
    public virtual void Update() { }
}
