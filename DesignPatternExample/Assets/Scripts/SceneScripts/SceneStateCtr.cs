using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneStateCtr : MonoBehaviour {
    private SceneStateContext _stateContext = new SceneStateContext();
    private static SceneStateCtr _instance;
    public static SceneStateCtr Instance { 
        get {
            return _instance;
        } 
    }
    private SceneStateCtr() { }
    void Awake() {
        if(_instance) {
            Destroy(this);
        }
        else {
            _instance = this;
            DontDestroyOnLoad(this);
        }
    }
    void Start() {
        _stateContext.SetState(new StartState(_stateContext), "");
    }
    void Update() {
        _stateContext.StateUpdate();
    }
}
