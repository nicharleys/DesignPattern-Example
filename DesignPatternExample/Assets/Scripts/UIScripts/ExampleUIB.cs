using UnityEngine;

public class ExampleUIB : IUserInterface {

    public ExampleUIB(GameFunction theFunction) : base(theFunction) {
        Initialize();
    }
    public override void Update() {
        base.Update();
        GetExampleUIB();
        GetSystemFunction();
    }
    public override void Initialize() {

    }
    private void GetExampleUIB() {
        Debug.Log("ExampleUIB");
    }
    public void GetSystemFunction() {
        m_GameFunction.GetExampleCountA();
    }
}
