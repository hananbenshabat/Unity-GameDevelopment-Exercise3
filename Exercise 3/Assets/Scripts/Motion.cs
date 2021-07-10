using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Motion : MonoBehaviour
{
    // Variables
    [SerializeField] private float moveSpeed = 0;
    [SerializeField] private float walkSpeed = 10;
    [SerializeField] private float runSpeed = 25;

    private Vector3 moveDirection;
    private Vector3 velocity;

    [SerializeField] private bool isGrounded = false;
    [SerializeField] private float groundCheckDistance = 0;
    [SerializeField] private LayerMask groundMask = 0;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private float jumpHeight = 2;

    // References
    private CharacterController controller;
    private Animator anim;
    private AudioSource footStep;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponentInChildren<Animator>();
        footStep = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        isGrounded = Physics.CheckSphere(transform.position, groundCheckDistance, groundMask);

        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -25f;
        }

        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        moveDirection = new Vector3(moveX, 0, moveZ);

        moveDirection = transform.TransformDirection(moveDirection);


        if (isGrounded)
        {
            if (moveDirection != Vector3.zero && !Input.GetKey(KeyCode.LeftShift))
            {
                Walk();
            }
            else if (moveDirection != Vector3.zero && Input.GetKey(KeyCode.LeftShift))
            {
                Run();
            }
            else if (moveDirection == Vector3.zero)
            {
                Idle();
            }

            moveDirection *= moveSpeed;

            if(Input.GetKeyDown(KeyCode.Space) && !anim.GetBool("isJump"))
            {
                Jump();
            }
        }

        controller.Move(moveDirection * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }

    private void Idle()
    {
        anim.SetFloat("Speed", 0, 0.1f, Time.deltaTime);
    }

    private void Walk()
    {
        moveSpeed = walkSpeed;
        anim.SetFloat("Speed", 10, 0.1f, Time.deltaTime);

        // Walking sound
        if (!footStep.isPlaying && !anim.GetBool("isJump"))
        {
            footStep.pitch = 1;
            footStep.Play();
        }
    }

    private void Run()
    {
        moveSpeed = runSpeed;
        anim.SetFloat("Speed", 25, 0.1f, Time.deltaTime);

        // Running sound
        if (!footStep.isPlaying && !anim.GetBool("isJump"))
        {
            footStep.pitch = 2;
            footStep.Play();
        }
    }

    private void Jump()
    {
        anim.SetBool("isJump", true);
        velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
    }
}