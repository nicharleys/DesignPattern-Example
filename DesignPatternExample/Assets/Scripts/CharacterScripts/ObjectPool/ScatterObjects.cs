using UnityEngine;
using System.Collections;

public class ScatterObjects: MonoBehaviour {
    public ObjectPool m_CubePool;
    public ObjectPool m_SpherePool;
    public ObjectPool m_CapsulePool;

    public Vector3 screenPosRange;

    void Awake() {
        m_CubePool.Initialize();
        m_SpherePool.Initialize();
        m_CapsulePool.Initialize();
    }
    void Update() {
        FillAmount(m_CubePool);
        FillAmount(m_SpherePool);
        FillAmount(m_CapsulePool);
    }
    private void FillAmount(ObjectPool thePool) {
        if(thePool.GetObjectAmount() > 0) {
            Vector3 randomScreenPos = new Vector3(Random.Range(-screenPosRange.x, screenPosRange.x), 1, Random.Range(-screenPosRange.z, screenPosRange.z));
            Quaternion pfbRotation = Quaternion.Euler(0, Random.Range(-180, 180), 0);
            thePool.ReUse(randomScreenPos, pfbRotation);
        }
    }
}