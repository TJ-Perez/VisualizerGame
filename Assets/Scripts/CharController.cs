using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharController : MonoBehaviour
{
    public CharacterController characterController;
    public Camera cam;
    public UIDriver uiDriver;

    public float moveSpeed;
    public float camSpeedHor;
    public float camSpeedVert;

    private float xRotation = 0.0f;
    private float yRotation = 0.0f;

    public Vector3 playerVelocity;
    public float jumpHeight;
    public float gravityValue = -9.81f;

    public bool groundedPlayer;
    public Transform groundCheck;
    public float feetDistance = .8f;
    public LayerMask groundLayer;

    public int score;


    void Start()
    {
        cam = Camera.main;
        //if (uiDriver.menuMode == false)
            //Cursor.lockState = CursorLockMode.Locked;

    }

    void OnMouseDown()
    {
        if (uiDriver.menuMode == false)
            Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {

        if (uiDriver.menuMode == false)
        {
            PlayerMovement();
            CameraUpdate();
            CheckDeath();

        }
    }

    void PlayerMovement()
    {
        GroundCheck();

        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0;
        }

        Vector3 movement = Vector3.zero;
        movement += Input.GetAxis("Horizontal") * moveSpeed * transform.right;
        movement += Input.GetAxis("Vertical") * moveSpeed * transform.forward;

        movement.y = 0;

        characterController.Move(movement * Time.deltaTime);


        if (Input.GetButtonDown("Jump") && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
            //playerVelocity.y += 10;
            Debug.Log("jump attempt");
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        characterController.Move(playerVelocity * Time.deltaTime);


    }


    void CameraUpdate()
    {

        float mouseX = Input.GetAxis("Mouse X") * camSpeedHor;
        float mouseY = Input.GetAxis("Mouse Y") * camSpeedVert;

        yRotation += mouseX;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90, 90);

        gameObject.transform.eulerAngles = new Vector3(xRotation, yRotation, 0.0f);


    }


    void GroundCheck()
    {
        //RaycastHit hit;
        //if (Physics.Raycast(transform.position, Vector3.down, out hit, 1.1f))
        //{

        //    return true;
        //}
        //return false;

        //groundedPlayer = GroundCheck();
        //groundedPlayer = Physics.CheckBox(transform.position - new Vector3(0,-.8f,0), new Vector3(feetDistance,feetDistance+.9f,feetDistance), Quaternion.identity, groundLayer);
        //RaycastHit hit;
        //groundedPlayer = Physics.BoxCast(transform.position - new Vector3(0, -.8f, 0), new Vector3(feetDistance, feetDistance + .9f, feetDistance), Vector3.down, out hit, Quaternion.identity, groundLayer);
        Collider[] hitColliders = Physics.OverlapBox(transform.position - new Vector3(0, -.8f, 0), new Vector3(feetDistance, feetDistance + 1.8f, feetDistance), Quaternion.identity, groundLayer);

        groundedPlayer = false;
        foreach (Collider hit in hitColliders)
        {
            if (hit.gameObject.layer == 7)
            {
                groundedPlayer = true;
            }

            int nameValue;
            if (int.TryParse(hit.gameObject.name, out nameValue))
                if (nameValue > score)
                {
                    score = nameValue;
                }
        }


    }


    void CheckDeath()
    {
        if(transform.position.y <= -50)
        {
            Death();
        }

    }

    void Death()
    {
        Time.timeScale = 0;
        uiDriver.DeathUIEnable();
    }

}
