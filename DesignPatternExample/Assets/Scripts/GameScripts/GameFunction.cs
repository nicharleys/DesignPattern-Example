public class GameFunction {

    private static GameFunction _instance;
    public static GameFunction Instance {
        get {
            if(_instance == null)
                _instance = new GameFunction();
            return _instance;
        }
    }

    public bool IsCloseGame;
    //UI
    private ExampleUIA _exampleUIA = null;
    private ExampleUIB _exampleUIB = null;
    //System
    private ExampleSystemA _exampleSystemA = null;
    private ExampleSystemB _exampleSystemB = null;
    private ExampleSystemC _exampleSystemC = null;

    public void Initialize() {
        IsCloseGame = false;
        //UI
        _exampleUIA = new ExampleUIA(this);
        _exampleUIB = new ExampleUIB(this);
        //System
        _exampleSystemA = new ExampleSystemA(this);
        _exampleSystemB = new ExampleSystemB(this);
        _exampleSystemC = new ExampleSystemC(this);

    }
    public void Release() {
        //UI
        _exampleUIA = null;
        _exampleUIB = null;
        //System
        _exampleSystemA = null;
        _exampleSystemB = null;
        _exampleSystemC = null;
    }
    public void Update() {
        //UI
        _exampleUIA.Update();
        _exampleUIB.Update();
        //System
        _exampleSystemA.Update();
        _exampleSystemB.Update();
        _exampleSystemC.Update();
    }
    public void ChangeCloseGameBool() {
        _exampleSystemA.CloseGame();
    }
    public bool GetCloseGameBool() {
        return IsCloseGame;
    }
    public int GetExampleCountA() {
        return _exampleSystemA.GetExampleCountA();
    }
    public int GetTimeCount() {
        return _exampleSystemB.GetTimeCount();
    }
    public int GetExampleCountC() {
        return _exampleSystemC.GetExampleCountC();
    }
}
