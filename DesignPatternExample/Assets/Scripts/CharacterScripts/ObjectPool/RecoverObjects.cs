using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecoverObjects : MonoBehaviour {
    private float m_fSecondTime;
    private bool m_bTouchObj = false;
    void Update() {
        if(gameObject.transform.position.y <= 0) {
            RecoveryConfirmObj();
        }
        if(m_bTouchObj == true) {
            TimeCount();
        }
        OverLifeTime();
    }
    private void RecoveryConfirmObj(){
        ScatterObjects m_scatterObjects = GameObject.Find("ObjectPool").GetComponent<ScatterObjects>();
        switch(gameObject.transform.name) {
            case "Cube(Clone)":
                Recovery(m_scatterObjects.m_CubePool);
                return;
            case "Sphere(Clone)":
                Recovery(m_scatterObjects.m_SpherePool);
                return;
            case "Capsule(Clone)":
                Recovery(m_scatterObjects.m_CapsulePool);
                return;
            default:
                return;
        }
    }
    private void Recovery(ObjectPool objectPool) {
        ScatterObjects m_scatterObjects = GameObject.Find("ObjectPool").GetComponent<ScatterObjects>();
        Vector3 randomScreenPos = new Vector3(Random.Range(-m_scatterObjects.screenPosRange.x, m_scatterObjects.screenPosRange.x), 1, Random.Range(-m_scatterObjects.screenPosRange.z, m_scatterObjects.screenPosRange.z));
        Quaternion pfbRotation = Quaternion.Euler(0, Random.Range(-180, 180), 0);
        objectPool.Recovery(gameObject, randomScreenPos, pfbRotation);
    }
    private void OnCollisionEnter(Collision collision) {
        if(collision.transform.name != "Plane") {
            gameObject.transform.tag = "Untagged";
            gameObject.transform.GetComponent<Rigidbody>().isKinematic = true;
            gameObject.transform.GetChild(0).gameObject.SetActive(false);
            gameObject.transform.GetChild(1).gameObject.SetActive(true);
            m_bTouchObj = true;
        }
    }
    private void TimeCount() {
        m_fSecondTime += Time.deltaTime;
    }
    private void OverLifeTime() {
        if(m_fSecondTime >= 1.2f) {
            m_bTouchObj = false;
            m_fSecondTime = 0;
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
            gameObject.transform.GetChild(1).gameObject.SetActive(false);
            gameObject.transform.GetComponent<Rigidbody>().isKinematic = false;
            gameObject.transform.tag = "Things";
            RecoveryConfirmObj();
        }
    }
}
