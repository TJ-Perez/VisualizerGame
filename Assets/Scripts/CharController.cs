using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharController : MonoBehaviour
{
    public CharacterController characterController;
    public Camera cam;

    public float moveSpeed;
    public float camSpeedHor;
    public float camSpeedVert;

    private float xRotation = 0.0f;
    private float yRotation = 0.0f;

    public Vector3 playerVelocity;
    private float jumpHeight = 1.0f;
    private float gravityValue = -9.81f;

    public bool groundedPlayer;



    void Start()
    {
        //characterController = GetComponent<CharacterController>();
        cam = Camera.main;
    }

    private void OnMouseDown()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }


    void Update()
    {
        // player movement - forward, backward, left, right
        float horizontal = Input.GetAxis("Horizontal") * moveSpeed;
        float vertical = Input.GetAxis("Vertical") * moveSpeed;

        characterController.Move((cam.transform.right * horizontal + cam.transform.forward * vertical) * Time.deltaTime);

        /////Jump Mechanic

        groundedPlayer = GroundCheck();

        if (!groundedPlayer)
        {
            playerVelocity.y += gravityValue * Time.deltaTime;
        }
        else if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }
        else if (Input.GetButtonDown("Jump") && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
            //playerVelocity.y += 10;
            Debug.Log("jump attempt");
        }

        //playerVelocity.y += gravityValue * Time.deltaTime;
        characterController.Move(playerVelocity * Time.deltaTime);


        // Camera Update

        float mouseX = Input.GetAxis("Mouse X") * camSpeedHor;
        float mouseY = Input.GetAxis("Mouse Y") * camSpeedVert;

        yRotation += mouseX;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90, 90);


        cam.transform.eulerAngles = new Vector3(xRotation, yRotation, 0.0f);

    }


    bool GroundCheck()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, 1.2f))
            return true;
        return false;

    }
}
