using System;
using System.Collections;
using System.Collections.Generic;
using PixelCrushers.DialogueSystem;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed = 7;
    [SerializeField] float jumpHeight = 5;
    [SerializeField] float rotationSpeed = 0.05f;
    [SerializeField] Vector2 rotationBoundsY = new Vector2(15, 10);
    
    Vector2 direction;
    
    Rigidbody rb;

    Transform cam;
    float startRotationX;
    Transform viewObject;
    
    bool isGrounded;
    GameObject ground;
    List<GameObject> collidingObjects;

    GameObject keyImage, buttonImage, buttonX, buttonSquare;

    [HideInInspector] public bool isRotating;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        rb = GetComponent<Rigidbody>();
        
        cam = Camera.main.transform;
        startRotationX = cam.transform.localEulerAngles.x;
        viewObject = cam.parent;
        
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
        direction = speed * input.Get<Vector2>();
    }

    void OnJump()
    {
        if (isGrounded)
        {
            rb.velocity += Vector3.up * jumpHeight;
        }
    }

    void OnLookX(InputValue input)
    {
        viewObject.transform.Rotate(Vector3.up, input.Get<float>() * rotationSpeed);
    }
    
    void OnLookY(InputValue input)
    {
        float currentRotationX = cam.transform.localEulerAngles.x;
        float rotationFactor = Mathf.Pow(1.5f, -Mathf.Abs(startRotationX - currentRotationX));
        rotationFactor = 1;

        if (rotationFactor > 1)
        {
            rotationFactor = 1;
        }

        cam.transform.Rotate(Vector3.left, input.Get<float>() * rotationSpeed * rotationFactor);

        if (cam.transform.localEulerAngles.x > startRotationX + rotationBoundsY.x)
        {
            print("fix down");
            cam.transform.localEulerAngles = Vector3.right * (startRotationX + rotationBoundsY.x);
        }
        else if (cam.transform.localEulerAngles.x < startRotationX - rotationBoundsY.y)
        {
            print("fix up");
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

    void FixedUpdate()
    {
        if (!direction.Equals(Vector2.zero))
        {
            Vector3 temp = new Vector3(direction.x, rb.velocity.y, direction.y);
            
            if (collidingObjects.Count == 0 || collidingObjects.Contains(ground))
            {
                rb.velocity = viewObject.TransformVector(temp);
            }

            Vector3 targetDirection = viewObject.TransformPoint(temp) - transform.position;
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, 3 * rotationSpeed, 0);
            
            transform.rotation = Quaternion.LookRotation(newDirection);
            transform.rotation = Quaternion.Euler(Vector3.up * transform.rotation.eulerAngles.y);
        }

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
            viewObject.transform.Rotate(Vector3.up, 3 * rotationSpeed);
        }
    }

    void Update()
    {
        viewObject.position = transform.position;
        
        RaycastHit hit;
        isGrounded = Physics.Raycast(transform.position, Vector3.down, out hit, 0.1f);

        if (isGrounded)
        {
            ground = hit.transform.gameObject;
        }
    }

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
