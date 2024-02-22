using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public int score = 0;
    public float turnSpeed = 20;
    public float moveSpeed = 1f;
    public float JumpForce = 10f;
    public float GravityModifier = 1f;
    public float outOfBounds = -10f;
    public GameObject checkpointAreaObject;
    public GameObject finishAreaObject;
    public bool IsOnGround = true;
    public bool isAtCheckpoint = false;
    private Vector3 _movement;
    //private Animator _animator;
    private Rigidbody _rigidbody;
    private Quaternion _rotation = Quaternion.identity;
    private Vector3 _defaultGravity = new Vector3(0f, -9.81f, 0f);
    private Vector3 _startingPosition;
    private Vector3 _checkpointPosition;

     private GameObject[] _collectibles;


    // Start is called before the first frame update
    void Start()
    {
        //m_Animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody>();
        Physics.gravity = _defaultGravity;
        Physics.gravity *= GravityModifier;
        _startingPosition = transform.position;
        _collectibles = GameObject.FindGameObjectsWithTag("Collectible-Return");
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && IsOnGround)
        {
            _rigidbody.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
            IsOnGround = false;
        }

        if(transform.position.y < outOfBounds)
        {
            if(isAtCheckpoint)
            {
                ReturningCollectibles();
                transform.position = _checkpointPosition;
            }
            else
            {
                ReturningCollectibles();
                transform.position = _startingPosition;
            }
            
        }
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


        if (collision.gameObject.CompareTag("Spinner"))
        {
            if(isAtCheckpoint)
            {
                ReturningCollectibles();
                transform.position = checkpointAreaObject.transform.position;
                
            }
            else
            {
                ReturningCollectibles();
                transform.position = _startingPosition;
            }
        }

    //void OnAnimatorMove ()
    //{
      //  _rigidbody.MovePosition (_rigidbody.position + _movement * m_Animator.deltaPosition.magnitude);
       // _rigidbody.MoveRotation (_rotation);
    //}
}
    void OnTriggerEnter(Collider other)
        {
            if(other.gameObject == checkpointAreaObject)
            {
                isAtCheckpoint = true;
                _checkpointPosition = checkpointAreaObject.transform.position;
            }    

            if(other.gameObject == finishAreaObject)
            {
                isAtCheckpoint = false;
                ReturningCollectibles();
                transform.position = _startingPosition;
            }

            if(other.gameObject.CompareTag("Collectible-Destroy"))
        {
            score++;
            Destroy(other.gameObject);
        }

        if(other.gameObject.CompareTag("Collectible-Return"))
        {
            score++;
            other.gameObject.GetComponent<Collectible>().HideCollectibles();
        }
        }

        void ReturningCollectibles()
    {
        for(int i = 0; i < _collectibles.Length; i++)
        {
            _collectibles[i].SetActive(true);
            _collectibles[i].GetComponent<Collectible>().ReturnCollectibles();
        }
    }
    public bool IsPlayerOnGround()
    {
        return IsOnGround;
    }

}