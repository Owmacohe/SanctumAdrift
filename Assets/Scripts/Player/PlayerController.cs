using System;
using System.Collections;
using System.Collections.Generic;
using PixelCrushers.DialogueSystem;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Tooltip("Player movement speed")]
    [SerializeField] float speed = 7;
    [Tooltip("Player jump velocity")]
    [SerializeField] float jumpVelocity = 5;
    [Tooltip("Camera rotation speed")]
    [SerializeField] float rotationSpeed = 0.05f;
    [Tooltip("Global vertical camera rotation bounds")]
    [SerializeField] Vector2 rotationBoundsY = new Vector2(360, 30);
    
    Vector2 direction; // Current identity Vector2 direction of movement
    
    Rigidbody rb; // Player RigidBody

    Transform cam; // Camera Transform
    float startRotationX; // Startup x rotation of the gcamera
    Transform viewObject; // Camera parent object
    
    bool isGrounded; // Whether the player is currently on the ground
    GameObject ground; // The last hit ground object
    List<GameObject> collidingObjects; // All the objects that the player is currently colliding with

    GameObject keyImage, buttonImage, buttonX, buttonSquare; // Popup UI elements

    [HideInInspector] public bool isRotating; // Whether the camera has been set to slowly rotate around the player

    void Start()
    {
        // Hiding and locking the cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        rb = GetComponent<Rigidbody>();
        
        cam = Camera.main.transform;
        startRotationX = cam.transform.localEulerAngles.x;
        viewObject = cam.parent;
        
        collidingObjects = new List<GameObject>();
        
        // Setting the popup buttons accordingly
        StandardUISelectorElements selectorUI = FindObjectOfType<StandardUISelectorElements>();
        keyImage = selectorUI.nameText.gameObject.transform.parent.GetChild(3).gameObject;
        keyImage.SetActive(false);
        buttonImage = selectorUI.nameText.gameObject.transform.parent.GetChild(4).gameObject;
        buttonX = buttonImage.transform.GetChild(0).gameObject;
        buttonSquare = buttonImage.transform.GetChild(1).gameObject;
        buttonImage.SetActive(false);
    }

    /// <summary>
    /// Triggers when the player gets input to move
    /// </summary>
    /// <param name="input">Vector2 direction of movement</param>
    void OnMove(InputValue input)
    {
        direction = speed * input.Get<Vector2>();
    }

    /// <summary>
    /// Triggers when the player gets input to jump
    /// </summary>
    void OnJump()
    {
        if (isGrounded)
        {
            rb.velocity += Vector3.up * jumpVelocity;
        }
    }

    /// <summary>
    /// Triggers when the player gets input to look left/right
    /// </summary>
    /// <param name="input">float positive/negative direction</param>
    void OnLookX(InputValue input)
    {
        // Rotates the view parent
        // so that the camera stays at a certain distance away
        viewObject.transform.Rotate(Vector3.up, input.Get<float>() * rotationSpeed);
    }
    
    /// <summary>
    /// Triggers when the player gets input to look up/down
    /// </summary>
    /// <param name="input">float positive/negative direction</param>
    void OnLookY(InputValue input)
    {
        /*
        float currentRotationX = cam.transform.localEulerAngles.x;
        
        float rotationFactor = Mathf.Pow(1.5f, -Mathf.Abs(startRotationX - currentRotationX));

        if (rotationFactor > 1)
        {
            rotationFactor = 1;
        }
        */

        cam.transform.Rotate(Vector3.left, input.Get<float>() * rotationSpeed/* * rotationFactor*/); // Only rotates the camera

        float temp = cam.transform.eulerAngles.x;

        // Checks that the vertical rotation isn't out of the rotation bounds
        if (temp < rotationBoundsY.x && temp > 180)
        {
            cam.transform.localEulerAngles = Vector3.right * rotationBoundsY.x;
        }
        else if (temp > rotationBoundsY.y && temp < 180)
        {
            cam.transform.localEulerAngles = Vector3.right * rotationBoundsY.y;
        }
    }
    
    /// <summary>
    /// Triggers when the player gets any keyboard input
    /// </summary>
    void OnKeyPress() {
        keyImage.SetActive(true);
        buttonImage.SetActive(false);
    }

    /// <summary>
    /// Triggers when the player gets any controller stick input
    /// </summary>
    void OnStickPress()
    {
        keyImage.SetActive(false);
        buttonImage.SetActive(true);
            
        buttonX.SetActive(true);
        buttonSquare.SetActive(false);
    }
    
    /// <summary>
    /// Triggers when the player gets any controller button input
    /// </summary>
    void OnButtonPress()
    {
        keyImage.SetActive(false);
        buttonImage.SetActive(true);
            
        buttonX.SetActive(true);
        buttonSquare.SetActive(false);
    }

    void FixedUpdate()
    {
        if (!direction.Equals(Vector2.zero))
        {
            Vector3 temp = new Vector3(direction.x, rb.velocity.y, direction.y); // Gets the identity Vector3 direction to go in
            
            if (collidingObjects.Count == 0 || collidingObjects.Contains(ground))
            {
                rb.velocity = viewObject.TransformVector(temp); // Pushes the player in the direction
            }

            // Creates a directional vector and
            // slowly rotates the player towards it 
            Vector3 targetDirection = viewObject.TransformPoint(temp) - transform.position;
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, 3 * rotationSpeed, 0);
            transform.rotation = Quaternion.LookRotation(newDirection);
            
            // Ignores x and z rotation
            transform.rotation = Quaternion.Euler(Vector3.up * transform.rotation.eulerAngles.y);
        }

        // Removes drag when in the air, so that the fall is faster
        if (collidingObjects.Count > 0)
        {
            rb.drag = 5;
        }
        else
        {
            rb.drag = 0.1f;
        }

        if (isRotating)
        {
            viewObject.transform.Rotate(Vector3.up, 3 * rotationSpeed); // The camera slowly circling the player
        }
    }

    void Update()
    {
        viewObject.position = transform.position;
        
        // Checking to see if a short raycast can hit a ground object under it
        RaycastHit hit;
        isGrounded = Physics.Raycast(transform.position, Vector3.down, out hit, 0.1f);
        
        if (isGrounded)
        {
            ground = hit.transform.gameObject;
        }
    }

    /// <summary>
    /// Publicly-accessible methods to set isRotating
    /// </summary>
    /// <param name="rot">New value for isRotating</param>
    public void SetRotating(bool rot) { isRotating = rot; }

    void OnCollisionEnter(Collision collision)
    {
        collidingObjects.Add(collision.gameObject);
    }
    
    void OnCollisionExit(Collision collision)
    {
        collidingObjects.Remove(collision.gameObject);
    }
}
