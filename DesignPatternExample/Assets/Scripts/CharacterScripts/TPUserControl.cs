using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TPController))]
public class TPUserControl : MonoBehaviour {

    private Transform m_cam;
    private TPController m_TPController;
    private bool m_bJump;

    void Start() {
        m_cam = Camera.main.transform;
        m_TPController = GetComponent<TPController>();
        Cursor.lockState = CursorLockMode.Confined;
    }
    void Update() {
        if(Input.GetButtonDown("Jump")) {
            m_bJump = true;
        }
    }
    private void FixedUpdate() {
        Vector3 move = m_cam.forward * Input.GetAxis("Vertical") + m_cam.right * Input.GetAxis("Horizontal");
        move = Vector3.Scale(move, new Vector3(1, 0, 1)).normalized;
        if(Input.GetKeyDown(KeyCode.LeftShift)) {
            move *= 0.5f;
        }
        m_TPController.Move(move, m_bJump);
        m_bJump = false;
    }
}
