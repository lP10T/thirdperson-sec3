using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;



public class Player_movement : MonoBehaviour
{
    private CharacterController controller;
    [SerializeField] private float playerSpeed = 5f;
    [SerializeField] private Camera followCamera;

    [SerializeField] private float rotationSpeed = 10f;

    private Vector3 playerVelocity;
    [SerializeField] private float gravityValue = -13f;

    public bool groundPlayer;
    [SerializeField] private float jumpHeight = 2.5f;

    public Animator animator;

    public static Player_movement instance;


    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (CheckWinner.instance.isWinner)
        {
            case true:
                animator.SetBool("Victory", CheckWinner.instance.isWinner);
                break;
            case false:
                Movement();
                break;
        }
    }

    void Movement()
    {
        groundPlayer = controller.isGrounded;

        if (controller.isGrounded && playerVelocity.y < -2f)
        {
            playerVelocity.y = -1f;
        }

        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movementInput = Quaternion.Euler(0, followCamera.transform.eulerAngles.y,0) * new Vector3 (horizontalInput, 0, verticalInput);
        Vector3 movementDirection = movementInput.normalized;

        controller.Move(movementDirection * playerSpeed * Time.deltaTime);

        if (movementDirection != Vector3.zero) 
        {
            Quaternion desiredRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, rotationSpeed * Time.deltaTime);
        }

        if (Input.GetButtonDown("Jump") && groundPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3f * gravityValue);
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);

        animator.SetFloat("Speed", Mathf.Abs(movementDirection.x) + Mathf.Abs(movementDirection.z));
        animator.SetBool("Ground", controller.isGrounded);
        animator.SetTrigger("Jumping");

    }

    

    

}
