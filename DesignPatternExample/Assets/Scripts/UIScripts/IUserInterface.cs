using UnityEngine;

public abstract class IUserInterface
{
    protected GameFunction GameFunction = null;
    protected GameObject RootUI = null;
    private bool _isAction = true;
    public IUserInterface(GameFunction theFunction) {
        GameFunction = theFunction;
    }
    public bool GetActionState() {
        return _isAction;
    }
    public virtual void ShowUI() {
        RootUI.SetActive(true);
        _isAction = true;
    }
    public virtual void HideUI() {
        RootUI.SetActive(false);
        _isAction = false;
    }
    public virtual void Initialize() { }
    public virtual void Release() { }
    public virtual void Update() { }

}
