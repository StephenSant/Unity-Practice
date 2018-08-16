﻿using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class CharacterMovement : MonoBehaviour
{
    #region Variables
    public bool isPaused;
    [Header("PLAYER VARIABLES")]
    [Header("Movement Variables")]
    public float speed = 6f;
    public float jumpSpeed = 8f;
    public float gravity = 20f;
    private Vector3 moveDirection = Vector3.zero;
    private CharacterController controller;
    public GameObject myCamera;
    [Header("CAM ROTATION VARIABLES")]
    [Header("Rotational Axis")]
    //create a public link to the rotational axis called axis and set a defualt axis
    public RotationalAxis axis = RotationalAxis.MouseX;
    [Header("Sensitivity")]
    //public floats for our x and y sensitivity
    public float sensitivityX = 500f;
    public float sensitivityY = 500f;
    [Header("Y Rotation Clamp")]
    //max and min Y rotation
    public float minimumY = -90.0f;
    public float maximumY = 90.0f;
    //we will have to invert our mouse position later to calculate our mouse look correctly
    //float for rotation Y
    float rotationY = 0.0f;
    private Vector3 spawnPoint;
    #endregion
    #region Start
    void Start()
    {
        controller = GetComponent<CharacterController>();
        myCamera = GameObject.Find("Main Camera");
        spawnPoint = transform.position;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    #endregion
    #region Update
    // Update is called once per frame
    void Update()
    {
        #region Movement
        if (controller.isGrounded)
        {
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;
            if (Input.GetButton("Jump"))
            {
                moveDirection.y = jumpSpeed;
            }
        }
        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);
        #endregion
        #region Axis'
        switch (axis)
        {
            case RotationalAxis.MouseX:
                MouseX();
                break;
            case RotationalAxis.MouseY:
                MouseY();
                break;
            default:
                MouseXAndY();
                break;
        }
        #endregion
        #region Respawn
        if (transform.position.y < -10)
        {
            transform.position = spawnPoint;
        }
        #endregion
        #region Pausing
        if (Input.GetKeyDown(KeyCode.Escape) && !isPaused)
        {
            Time.timeScale = 0;
            isPaused = true;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && isPaused)
        {
            Time.timeScale = 1;
            isPaused = false;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        #endregion
    }
    #endregion
    #region Axis functons
    void MouseXAndY()
    {
        if (Time.deltaTime == 0)
        {
            rotationY = rotationY;
        }
        else
        {
            //our rotation Y is plus equals  our mouse input for Mouse Y times Y sensitivity
            rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
        }
        //the rotation Y is clamped using Mathf and we are clamping the y rotation to the Y min and Y max
        rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);
        //transform our local position to the nex vector3 rotaion - y rotaion on the x axis and x rotation on the y axis
        myCamera.transform.localEulerAngles = new Vector3(-rotationY, 0, 0);
        
        transform.Rotate(0, Input.GetAxis("Mouse X") * (sensitivityX*100) * Time.deltaTime, 0);
    }
    void MouseX()
    {

        //transform the rotation on our game objects Y by our Mouse input Mouse X times X sensitivity
        transform.Rotate(0, Input.GetAxis("Mouse X") * (sensitivityX * 100) * Time.deltaTime, 0);
    }
    void MouseY()
    {
        //our rotation Y is pulse equals  our mouse input for Mouse Y times Y sensitivity
        rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
        //the rotation Y is clamped using Mathf and we are clamping the y rotation to the Y min and Y max
        rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);
        //transform our local position to the nex vector3 rotaion - y rotaion on the x axis and local euler angle Y on the y axis
        myCamera.transform.localEulerAngles = new Vector3(-rotationY, 0, 0);
        if (Time.deltaTime == 0)
        {
            myCamera.transform.localEulerAngles = new Vector3(myCamera.transform.localEulerAngles.y, 0, 0);
        }
    }
    #endregion
    
}
#region RotationalAxis
public enum RotationalAxis
{
    MouseXAndY,
    MouseX,
    MouseY
}
#endregion

