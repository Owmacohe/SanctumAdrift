using PixelCrushers.DialogueSystem;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float movementSpeed = 10;
    [SerializeField] float mouseRotationSpeed = 2;
    [SerializeField] float joystickRotationSpeed = 0.7f;
    [SerializeField] float jumpHeight = 5;
    [SerializeField] Vector2 cameraRotationBounds = new Vector2(5, 25);

    enum inputTypes { None, Keyboard, Controller }
    inputTypes inputType = inputTypes.None;
    inputTypes lastInputType = inputTypes.None;
    enum controllerTypes { None, Xbox, PS }
    controllerTypes controllerType = controllerTypes.None;
    int controllerCount, lastControllerCount;

    Rigidbody rb;
    GroundChecker gc;
    GameObject keyImage, buttonImage, buttonX, buttonSquare;
    bool isOnGround, lastIsOnGround, isOnWall, isJumping, isMovementPaused, isMoving;
    float defaultMouseRotationSpeed;
    GameObject playerCamera, cameraParent;
    float lastCameraRotation;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        gc = GetComponentInChildren<GroundChecker>();

        CheckInputType();
        CheckControllerType();

        StandardUISelectorElements selectorUI = FindObjectOfType<StandardUISelectorElements>();

        keyImage = selectorUI.nameText.gameObject.transform.parent.GetChild(3).gameObject;
        keyImage.SetActive(false);
        buttonImage = selectorUI.nameText.gameObject.transform.parent.GetChild(4).gameObject;
        buttonX = buttonImage.transform.GetChild(0).gameObject;
        buttonSquare = buttonImage.transform.GetChild(1).gameObject;
        buttonImage.SetActive(false);

        defaultMouseRotationSpeed = mouseRotationSpeed;
        playerCamera = Camera.main.gameObject;
        cameraParent = playerCamera.transform.parent.gameObject;
    }

    void Update()
    {
        float cameraRotation = playerCamera.transform.localRotation.eulerAngles.x;

        if (lastCameraRotation != cameraRotation)
        {
            if (cameraRotation <= cameraRotationBounds.x)
            {
                playerCamera.transform.localRotation = Quaternion.Euler(Vector3.right * cameraRotationBounds.x);
            }
            else if (cameraRotation >= cameraRotationBounds.y)
            {
                playerCamera.transform.localRotation = Quaternion.Euler(Vector3.right * cameraRotationBounds.y);
            }
            else if (cameraRotation <= (cameraRotationBounds.x + 1) || cameraRotation >= (cameraRotationBounds.y - 1))
            {
                //mouseRotationSpeed = defaultMouseRotationSpeed * 0.1f;
            }
            else
            {
                mouseRotationSpeed = defaultMouseRotationSpeed;
            }

            lastCameraRotation = cameraRotation;
        }

        Vector2 mouseInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        Vector2 controllerInputRight = new Vector2(Input.GetAxis("VerticalRight"), Input.GetAxis("HorizontalRight"));

        if (!mouseInput.Equals(Vector2.zero))
        {
            inputType = inputTypes.Keyboard;

            if (!isMovementPaused)
            {
                transform.Rotate(0, mouseInput.x * mouseRotationSpeed, 0, Space.World);

                //playerCamera.transform.Rotate(mouseInput.y * -mouseRotationSpeed, 0, 0, Space.Self);
                //cameraParent.transform.Rotate(0, mouseInput.x * mouseRotationSpeed, 0, Space.Self);
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
    }

    void FixedUpdate()
    {
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

                CheckControllerType();

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

        CheckControllerCount();

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

        if (lastIsOnGround != isOnGround)
        {
            if (isOnGround)
            {
                isJumping = false;
            }

            lastIsOnGround = isOnGround;
        }

        Vector2 controllerInputLeft = new Vector2(Input.GetAxis("Vertical"), Input.GetAxis("Horizontal"));

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            MovePlayer(inputTypes.Keyboard, transform.forward);
        }

        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            MovePlayer(inputTypes.Keyboard, -transform.forward);
        }

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            MovePlayer(inputTypes.Keyboard, transform.right);
        }

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            MovePlayer(inputTypes.Keyboard, -transform.right);
        }

        if (Input.GetKey(KeyCode.Space) && isOnGround && !isJumping)
        {
            inputType = inputTypes.Keyboard;

            isJumping = true;

            if (!isMovementPaused)
            {
                Invoke("LimitDrag", jumpHeight / 30);

                rb.velocity += Vector3.up * jumpHeight;
            }
        }

        if (controllerInputLeft.x > 0)
        {
            MovePlayer(inputTypes.Controller, transform.forward);
        }

        if (controllerInputLeft.x < 0)
        {
            MovePlayer(inputTypes.Controller, -transform.forward);
        }

        if (controllerInputLeft.y > 0)
        {
            MovePlayer(inputTypes.Controller, transform.right);
        }

        if (controllerInputLeft.y < 0)
        {
            MovePlayer(inputTypes.Controller, -transform.right);
        }

        if (Input.GetButton("Jump") && isOnGround && !isJumping)
        {
            inputType = inputTypes.Controller;

            isJumping = true;

            if (!isMovementPaused)
            {
                Invoke("LimitDrag", jumpHeight / 30);

                rb.velocity += Vector3.up * jumpHeight;
            }
        }

        //print(Time.time + " " + isOnGround);
        print("<b>[INPUT TYPE]:</b> " + inputType);
    }

    void CheckInputType()
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

    void CheckControllerType()
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

    void CheckControllerCount()
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

    void LimitDrag() { rb.drag = -3; }

    public void PauseMovement(bool value) { isMovementPaused = value; }

    void MovePlayer(inputTypes input, Vector3 dir)
    {
        //transform.rotation = Quaternion.Euler(Vector3.up * cameraParent.transform.localRotation.eulerAngles.y);

        inputType = input;

        if (!isMovementPaused && (isOnGround || (!isOnGround && !isOnWall)))
        {
            rb.position += dir * movementSpeed * 0.01f;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (!isOnGround && !other.gameObject.Equals(gc.gameObject) && ((gc.groundObject != null && !other.gameObject.Equals(gc.groundObject)) || gc.groundObject == null))
        {
            isOnWall = true;
            LimitDrag();
        }
    }
}
