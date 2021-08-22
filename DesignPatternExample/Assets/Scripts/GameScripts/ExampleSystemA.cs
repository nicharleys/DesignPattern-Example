using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleSystemA : GameSystemAbstract {
    private int _exampleCount = 1;
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
        return _exampleCount;
    }
    public void CloseGame() {
        if(GameFunction.IsCloseGame == false) {
            GameFunction.IsCloseGame = true;
        }
    }
}
