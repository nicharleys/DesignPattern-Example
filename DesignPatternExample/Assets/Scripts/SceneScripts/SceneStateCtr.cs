using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneStateCtr : MonoBehaviour {
    SceneStateContext m_Context = new SceneStateContext();
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
        m_Context.SetState(new StartState(m_Context), "");
    }
    void Update() {
        m_Context.StateUpdate();
    }
}
