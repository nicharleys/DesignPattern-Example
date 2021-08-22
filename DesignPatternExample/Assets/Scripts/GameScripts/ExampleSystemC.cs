using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleSystemC : GameSystemAbstract {
    private int _exampleCount = 3;
    public ExampleSystemC(GameFunction theFunction) : base(theFunction) {
        Initialize();
    }
    public override void Initialize() {

    }
    public override void Update() {
        ExampleUpdateC();
    }
    private void ExampleUpdateC() {
        Debug.Log("Run ExampleC Update");
    }
    public int GetExampleCountC() {
        return _exampleCount;
    }
}
