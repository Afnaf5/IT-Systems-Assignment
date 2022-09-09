using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement: MonoBehaviour
{
    [Header("Movement Vars")]
    public float movementSpeed;

    public float groundDrag;

    public float jumpForce;
    public float jumpCD;
    public float airResistMult;
    bool readyJump;

    [Header("Controls")]
    public KeyCode jumpKey = KeyCode.Space;


    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask groundCheck;
    bool onGround;

    
    public Transform orientation;

    float horInput;
    float vertInput;

    Vector3 movementDirection;

    Rigidbody rb;

    private void Start(){


        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        readyJump = true;

    }

    private void Update ()
    {
        //raycast to see if player is on the ground
        onGround = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f);

        Color rayColor;
        if (onGround){
            rayColor = Color.red;
        }
        else {
            rayColor = Color.green;
        }

        Debug.DrawRay(transform.position, Vector3.down * (playerHeight * 0.5f + 0.2f), rayColor);
        
        
        playerInput();

        // what to do if player is on ground or not.
        if(onGround){
            rb.drag = groundDrag;
        }
        else{
            rb.drag = 0;
        }

        
    }

    private void FixedUpdate(){
        MovePlayer();
    }

    private void playerInput(){

        horInput = Input.GetAxisRaw("Horizontal");
        vertInput = Input.GetAxisRaw("Vertical");

        //jumping
        if(Input.GetKey(jumpKey) && readyJump && onGround) {
            readyJump = false;

            Jump();

            Invoke(nameof(ResetJump), jumpCD);
        }
    }

    private void MovePlayer(){
        movementDirection = orientation.forward * vertInput + orientation.right * horInput;

        if (onGround){
            rb.AddForce(movementDirection.normalized * movementSpeed * 10f, ForceMode.Force);
        }

        else if (!onGround){
            rb.AddForce(movementDirection.normalized * movementSpeed * 10f * airResistMult, ForceMode.Force);
        }

        
    }

    private void Jump(){
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void ResetJump(){
        readyJump = true;
    }
}
