using PixelCrushers.DialogueSystem;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed = 1.5f;
    public float mouseRotationSpeed = 2;
    public float joystickRotationSpeed = 0.4f;
    public float jumpHeight = 5;

    private enum inputTypes { None, Keyboard, Controller }
    private inputTypes inputType = inputTypes.None;
    private inputTypes lastInputType = inputTypes.None;
    private enum controllerTypes { None, Xbox, PS }
    private controllerTypes controllerType = controllerTypes.None;
    private int controllerCount, lastControllerCount;

    private Rigidbody rb;
    private GroundChecker gc;
    private GameObject keyImage, buttonImage, buttonX, buttonSquare;
    private bool isOnGround, isOnWall, isMovementPaused;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        gc = GetComponentInChildren<GroundChecker>();

        checkInputType();
        checkControllerType();

        StandardUISelectorElements selectorUI = FindObjectOfType<StandardUISelectorElements>();

        keyImage = selectorUI.nameText.gameObject.transform.parent.GetChild(3).gameObject;
        keyImage.SetActive(false);
        buttonImage = selectorUI.nameText.gameObject.transform.parent.GetChild(4).gameObject;
        buttonX = buttonImage.transform.GetChild(0).gameObject;
        buttonSquare = buttonImage.transform.GetChild(1).gameObject;
        buttonImage.SetActive(false);
    }

    private void Update()
    {
        //print(Time.time + " " + isOnGround);

        if (!lastInputType.Equals(inputType))
        {
            if (inputType.Equals(inputTypes.Keyboard))
            {
                keyImage.SetActive(true);
                buttonImage.SetActive(false);
            }
            else if (inputType.Equals(inputTypes.Controller))
            {
                keyImage.SetActive(false);
                buttonImage.SetActive(true);

                checkControllerType();

                if (controllerType.Equals(controllerTypes.Xbox))
                {
                    buttonX.SetActive(true);
                    buttonSquare.SetActive(false);
                }
                else if (controllerType.Equals(controllerTypes.PS))
                {
                    buttonX.SetActive(false);
                    buttonSquare.SetActive(true);
                }
            }

            lastInputType = inputType;
        }

        checkControllerCount();

        if (lastControllerCount != controllerCount)
        {
            if (controllerCount < lastControllerCount)
            {
                inputType = inputTypes.Keyboard;
            }
            else
            {
                inputType = inputTypes.Controller;
            }

            lastControllerCount = controllerCount;
        }

        isOnGround = gc.isOnGround;

        if (isOnGround)
        {
            isOnWall = false;
            rb.drag = 3;
        }

        //transform.rotation = Quaternion.Euler(Vector3.up * transform.localRotation.eulerAngles.y);

        Vector2 mouseInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        Vector2 controllerInputRight = new Vector2(Input.GetAxis("VerticalRight"), Input.GetAxis("HorizontalRight"));
        Vector2 controllerInputLeft = new Vector2(Input.GetAxis("Vertical"), Input.GetAxis("Horizontal"));

        if (!mouseInput.Equals(Vector2.zero))
        {
            inputType = inputTypes.Keyboard;

            if (!isMovementPaused)
            {
                transform.Rotate(0, mouseInput.x * mouseRotationSpeed, 0, Space.World);
            }
        }

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            movePlayer(inputTypes.Keyboard, transform.forward);
        }

        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            movePlayer(inputTypes.Keyboard, -transform.forward);
        }

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            movePlayer(inputTypes.Keyboard, transform.right);
        }

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            movePlayer(inputTypes.Keyboard, -transform.right);
        }

        if (Input.GetKeyDown(KeyCode.Space) && isOnGround)
        {
            inputType = inputTypes.Keyboard;

            if (!isMovementPaused)
            {
                Invoke("limitDrag", jumpHeight / 30);

                rb.velocity += Vector3.up * jumpHeight;
            }
        }

        if (!controllerInputRight.Equals(Vector2.zero))
        {
            inputType = inputTypes.Controller;

            if (!isMovementPaused)
            {
                transform.Rotate(0, controllerInputRight.x * joystickRotationSpeed, 0, Space.World);
            }
        }

        if (controllerInputLeft.x > 0)
        {
            movePlayer(inputTypes.Controller, transform.forward);
        }

        if (controllerInputLeft.x < 0)
        {
            movePlayer(inputTypes.Controller, -transform.forward);
        }

        if (controllerInputLeft.y > 0)
        {
            movePlayer(inputTypes.Controller, transform.right);
        }

        if (controllerInputLeft.y < 0)
        {
            movePlayer(inputTypes.Controller, -transform.right);
        }

        if (Input.GetButtonDown("Jump") && isOnGround)
        {
            inputType = inputTypes.Controller;

            if (!isMovementPaused)
            {
                Invoke("limitDrag", jumpHeight / 30);

                rb.velocity += Vector3.up * jumpHeight;
            }
        }

        //print(wallFront + " " + wallBack + " " + wallRight + " " + wallLeft + " " + Time.time);
        print("<b>[INPUT TYPE]:</b> " + inputType);
    }

    private void checkInputType()
    {
        string[] controllerNames = Input.GetJoystickNames();

        foreach (string i in controllerNames)
        {
            if (i.Length > 0)
            {
                inputType = inputTypes.Controller;
            }
        }

        if (!inputType.Equals(inputTypes.Controller))
        {
            inputType = inputTypes.Keyboard;
        }
    }

    private void checkControllerType()
    {
        if (inputType.Equals(inputTypes.Controller))
        {
            string[] controllerNames = Input.GetJoystickNames();

            foreach (string i in controllerNames)
            {
                if (i.ToUpper().Contains("XBOX"))
                {
                    controllerType = controllerTypes.Xbox;
                }
            }

            if (!controllerType.Equals(controllerTypes.Xbox))
            {
                controllerType = controllerTypes.PS;
            }
        }
        else
        {
            controllerType = controllerTypes.None;
        }
    }

    private void checkControllerCount()
    {
        string[] controllerNames = Input.GetJoystickNames();
        controllerCount = 0;

        foreach (string i in controllerNames)
        {
            if (!i.Equals(""))
            {
                controllerCount++;
            }
        }
    }

    private void limitDrag() { rb.drag = -3; }

    public void pauseMovement(bool value) { isMovementPaused = value; }

    private void movePlayer(inputTypes input, Vector3 dir)
    {
        inputType = input;

        if (!isMovementPaused && (isOnGround || (!isOnGround && !isOnWall)))
        {
            rb.position += dir * movementSpeed * 0.01f;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isOnGround && !other.gameObject.Equals(gc.gameObject) && ((gc.groundObject != null && !other.gameObject.Equals(gc.groundObject)) || gc.groundObject == null))
        {
            isOnWall = true;
            limitDrag();
        }
    }
}
