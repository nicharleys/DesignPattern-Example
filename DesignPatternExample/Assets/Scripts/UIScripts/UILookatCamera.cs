using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILookatCamera : MonoBehaviour {
    void Update() {
        Vector3 direction = Camera.main.transform.position - transform.position;
        Quaternion rot = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Euler(rot.eulerAngles.x, rot.eulerAngles.y, 0);
    }
}
