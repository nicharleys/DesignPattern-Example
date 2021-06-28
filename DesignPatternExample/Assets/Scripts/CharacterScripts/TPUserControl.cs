using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TPController))]
public class TPUserControl : MonoBehaviour {

    private Transform _cam;
    private TPController _tpController;
    private bool _isJump;

    void Start() {
        _cam = Camera.main.transform;
        _tpController = GetComponent<TPController>();
        Cursor.lockState = CursorLockMode.Confined;
    }
    void Update() {
        if(Input.GetButtonDown("Jump")) {
            _isJump = true;
        }
    }
    private void FixedUpdate() {
        Vector3 move = _cam.forward * Input.GetAxis("Vertical") + _cam.right * Input.GetAxis("Horizontal");
        move = Vector3.Scale(move, new Vector3(1, 0, 1)).normalized;
        if(Input.GetKeyDown(KeyCode.LeftShift)) {
            move *= 0.5f;
        }
        _tpController.Move(move, _isJump);
        _isJump = false;
    }
}
