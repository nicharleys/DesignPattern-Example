using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFunctionCtr : MonoBehaviour {
    public void ChangeCloseGameBool() {
        GameFunction.Instance.ChangeCloseGameBool();
    }
    public void GetExampleCountA() {
        Debug.Log(GameFunction.Instance.GetExampleCountA());
    }
    public void GetTimeCount() {
        Debug.Log(GameFunction.Instance.GetTimeCount());
    }
    public void GetExampleCountC() {
        Debug.Log(GameFunction.Instance.GetExampleCountC());
    }
}
