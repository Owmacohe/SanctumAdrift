using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed = 1.5f;
    public float mouseRotationSpeed = 2;
    public float joystickRotationSpeed = 0.4f;
    public float jumpHeight = 5;

    private enum inputTypes { None, Keyboard, Controller }
    private inputTypes inputType = inputTypes.Keyboard;

    private Rigidbody rb;
    private GroundChecker gc;
    private bool isOnGround, isOnWall, isMovementPaused;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        gc = GetComponentInChildren<GroundChecker>();

        string[] controllers = Input.GetJoystickNames();

        if (controllers.Length > 0 && !controllers[0].Equals(""))
        {
            inputType = inputTypes.Controller;
        }
    }

    private void Update()
    {
        isOnGround = gc.isOnGround;

        if (isOnGround)
        {
            isOnWall = false;
            rb.drag = 3;
        }

        //transform.rotation = Quaternion.Euler(Vector3.up * transform.localRotation.eulerAngles.y);

        if (!isMovementPaused)
        {
            Vector2 mouseInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
            Vector2 controllerInputRight = new Vector2(Input.GetAxis("VerticalRight"), Input.GetAxis("HorizontalRight"));
            Vector2 controllerInputLeft = new Vector2(Input.GetAxis("Vertical"), Input.GetAxis("Horizontal"));

            if (!mouseInput.Equals(Vector2.zero))
            {
                inputType = inputTypes.Keyboard;

                transform.Rotate(0, mouseInput.x * mouseRotationSpeed, 0, Space.World);
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

                Invoke("limitDrag", jumpHeight / 30);

                rb.velocity += Vector3.up * jumpHeight;
            }

            if (!controllerInputRight.Equals(Vector2.zero))
            {
                inputType = inputTypes.Controller;

                transform.Rotate(0, controllerInputRight.x * joystickRotationSpeed, 0, Space.World);
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

                Invoke("limitDrag", jumpHeight / 30);

                rb.velocity += Vector3.up * jumpHeight;
            }
        }

        //print(wallFront + " " + wallBack + " " + wallRight + " " + wallLeft + " " + Time.time);
        print("<b>[INPUT TYPE]:</b> " + inputType);
    }

    private void limitDrag() { rb.drag = -3; }

    public void pauseMovement(bool value) { isMovementPaused = value; }

    private void movePlayer(inputTypes input, Vector3 dir)
    {
        inputType = input;

        if (isOnGround || (!isOnGround && !isOnWall))
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
