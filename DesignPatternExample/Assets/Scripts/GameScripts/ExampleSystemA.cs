using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleSystemA : IGameSystem {
    private int m_ExampleCount = 1;
    public ExampleSystemA(GameFunction theFunction) : base(theFunction) {
        Initialize();
    }
    public override void Initialize() {

    }
    public override void Update() {
        ExampleUpdateA();
    }
    private void ExampleUpdateA() {
        Debug.Log("Run ExampleA Update");
    }
    public int GetExampleCountA() {
        return m_ExampleCount;
    }
    public void CloseGame() {
        if(m_GameFunction.m_bCloseGame == false) {
            m_GameFunction.m_bCloseGame = true;
        }
    }
}
