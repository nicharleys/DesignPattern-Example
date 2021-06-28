using UnityEngine;
using System.Collections;

public class ScatterObjects: MonoBehaviour {
    public Vector3 screenPosRange;
    public ObjectPool CubePool;
    public ObjectPool SpherePool;
    public ObjectPool CapsulePool;

    void Awake() {
        CubePool.Initialize();
        SpherePool.Initialize();
        CapsulePool.Initialize();
    }
    void Update() {
        FillAmount(CubePool);
        FillAmount(SpherePool);
        FillAmount(CapsulePool);
    }
    private void FillAmount(ObjectPool thePool) {
        if(thePool.GetObjAmount() > 0) {
            Vector3 randomScreenPos = new Vector3(Random.Range(-screenPosRange.x, screenPosRange.x), 1, Random.Range(-screenPosRange.z, screenPosRange.z));
            Quaternion pfbRotation = Quaternion.Euler(0, Random.Range(-180, 180), 0);
            thePool.ReUse(randomScreenPos, pfbRotation);
        }
    }
}