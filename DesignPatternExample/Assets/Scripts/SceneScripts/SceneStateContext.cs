using UnityEngine;
public class SceneStateContext {
    private ISceneState m_State;
    private bool m_bRunBegin = false;
    public SceneStateContext() { }
    public void SetState(ISceneState theState, string theSceneName) {
        m_bRunBegin = false;
        LoadScene(theSceneName);
        if(m_State != null)
            m_State.StateEnd();
        m_State = theState;
    }
    private void LoadScene(string theSceneName) {
        if(theSceneName == null || theSceneName.Length == 0)
            return;
        Application.LoadLevel(theSceneName);
    }
    public void StateUpdate() {
        if(Application.isLoadingLevel)
            return;
        if(m_State != null && m_bRunBegin == false) {
            m_State.StateBegin();
            m_bRunBegin = true;
        }
        if(m_State != null) {
            m_State.StateUpdate();
        }
    }
}
