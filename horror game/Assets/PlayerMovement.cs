using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public float speed;

    public float defaultSpeed = 4f;
    public float sprintSpeed = 10f;
    public float sprintTime = 10.0f; // total time for sprint
    private float sprintTimer; // current time for sprint

    public float gravity = -9.81f;
    Vector3 velocity;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    bool isGrounded;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Gravity();

        Sprint();

        Move();
    }

    void Move()
    {
        // miscare fata spate stanga dreapta
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);
    }

    void Sprint()
    {
        // daca playerul apasa pe shift alearga
        if (Input.GetKey(KeyCode.LeftShift) && sprintTimer > 0)
        {
            speed = sprintSpeed; // set current speed to sprint speed
            sprintTimer -= Time.deltaTime; // decrease the sprint timer
        }
        else
        {
            speed = defaultSpeed; // set current speed to default speed
            if (sprintTimer < sprintTime && !Input.GetKey(KeyCode.LeftShift))
            {
                sprintTimer += Time.deltaTime; // increase the sprint timer if it's not already at max
            }
        }
    }

    void Gravity()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        // gravitatie
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    
}