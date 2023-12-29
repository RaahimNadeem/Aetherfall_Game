using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// You cannot add this script to an object without a Rigidbody2D.
// It is a good practice to add this line to ensure the presence of a Rigidbody2D.
[RequireComponent(typeof(Rigidbody2D))]

public class PlayerController : MonoBehaviour
{
    // Public variable to set the walk speed of the player.
    public float walkSpeed = 5f;
    public float runSpeed = 8f;

    // Create a private variable to decide the current speed of the player.
    public float CurrentMoveSpeed
    {
        // The get accessor is used to return the value of the variable.
        get
        {
            if(IsMoving)
            {
                // if the player is running
                if (IsRunning)
                {
                    // return the run speed
                    return runSpeed;
                }
                // if the player is not running
                else
                {
                    // return the walk speed
                    return walkSpeed;
                }
            }
            // if the player is not moving
            else
            {
                // return 0 i.e., the player is not moving/is idle 
                return 0f;
            }
        }
    }

    // Private Rigidbody2D variable to store the reference to the Rigidbody2D component.
    Rigidbody2D rb;

    // Private Vector2 variable to store the player's movement input.
    Vector2 moveInput;
    
    // SerializeField attribute to make the private variable visible in the Inspector.
    [SerializeField]
    private bool _isMoving = false;

    // Public property to check if the player is currently moving.
    // It has a private set so that it can only be modified within this script.
    public bool IsMoving { get 
        {
            // Return the value of the private variable. This will decide whether the player is moving or not.
            return _isMoving;
        } 
        // Set the value of the private variable and update the "isMoving" parameter in the Animator component.
        private set 
        {
            _isMoving = value;
            animator.SetBool("isMoving", value);
        }
    }

    // SerializeField attribute to make the private variable visible in the Inspector.
    [SerializeField]
    private bool _isRunning = false;

    public bool IsRunning
    {
        get 
        {
            return _isRunning;
        }
        set
        {
            _isRunning = value;
            animator.SetBool("isRunning", value);
        }
    }

    // Private Animator variable to store the reference to the Animator component. This is for the animation of the player sprite.
    Animator animator; 

    //to store the direction the player is facing.
    public bool _isFacingRight = true;

    public bool IsFacingRight {
        get {
            return _isFacingRight;
        }
        private set {
            // if not facing the current direction
            if (value != _isFacingRight)
            {
                // set the value of the private variable to the opposite value
                transform.localScale *= new Vector2(-1, 1);
            }
            _isFacingRight = value;

        }
    }


    // Awake is called when the script instance is being loaded.
    private void Awake()
    {
        // Get the reference to the Rigidbody2D component attached to the same GameObject.
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update.
    void Start()
    {
        // This method can be used for any initialization that needs to happen once at the start.
        // It is currently empty in this example.
    }

    // Update is called once per frame.
    void Update()
    {
        // This method is often used for non-physics-related updates.
        // It is currently empty in this example.
    }

    // FixedUpdate is called at a fixed interval and is often used for physics-related updates.
    private void FixedUpdate()
    {
        // Move the player horizontally based on the input.
        // No need to multiply by Time.deltaTime because Rigidbody2D already takes care of that.
        rb.velocity = new Vector2(moveInput.x * CurrentMoveSpeed, rb.velocity.y);
    }

    // Callback method for the "Move" input action.
    public void OnMove(InputAction.CallbackContext context)
    {
        // Log a message indicating that the "Move" action has been triggered.
        Debug.Log("Move");

        // Read the X and Y input for movement.
        moveInput = context.ReadValue<Vector2>();

        // Update the IsMoving property based on whether the input is not zero.
        IsMoving = moveInput != Vector2.zero;

        // Flip the sprite based on the direction of movement.
        SetDirection(moveInput);
    }

    // Custom method to flip the sprite based on the direction of movement.
    private void SetDirection(Vector2 moveInput)
    {
        // If the direction is to the right, set the scale to the original value.
        if (moveInput.x > 0 && !IsFacingRight)
        {
            // Face right
            IsFacingRight = true;
        }
        // If the direction is to the left, flip the sprite by setting the scale to the negative value.
        else if (moveInput.x < 0 && IsFacingRight)
        {
            // Face left
            IsFacingRight = false;
        }
    }

    

    // Custom method for the "Attack" action.
    void OnAttack()
    {
        // Log a message indicating that the "Attack" action has been triggered.
        Debug.Log("Attack");
    }

    public void OnRun(InputAction.CallbackContext context)
    {
        Debug.Log("Run");
        // if the button is pressed down 
        if (context.started)
        {
            // start running
            IsRunning = true;
        }
        // if the button is released
        else if (context.canceled)
        {
            // stop running
            IsRunning = false;
        } 
    }
}
