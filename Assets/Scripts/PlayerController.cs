using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour

{
    // Start is called before the first frame update


    public GameObject plyr;
    public Camera plyrCamera;
    public Rigidbody rb;


    public Vector3 moveBy;
    public Vector3 rotBy;
    public float moveSpeed;
    public float lookSpeed;
    public bool onGround = true;

    public float movementSpeed = 15;
    public Vector3 rotationSpeed = new Vector3(0, 40, 0);
    private Vector2 inputDirection;



    void Start()
    {
        plyr = gameObject;
        //plyrPos = gameObject.transform.position;
    }

    private void OnMouseDown()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    //void FixedUpdate()
    //{
    //    if (Input.GetAxis("Horizontal") != 0)
    //    {
    //        if (onGround == true)
    //        {
    //            moveBy += Vector3.left * Input.GetAxis("Horizontal") * moveSpeed;
    //        }
    //    }

    //    if (Input.GetAxis("Vertical") != 0)
    //    {
    //        if (onGround == true)
    //        {
    //            moveBy += Vector3.forward * Input.GetAxis("Horizontal") * moveSpeed;
    //        }
    //    }

    //    if (Input.GetAxis("Mouse X") != 0)
    //    {
    //        rotBy.y += Input.GetAxis("Mouse X") * lookSpeed;
    //    }

    //    if (Input.GetAxis("Mouse Y") != 0)
    //    {
    //        rotBy.x -= Input.GetAxis("Mouse Y") * lookSpeed;
    //    }



    //    UpdatePosition();
    //}

    private void Update()
    {
        Vector2 inputs = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        inputDirection = inputs.normalized;
    }

    private void FixedUpdate()
    {
        Quaternion deltaRotation = Quaternion.Euler(inputDirection.x * rotationSpeed * Time.deltaTime);
        rb.MoveRotation(rb.rotation * deltaRotation);
        rb.MovePosition(rb.position + transform.forward * movementSpeed * inputDirection.y * Time.deltaTime);
    }




    void UpdatePosition()
    {
        gameObject.transform.position += moveBy;
        gameObject.transform.eulerAngles += rotBy;
        //plyrCamera.transform.eulerAngles += rotBy;
        moveBy = Vector3.zero;
        rotBy = Vector3.zero;
    }
}
