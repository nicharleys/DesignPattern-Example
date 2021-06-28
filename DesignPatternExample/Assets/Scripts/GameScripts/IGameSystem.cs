public abstract class IGameSystem {
    protected GameFunction GameFunction = null;
    public IGameSystem(GameFunction theFunction) {
        GameFunction = theFunction;
    }
    public virtual void Initialize() { }
    public virtual void Release() { }
    public virtual void Update() { }
}
