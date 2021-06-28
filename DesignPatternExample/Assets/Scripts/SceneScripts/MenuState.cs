using UnityEngine;
using UnityEngine.UI;

public class MenuState : ISceneState {
    public MenuState(SceneStateContext theContext) : base(theContext) {
        StateName = "MenuState";
    }
    public override void StateBegin() {
        Button startGameBtn = GameObject.FindWithTag("StartGameBtn").GetComponent<Button>();
        if(startGameBtn != null) {
            startGameBtn.onClick.AddListener(() => OnStartGameBtnClick());
        }
    }
    public override void StateEnd() {
        //Release();
    }
    public override void StateUpdate() {
    }
    private void OnStartGameBtnClick() {
        StateContext.SetState(new GameState(StateContext), "GameScene");
    }
}
