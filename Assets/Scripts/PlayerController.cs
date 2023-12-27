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

    // Private Rigidbody2D variable to store the reference to the Rigidbody2D component.
    Rigidbody2D rb;

    // Private Vector2 variable to store the player's movement input.
    Vector2 moveInput;

    // Public property to check if the player is currently moving.
    // It has a private set so that it can only be modified within this script.
    public bool IsMoving { get; private set; }

    // Awake is called when the script instance is being loaded.
    private void Awake()
    {
        // Get the reference to the Rigidbody2D component attached to the same GameObject.
        rb = GetComponent<Rigidbody2D>();
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
        rb.velocity = new Vector2(moveInput.x * walkSpeed, rb.velocity.y);
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
    }

    // Custom method for the "Attack" action.
    void OnAttack()
    {
        // Log a message indicating that the "Attack" action has been triggered.
        Debug.Log("Attack");
    }
}
