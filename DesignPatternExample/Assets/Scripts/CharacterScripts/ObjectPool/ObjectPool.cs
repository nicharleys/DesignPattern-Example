using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectPool : MonoBehaviour {
    public GameObject prefab;
    public int PfbAmount = 0;

    private Queue<GameObject> m_Pool = new Queue<GameObject>();
    public void Initialize() {
        for(int i = 0; i < PfbAmount; i++) {
            GameObject initPfb = Instantiate(prefab);
            m_Pool.Enqueue(initPfb);
            initPfb.SetActive(false);
            initPfb.transform.SetParent(gameObject.transform);
        }
    }
    public int GetObjectAmount() {
        return m_Pool.Count;
    }
    public void ReUse(Vector3 position, Quaternion rotation) {
        if(m_Pool.Count > 0) {
            GameObject reusePfb = m_Pool.Dequeue();
            reusePfb.transform.position = position;
            reusePfb.transform.rotation = rotation;
            reusePfb.SetActive(true);
            reusePfb.transform.SetParent(gameObject.transform);
        }
        else {
            GameObject initPfb = Instantiate(prefab);
            initPfb.transform.position = position;
            initPfb.transform.rotation = rotation;
            initPfb.transform.SetParent(gameObject.transform);
        }
    }
    public void Recovery(GameObject recoveryPfb, Vector3 position, Quaternion rotation) {
        m_Pool.Enqueue(recoveryPfb);
        recoveryPfb.transform.position = position;
        recoveryPfb.transform.rotation = rotation;
        recoveryPfb.SetActive(false);
        recoveryPfb.transform.SetParent(gameObject.transform);
    }
}