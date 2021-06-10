using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleSystemB : IGameSystem {
    private float m_fSecondTime;
    private int m_iSecondTime;
    public ExampleSystemB(GameFunction theFunction) : base(theFunction) {
        Initialize();
    }
    public override void Initialize() {
        m_fSecondTime = 0;
        m_iSecondTime = 0;
    }
    public override void Update() {
        TimeCount();
    }
    private void TimeCount() {
        m_fSecondTime += Time.deltaTime;
        m_iSecondTime = (int)m_fSecondTime;
    }
    public int GetTimeCount() {
        return m_iSecondTime;
    }
}
