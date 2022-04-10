using System;
using System.Collections;
using System.Collections.Generic;
using PixelCrushers.DialogueSystem;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed = 7;
    [SerializeField] float jumpHeight = 7;
    [SerializeField] float rotationSpeed = 0.1f;
    [SerializeField] Vector2 rotationBoundsY = new Vector2(4, 7);
    
    Vector2 direction;
    
    Rigidbody rb;
    
    Transform cam;
    float startRotationX;
    
    bool isGrounded;
    GameObject ground;
    List<GameObject> collidingObjects;

    GameObject keyImage, buttonImage, buttonX, buttonSquare;
    bool isInConversation;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        rb = GetComponent<Rigidbody>();
        
        cam = Camera.main.transform;
        startRotationX = cam.transform.localEulerAngles.x;
        
        collidingObjects = new List<GameObject>();
        
        StandardUISelectorElements selectorUI = FindObjectOfType<StandardUISelectorElements>();
        
        keyImage = selectorUI.nameText.gameObject.transform.parent.GetChild(3).gameObject;
        keyImage.SetActive(false);
        buttonImage = selectorUI.nameText.gameObject.transform.parent.GetChild(4).gameObject;
        buttonX = buttonImage.transform.GetChild(0).gameObject;
        buttonSquare = buttonImage.transform.GetChild(1).gameObject;
        buttonImage.SetActive(false);
    }

    void OnMove(InputValue input)
    {
        if (!isInConversation)
        {
            direction = speed * input.Get<Vector2>();
        }
    }

    void OnJump()
    {
        if (!isInConversation && isGrounded)
        {
            rb.velocity += Vector3.up * jumpHeight;
        }
    }

    void OnLookX(InputValue input)
    {
        if (!isInConversation)
        {
            rb.transform.Rotate(Vector3.up, input.Get<float>() * rotationSpeed);
        }
    }
    
    void OnLookY(InputValue input)
    {
        if (!isInConversation)
        {
            float currentRotationX = cam.transform.localEulerAngles.x;
            float rotationFactor = Mathf.Pow(2, -Mathf.Abs(startRotationX - currentRotationX));

            if (rotationFactor > 1)
            {
                rotationFactor = 1;
            }

            cam.transform.Rotate(Vector3.left, input.Get<float>() * rotationSpeed * rotationFactor);
        }

        if (cam.transform.localEulerAngles.x > startRotationX + rotationBoundsY.x)
        {
            cam.transform.localEulerAngles = Vector3.right * (startRotationX + rotationBoundsY.x);
        }
        else if (cam.transform.localEulerAngles.x < startRotationX - rotationBoundsY.y)
        {
            cam.transform.localEulerAngles = Vector3.right * (startRotationX - rotationBoundsY.y);
        }
    }

    void OnKeyPress() {
        keyImage.SetActive(true);
        buttonImage.SetActive(false);
    }

    void OnStickPress()
    {
        keyImage.SetActive(false);
        buttonImage.SetActive(true);
            
        buttonX.SetActive(true);
        buttonSquare.SetActive(false);
    }
    
    void OnButtonPress()
    {
        keyImage.SetActive(false);
        buttonImage.SetActive(true);
            
        buttonX.SetActive(true);
        buttonSquare.SetActive(false);
    }
    
    public void Conversation(bool convo) { isInConversation = convo; }

    void FixedUpdate()
    {
        if (!direction.Equals(Vector2.zero) && (collidingObjects.Count == 0 || collidingObjects.Contains(ground)))
        {
            rb.velocity = transform.TransformVector(new Vector3(direction.x, rb.velocity.y, direction.y));
        }
    }

    void Update()
    {
        RaycastHit hit;
        isGrounded = Physics.Raycast(transform.position, Vector3.down, out hit, 1.1f);

        if (isGrounded)
        {
            ground = hit.transform.gameObject;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        collidingObjects.Add(collision.gameObject);
    }
    
    void OnCollisionExit(Collision collision)
    {
        collidingObjects.Remove(collision.gameObject);
    }
}
