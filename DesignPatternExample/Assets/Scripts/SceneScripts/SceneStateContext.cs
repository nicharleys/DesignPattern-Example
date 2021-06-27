using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneStateContext {
    private ISceneState m_State;
    private bool m_bRunBegin = false;
    private string m_stSceneName = "";
    public SceneStateContext() { }
    public void SetState(ISceneState theState, string theSceneName) {
        m_bRunBegin = false;
        m_stSceneName = theSceneName;
        LoadScene(theSceneName);
        if(m_State != null)
            m_State.StateEnd();
        m_State = theState;
    }
    private void LoadScene(string theSceneName) {
        if(theSceneName == null || theSceneName.Length == 0)
            return;
        SceneManager.LoadScene(theSceneName);
    }
    public void StateUpdate() {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(m_stSceneName);
        if(!asyncOperation.isDone)
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
