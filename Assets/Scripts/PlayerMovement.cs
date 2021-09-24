using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public GameObject ground;

    private enum InputTypes { None, Keyboard, Controller }
    private InputTypes inputType;

    private Rigidbody rb;
    private bool isJumping, isOnGround, isControllerConnected;
    private float controllerSensitivity = 0.1f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        checkControllerState();

        if (!isControllerConnected)
        {
            inputType = InputTypes.Keyboard;
        }
        else
        {
            inputType = InputTypes.Controller;
        }
    }

    private void Update()
    {
        if (transform.rotation.x != 0 || transform.rotation.z != 0)
        {
            transform.rotation = Quaternion.Euler(Vector3.up * transform.rotation.y);
        }

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, speed);
            inputType = InputTypes.Keyboard;
        }

        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, -speed);
            inputType = InputTypes.Keyboard;
        }

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            rb.velocity = new Vector3(speed, rb.velocity.y, rb.velocity.z);
            inputType = InputTypes.Keyboard;
        }

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            rb.velocity = new Vector3(-speed, rb.velocity.y, rb.velocity.z);
            inputType = InputTypes.Keyboard;
        }

        if (Input.GetKeyDown(KeyCode.Space) && !isJumping && isOnGround)
        {
            rb.velocity = new Vector3(rb.velocity.x, speed, rb.velocity.z);
            inputType = InputTypes.Keyboard;
            isJumping = true;
        }

        checkControllerState();

        if (isControllerConnected)
        {
            Vector3 controllerVelocity = new Vector3(Input.GetAxis("Horizontal"), rb.velocity.y, Input.GetAxis("Vertical"));

            if (controllerVelocity.x > -controllerSensitivity && controllerVelocity.x < controllerSensitivity)
            {
                controllerVelocity.x = 0;
            }

            if (controllerVelocity.z > -controllerSensitivity && controllerVelocity.z < controllerSensitivity)
            {
                controllerVelocity.z = 0;
            }

            if (controllerVelocity.x != 0 || controllerVelocity.z != 0)
            {
                inputType = InputTypes.Controller;

                rb.velocity = new Vector3(controllerVelocity.x * speed, controllerVelocity.y, controllerVelocity.z * speed);
            }

            if (Input.GetButtonDown("Jump") && !isJumping && isOnGround)
            {
                rb.velocity = new Vector3(rb.velocity.x, speed, rb.velocity.z);
                isJumping = true;
            }
        }
        else
        {
            inputType = InputTypes.Keyboard;
        }

        print("<b>[INPUT TYPE]:</b> " + inputType);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.Equals(ground))
        {
            isOnGround = true;
            isJumping = false;

            rb.drag = 2 * speed;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.Equals(ground))
        {
            isOnGround = false;

            rb.drag = -(speed / 2);
        }
    }

    private void checkControllerState()
    {
        string[] controllers = Input.GetJoystickNames();

        if (controllers.Length <= 0 || controllers[0].Equals(""))
        {
            isControllerConnected = false;
        }
        else if (controllers.Length > 0 && !controllers[0].Equals(""))
        {
            isControllerConnected = true;
        }
    }
}
