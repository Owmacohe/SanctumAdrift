using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public float jumpHeight;
    public float bufferDistance;

    private enum inputTypes { None, Keyboard, Controller }
    private inputTypes inputType = inputTypes.Keyboard;

    private Rigidbody rb;
    private bool wallFront, wallBack, wallRight, wallLeft;
    private bool isOnGround, isControllerConnected;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        checkControllerState();

        if (isControllerConnected)
        {
            inputType = inputTypes.Controller;
        }
    }

    private void Update()
    {
        checkControllerState();
        checkCloseness();
        transform.rotation = Quaternion.Euler(Vector3.up * transform.rotation.y);

        Vector2 controllerInput = new Vector3(Input.GetAxis("Vertical"), Input.GetAxis("Horizontal"));

        /*
        if (controllerInput.x <= 0.1)
        {
            controllerInput.x = 0;
        }

        if (controllerInput.y <= 0.1)
        {
            controllerInput.y = 0;
        }
        */

        if (controllerInput.x != 0 || controllerInput.y != 0)
        {
            inputType = inputTypes.Controller;
        }

        if (inputType.Equals(inputTypes.Controller))
        {
            if (!wallFront && controllerInput.x > 0)
            {
                transform.position += Vector3.forward * speed * 0.01f;
            }

            if (!wallBack && controllerInput.x < 0)
            {
                transform.position += -Vector3.forward * speed * 0.01f;
            }

            if (!wallRight && controllerInput.y > 0)
            {
                transform.position += Vector3.right * speed * 0.01f;
            }

            if (!wallLeft && controllerInput.y < 0)
            {
                transform.position += -Vector3.right * speed * 0.01f;
            }

            if (Input.GetButtonDown("Jump") && isOnGround)
            {
                inputType = inputTypes.Controller;

                rb.velocity += Vector3.up * jumpHeight;
            }
        }
        else
        {
            inputType = inputTypes.Keyboard;

            if (!wallFront && (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)))
            {
                inputType = inputTypes.Keyboard;

                transform.position += Vector3.forward * speed * 0.01f;
            }

            if (!wallBack && (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)))
            {
                inputType = inputTypes.Keyboard;

                transform.position += -Vector3.forward * speed * 0.01f;
            }

            if (!wallRight && (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)))
            {
                inputType = inputTypes.Keyboard;

                transform.position += Vector3.right * speed * 0.01f;
            }

            if (!wallLeft && (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)))
            {
                inputType = inputTypes.Keyboard;

                transform.position += -Vector3.right * speed * 0.01f;
            }

            if (Input.GetKeyDown(KeyCode.Space) && isOnGround)
            {
                inputType = inputTypes.Keyboard;

                rb.velocity += Vector3.up * jumpHeight;
            }
        }

        //print(wallFront + " " + wallBack + " " + wallRight + " " + wallLeft + " " + Time.time);
        print("<b>[INPUT TYPE]:</b> " + inputType);
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

    private void checkCloseness()
    {
        Vector3[] dirs = { Vector3.forward, -Vector3.forward, Vector3.right, -Vector3.right };

        foreach (Vector3 i in dirs)
        {
            if (Physics.Raycast(transform.position, transform.TransformDirection(i), bufferDistance))
            {
                if (i.Equals(Vector3.forward))
                {
                    wallFront = true;
                }
                else if (i.Equals(-Vector3.forward))
                {
                    wallBack = true;
                }
                else if (i.Equals(Vector3.right))
                {
                    wallRight = true;
                }
                else if (i.Equals(-Vector3.right))
                {
                    wallLeft = true;
                }
            }
            else
            {
                if (i.Equals(Vector3.forward))
                {
                    wallFront = false;
                }
                else if (i.Equals(-Vector3.forward))
                {
                    wallBack = false;
                }
                else if (i.Equals(Vector3.right))
                {
                    wallRight = false;
                }
                else if (i.Equals(-Vector3.right))
                {
                    wallLeft = false;
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.Equals(gameObject))
        {
            isOnGround = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.gameObject.Equals(gameObject))
        {
            isOnGround = false;
        }
    }
}
