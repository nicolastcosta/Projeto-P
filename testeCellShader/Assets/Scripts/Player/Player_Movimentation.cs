using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movimentation : MonoBehaviour
{
    [SerializeField]
    private GameObject unitModel;
    private CharacterController controller;
    private Animator animator;

    [SerializeField]
    private float moveSpeed, turnRate, jumpHight;
    private float gravity = -15f;
    private float gravityVelocity, turnRateVelocity, jumpForce;

    private Vector3 moveDirection;
    private Quaternion targetAngle;


    // Start is called before the first frame update
    void Awake()
    {
        controller = GetComponent<CharacterController>();
        animator = unitModel.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Gravity();
        Turn();
        Move();
    }

    void Turn()
    {
        if (moveDirection != Vector3.zero)
        {
            targetAngle = Quaternion.LookRotation(moveDirection, Vector3.up);
            Quaternion angle = Quaternion.RotateTowards(transform.rotation, targetAngle, turnRate * Time.deltaTime);
            transform.rotation = angle;
        }
    }

    void Gravity()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (controller.isGrounded)
            {
                jumpForce = jumpHight;
                animator.SetTrigger("jump");
            }
        }

        gravityVelocity += Time.deltaTime * gravity;

        jumpForce += gravity * Time.deltaTime;
        if (jumpForce <= 0)
            jumpForce = 0;

        if (controller.isGrounded)
            gravityVelocity = 0 + jumpForce;
    }

    void Move()
    {
        Vector2 inputDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        moveDirection = new Vector3(inputDirection.x, 0f, inputDirection.y);
        
        Vector3 velocity = moveDirection * moveSpeed + Vector3.up * gravityVelocity;
        
        if (velocity != Vector3.zero)
        {
            controller.Move(velocity * Time.deltaTime);
            
            animator.SetBool("running", true);
        }
        else
            animator.SetBool("running", false);
    }
}
