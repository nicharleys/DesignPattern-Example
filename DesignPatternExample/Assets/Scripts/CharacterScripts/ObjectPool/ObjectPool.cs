using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectPool : MonoBehaviour {
    public GameObject Prefab;
    public int PfbAmount = 0;

    private Queue<GameObject> _pool = new Queue<GameObject>();
    public void Initialize() {
        for(int i = 0; i < PfbAmount; i++) {
            GameObject initPfb = Instantiate(Prefab);
            _pool.Enqueue(initPfb);
            initPfb.SetActive(false);
            initPfb.transform.SetParent(gameObject.transform);
        }
    }
    public int GetObjAmount() {
        return _pool.Count;
    }
    public void ReUse(Vector3 position, Quaternion rotation) {
        if(_pool.Count > 0) {
            GameObject reusePfb = _pool.Dequeue();
            reusePfb.transform.position = position;
            reusePfb.transform.rotation = rotation;
            reusePfb.SetActive(true);
            reusePfb.transform.SetParent(gameObject.transform);
        }
        else {
            GameObject initPfb = Instantiate(Prefab);
            initPfb.transform.position = position;
            initPfb.transform.rotation = rotation;
            initPfb.transform.SetParent(gameObject.transform);
        }
    }
    public void Recovery(GameObject recoveryPfb, Vector3 position, Quaternion rotation) {
        _pool.Enqueue(recoveryPfb);
        recoveryPfb.transform.position = position;
        recoveryPfb.transform.rotation = rotation;
        recoveryPfb.SetActive(false);
        recoveryPfb.transform.SetParent(gameObject.transform);
    }
}