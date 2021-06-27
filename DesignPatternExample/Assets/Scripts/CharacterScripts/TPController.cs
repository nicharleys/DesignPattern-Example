using UnityEngine;

public class TPController : MonoBehaviour {

    [SerializeField] float m_JumpPower = 8f;
    [SerializeField, Range(1, 4)] float m_GravityMultiplier = 2;

    private Animator m_Animator;
    private Rigidbody m_Rigidbody;
    private bool m_Grounded;

    void Start() {
        m_Animator = GetComponent<Animator>();
        m_Rigidbody = GetComponent<Rigidbody>();
    }
    public void Move(Vector3 move, bool jump) {
        Vector3 groundNormal;
        if(Physics.Raycast(transform.position + Vector3.up * 0.1f, Vector3.down, out RaycastHit hit, m_Rigidbody.velocity.y < 0.01f ? 0.4f : 0.01f)) {
            groundNormal = hit.normal;
            m_Grounded = true;
        }
        else {
            groundNormal = Vector3.up;
            m_Grounded = false;
        }
        m_Animator.SetBool("Ground", m_Grounded);

        move = Vector3.ProjectOnPlane(move, groundNormal);
        move = transform.InverseTransformDirection(move);

        float turnSpeed = Mathf.Lerp(180, 360, move.z);
        transform.Rotate(0, turnSpeed * Time.deltaTime * Mathf.Atan2(move.x, move.z), 0);
       

        if(move.z < 0) {
            m_Animator.SetFloat("Speed", Mathf.Lerp(m_Animator.GetFloat("Speed"), 0, 0.1f));
        }
        else {
            m_Animator.SetFloat("Speed", Mathf.Lerp(m_Animator.GetFloat("Speed"), move.z, 0.1f));
        }
        if(m_Grounded) {
            if(jump) {
                m_Rigidbody.AddForce(Vector3.up * m_JumpPower, ForceMode.Impulse);
            }
        }
        else {
            m_Rigidbody.AddForce(( Physics.gravity * m_GravityMultiplier ) - Physics.gravity);
        }
    }
    private void OnAnimatorMove() {
        if(m_Grounded) {
            Vector3 v = m_Animator.deltaPosition / Time.deltaTime;
            v.y = m_Rigidbody.velocity.y;
            m_Rigidbody.velocity = v;
        }
    }
}
