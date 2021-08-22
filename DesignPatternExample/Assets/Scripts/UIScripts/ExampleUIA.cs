using UnityEngine;

public class ExampleUIA : UserInterfaceAbstract {

    public ExampleUIA(GameFunction theFunction) : base(theFunction) {
        Initialize();
    }
    public override void Update() {
        base.Update();
        GetExampleUIA();
    }
    public override void Initialize() {

    }
    private void GetExampleUIA() {
        Debug.Log("ExampleUIA");
    }
}
