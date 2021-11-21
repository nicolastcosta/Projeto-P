using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movimentation : MonoBehaviour
{
    private GameObject unitModel;
    private CharacterController controller;
    private Animator animator;

    private float moveSpeed, turnRate;
    private float gravity = -15f;
    private float gravityVelocity;

    private Vector3 moveDirection;
    private Quaternion targetAngle;

    private Unit_Info unitInfo;

    // Start is called before the first frame update
    void Awake()
    {
        if (GetComponent<Unit_Info>())
        {
            unitInfo = GetComponent<Unit_Info>();

            if (unitInfo.model != null)
            {
                unitModel = unitInfo.model;

                if (unitInfo.animator != null)
                    animator = unitInfo.animator;
                else
                    Debug.Log("Player movement has no animator");
            }
            else
                Debug.Log("Player movement has no model");

            if (GetComponent<CharacterController>())
                controller = GetComponent<CharacterController>();
            else
                Debug.Log("Player movement needs the Character Controller");
        }
        else
            Debug.Log("Player movement needs the Unit_Info script");
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<Unit_Info>() == true && GetComponent<CharacterController>() == true)
        {
            moveSpeed = unitInfo.speed;
            animator.SetFloat("moveSpeed", moveSpeed);

            turnRate = unitInfo.turnRate;

            Gravity();
            Turn();
            Move();
        }
    }

    void Turn()
    {
        if (moveDirection != Vector3.zero)
        {
            targetAngle = Quaternion.LookRotation(moveDirection, Vector3.up);
            Quaternion angle = Quaternion.RotateTowards(unitModel.transform.rotation, targetAngle, turnRate * Time.deltaTime);
            unitModel.transform.rotation = angle;
        }
    }

    void Gravity()
    {
        gravityVelocity += Time.deltaTime * gravity;

        if (controller.isGrounded)
            gravityVelocity = 0;
    }

    void Move()
    {
        Vector2 inputDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        moveDirection = new Vector3(inputDirection.x, 0f, inputDirection.y).normalized;
        
        Vector3 velocity = moveDirection * moveSpeed + Vector3.up * gravityVelocity;

        if (velocity != Vector3.zero)
        {
            controller.Move(velocity * Time.deltaTime);

            if (animator != null)
                animator.SetBool("walking", true);
        }
        else
        {
            if (animator != null)
                animator.SetBool("walking", false);
        }
    }
}
