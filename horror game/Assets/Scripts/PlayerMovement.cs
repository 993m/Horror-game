using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public bool disabled = false;
    public CharacterController controller;
    public float defaultSpeed = 4f;
    public float sprintSpeed = 10f;
    public float speed;
    public float sprintTime = 10.0f; // total time for sprint
    private float sprintTimer; // current time for sprint

    public float gravity = -9.81f;
    Vector3 velocity;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    bool isGrounded;
    public bool isSprinting;
    public breathing breathingScript;

    public SprintBar sprintBar;

    // Start is called before the first frame update
    void Start()
    {
        speed = defaultSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (!disabled)
        {
            Gravity();

            Sprint();

            Move();

            sprintBar.UpdateSprintBar(sprintTime, sprintTimer);
        }
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
            sprintTimer -= 2*Time.deltaTime; // decrease the sprint timer
            isSprinting = true;
            breathingScript.playBreathSound();
        }
        else
        {
            if(isSprinting == true)
            {
                breathingScript.playOutOfBreathSound();
                isSprinting = false;
            }
            isSprinting = false;
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