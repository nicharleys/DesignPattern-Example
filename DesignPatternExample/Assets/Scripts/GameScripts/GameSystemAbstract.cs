public abstract class GameSystemAbstract {
    protected GameFunction GameFunction = null;
    public GameSystemAbstract(GameFunction theFunction) {
        GameFunction = theFunction;
    }
    public virtual void Initialize() { }
    public virtual void Release() { }
    public virtual void Update() { }
    public virtual void FixedUpdate() { }
}
