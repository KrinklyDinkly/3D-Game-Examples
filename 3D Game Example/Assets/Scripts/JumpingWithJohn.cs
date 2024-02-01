using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingWithJohn : MonoBehaviour
{
    public float turnSpeed = 20;
    public float moveSpeed = 1f;
    public float JumpForce = 10f;
    public float GravityModifier = 1f;
    public bool IsOnGround = true;
    private Vector3 _movement;
    //private Animator _animator;
    private Rigidbody _rigidbody;
    private Quaternion _rotation = Quaternion.identity;

    // Start is called before the first frame update
    void Start()
    {
        //m_Animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody>();
        Physics.gravity *= GravityModifier;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        _movement.Set(horizontal, 0f, vertical);
        _movement.Normalize();

        bool hasHorizontalInput = !Mathf.Approximately (horizontal, 0f);
        bool hasVerticalInput = !Mathf.Approximately (vertical, 0f);
        bool isWalking = hasHorizontalInput || hasVerticalInput;
        //m_Animator.SetBool ("IsWalking", isWalking);

        Vector3 desiredForward = Vector3.RotateTowards (transform.forward,_movement, turnSpeed * Time.deltaTime, 0f);
        _rotation = Quaternion.LookRotation (desiredForward);

        _rigidbody.MovePosition (_rigidbody.position + _movement * moveSpeed * Time.deltaTime);
        _rigidbody.MoveRotation (_rotation);

        if(Input.GetKeyDown(KeyCode.Space) && IsOnGround)
        {
            _rigidbody.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
            IsOnGround = false;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            IsOnGround = true;
        }

    //void OnAnimatorMove ()
    //{
      //  _rigidbody.MovePosition (_rigidbody.position + _movement * m_Animator.deltaPosition.magnitude);
       // _rigidbody.MoveRotation (_rotation);
    //}
}
}
