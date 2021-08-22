using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneStateContext {
    private SceneStateBase _sceneState;
    private bool _isRunBegin = false;
    private string _sceneName = "";
    public SceneStateContext() { }
    public void SetState(SceneStateBase theState, string sceneName) {
        _isRunBegin = false;
        _sceneName = sceneName;
        LoadScene(sceneName);
        if(_sceneState != null)
            _sceneState.StateEnd();
        _sceneState = theState;
    }
    private void LoadScene(string sceneName) {
        if(sceneName == null || sceneName.Length == 0)
            return;
        SceneManager.LoadScene(sceneName);
    }
    public void StateUpdate() {
        AsyncOperation asyncOperation = null;
        if(SceneManager.GetActiveScene().name != _sceneName && _sceneName != "") {
            asyncOperation = SceneManager.LoadSceneAsync(_sceneName);
            if(!asyncOperation.isDone) {
                return;
            }
        }
        if(_sceneState != null && _isRunBegin == false) {
            _sceneState.StateBegin();
            _isRunBegin = true;
        }
        if(_sceneState != null) {
            _sceneState.StateUpdate();
        }
    }
}
