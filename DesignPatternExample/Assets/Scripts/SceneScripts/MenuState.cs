using UnityEngine;
using UnityEngine.UI;

public class MenuState : ISceneState {
    public MenuState(SceneStateContext theContext) : base(theContext) {
        this.StateName = "MenuState";
    }
    public override void StateBegin() {
        Button tmpBtn = GameObject.FindWithTag("StartGameBtn").GetComponent<Button>();
        if(tmpBtn != null) {
            tmpBtn.onClick.AddListener(() => OnStartGameBtnClick());
        }
    }
    public override void StateUpdate() {
    }
    public override void StateEnd() {
        //Release();
    }
    private void OnStartGameBtnClick() {
        m_Context.SetState(new GameState(m_Context), "GameScene");
    }
}
