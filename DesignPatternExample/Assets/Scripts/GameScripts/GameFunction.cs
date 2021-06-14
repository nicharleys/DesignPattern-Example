public class GameFunction {

    private static GameFunction _instance;
    public static GameFunction Instance {
        get {
            if(_instance == null)
                _instance = new GameFunction();
            return _instance;
        }
    }

    public bool m_bCloseGame;
    //UI
    private ExampleUIA m_ExampleUIA = null;
    private ExampleUIB m_ExampleUIB = null;
    //System
    private ExampleSystemA m_ExampleSystemA = null;
    private ExampleSystemB m_ExampleSystemB = null;
    private ExampleSystemC m_ExampleSystemC = null;

    public void Initinal() {
        m_bCloseGame = false;
        //UI
        m_ExampleUIA = new ExampleUIA(this);
        m_ExampleUIB = new ExampleUIB(this);
        //System
        m_ExampleSystemA = new ExampleSystemA(this);
        m_ExampleSystemB = new ExampleSystemB(this);
        m_ExampleSystemC = new ExampleSystemC(this);

    }
    public void Release() {
        //UI
        m_ExampleUIA = null;
        m_ExampleUIB = null;
        //System
        m_ExampleSystemA = null;
        m_ExampleSystemB = null;
        m_ExampleSystemC = null;
    }
    public void Update() {
        //UI
        m_ExampleUIA.Update();
        m_ExampleUIB.Update();
        //System
        m_ExampleSystemA.Update();
        m_ExampleSystemB.Update();
        m_ExampleSystemC.Update();
    }
    public void ChangeCloseGameBool() {
        m_ExampleSystemA.CloseGame();
    }
    public bool GetCloseGameBool() {
        return m_bCloseGame;
    }
    public int GetExampleCountA() {
        return m_ExampleSystemA.GetExampleCountA();
    }
    public int GetTimeCount() {
        return m_ExampleSystemB.GetTimeCount();
    }
    public int GetExampleCountC() {
        return m_ExampleSystemC.GetExampleCountC();
    }
}
