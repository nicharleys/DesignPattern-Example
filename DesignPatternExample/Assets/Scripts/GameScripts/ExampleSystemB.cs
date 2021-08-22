using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleSystemB : GameSystemAbstract {
    private float _secondTimeFloat;
    private int _secondTime;
    public ExampleSystemB(GameFunction theFunction) : base(theFunction) {
        Initialize();
    }
    public override void Initialize() {
        _secondTimeFloat = 0;
        _secondTime = 0;
    }
    public override void Update() {
        TimeCount();
    }
    private void TimeCount() {
        _secondTimeFloat += Time.deltaTime;
        _secondTime = (int)_secondTimeFloat;
    }
    public int GetTimeCount() {
        return _secondTime;
    }
}
