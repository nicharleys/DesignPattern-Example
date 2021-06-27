using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotCtr : MonoBehaviour {
    private Animator m_Animator = null;
    public Transform BotHand;
    private GameObject HitThing = null;
    private bool m_bGetThing = false;
    private bool m_bThrowThing = false;
    void Start() {
        m_Animator = GetComponent<Animator>();
    }
    private void Update() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if(Input.GetMouseButton(0) && Physics.Raycast(ray, out hit)) {
            if(hit.transform.tag == "Things" && m_bGetThing == false) {
                m_bGetThing = true;
                HitThing = hit.transform.gameObject;
                HitThing.GetComponent<Rigidbody>().isKinematic = true;
                HitThing.GetComponent<Collider>().isTrigger = true;
                HitThing.transform.SetParent(BotHand);
                HitThing.transform.localPosition = Vector3.zero;
                HitThing.transform.localRotation = Quaternion.identity;
            }
        }
        if(HitThing != null && Input.GetKeyDown(KeyCode.LeftControl)) {
            m_bThrowThing = true;
            m_Animator.SetTrigger("Throw");
        }
    }
    public void ThrowThing() {
        HitThing.transform.SetParent(null);
        HitThing.GetComponent<Rigidbody>().isKinematic = false;
        HitThing.GetComponent<Collider>().isTrigger = false;
        HitThing.GetComponent<Rigidbody>().AddForce(gameObject.transform.forward * 1000);
        HitThing = null;
        m_bGetThing = false;
        m_bThrowThing = false;
    }
}
