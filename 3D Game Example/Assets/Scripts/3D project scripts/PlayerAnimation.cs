using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private PlayerMovement _characterMovement;
    private Animator _characterAnimtor;

    // Start is called before the first frame update
    void Start()
    {
        _characterAnimtor = GetComponent<Animator>();
        _characterMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerWalking();
        PlayerJumping();
    }
    private void PlayerWalking()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        if(Mathf.Abs(horizontal) > 0 || Mathf.Abs(vertical) > 0)
        {
            _characterAnimtor.SetBool("IsWalking", true);
        }
        else
        {
            _characterAnimtor.SetBool("IsWalking", false);
        }
    }

    public void PlayerJumping()
    {
        if(_characterMovement.IsPlayerOnGround())
        {
            _characterAnimtor.SetBool("IsOnGround", true);
        }
        else
        {
            _characterAnimtor.SetBool("IsOnGround", false);
        }
    }
}
