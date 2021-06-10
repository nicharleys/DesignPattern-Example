using UnityEngine;

public abstract class IUserInterface
{
    protected GameFunction m_GameFunction = null;
    protected GameObject m_RootUI = null;
    private bool m_bAction = true;
    public IUserInterface(GameFunction theFunction) {
        m_GameFunction = theFunction;
    }
    public bool IsVisible() {
        return m_bAction;
    }
    public virtual void ShowUI() {
        m_RootUI.SetActive(true);
        m_bAction = true;
    }
    public virtual void HideUI() {
        m_RootUI.SetActive(false);
        m_bAction = false;
    }
    public virtual void Initialize() { }
    public virtual void Release() { }
    public virtual void Update() { }

}
