using UnityEngine;

public class TPController : MonoBehaviour {

    [SerializeField] private float _jumpPower = 8f;
    [SerializeField, Range(1, 4)] private float _gravityMultiplier = 2;

    private Animator _animator;
    private Rigidbody _rigidbody;
    private bool _isGrounded;

    void Start() {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody>();
    }
    public void Move(Vector3 moveVector, bool isJump) {
        Vector3 groundNormal;
        if(Physics.Raycast(transform.position + Vector3.up * 0.1f, Vector3.down, out RaycastHit hit, _rigidbody.velocity.y < 0.01f ? 0.4f : 0.01f)) {
            groundNormal = hit.normal;
            _isGrounded = true;
        }
        else {
            groundNormal = Vector3.up;
            _isGrounded = false;
        }
        _animator.SetBool("Ground", _isGrounded);

        moveVector = Vector3.ProjectOnPlane(moveVector, groundNormal);
        moveVector = transform.InverseTransformDirection(moveVector);

        float turnSpeed = Mathf.Lerp(180, 360, moveVector.z);
        transform.Rotate(0, turnSpeed * Time.deltaTime * Mathf.Atan2(moveVector.x, moveVector.z), 0);
       

        if(moveVector.z < 0) {
            _animator.SetFloat("Speed", Mathf.Lerp(_animator.GetFloat("Speed"), 0, 0.1f));
        }
        else {
            _animator.SetFloat("Speed", Mathf.Lerp(_animator.GetFloat("Speed"), moveVector.z, 0.1f));
        }
        if(_isGrounded) {
            if(isJump) {
                _rigidbody.AddForce(Vector3.up * _jumpPower, ForceMode.Impulse);
            }
        }
        else {
            _rigidbody.AddForce(( Physics.gravity * _gravityMultiplier ) - Physics.gravity);
        }
    }
    private void OnAnimatorMove() {
        if(_isGrounded) {
            Vector3 vector = _animator.deltaPosition / Time.deltaTime;
            vector.y = _rigidbody.velocity.y;
            _rigidbody.velocity = vector;
        }
    }
}
