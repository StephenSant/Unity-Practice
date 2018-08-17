﻿using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class CharacterMovement : MonoBehaviour
{
    #region Variables
    [Header("PLAYER VARIABLES")]
    [Header("Movement Variables")]
    public float speed = 6f;//how fast the player can move
    public float jumpSpeed = 8f;//how height the player can jump
    public float gravity = 20f;//player's gravity
    private Vector3 moveDirection = Vector3.zero;//direction the player is moving
    private CharacterController controller;//player controller component
    private Vector3 spawnPoint;//sets spawnpoint
    public bool isPaused;//weather or not the game is paused
    [Header("CAM ROTATION VARIABLES")]
    [Header("Rotational Axis")]
    public GameObject myCamera;//Gets the camera
    float rotationY = 0.0f;//float for rotation Y
    public RotationalAxis axis = RotationalAxis.MouseX;//create a public link to the rotational axis called axis and set a defualt axis
    [Header("Sensitivity")]//public floats for our x and y sensitivity
    public float sensitivityX = 500f;
    public float sensitivityY = 500f;
    [Header("Y Rotation Clamp")]//max and min Y rotation
    public float minimumY = -90.0f;
    public float maximumY = 90.0f;
    #endregion
    #region Start
    void Start()
    {
        controller = GetComponent<CharacterController>();//finds the player controller component
        myCamera = GameObject.Find("Main Camera");//finds the camera
        spawnPoint = transform.position;//sets spawnpoint
        Cursor.lockState = CursorLockMode.Locked;//locks the cursor position
        Cursor.visible = false;//makes the cursor invisible 
    }
    #endregion
    #region Update
    // Update is called once per frame
    void Update()
    {
        #region Movement
        if (controller.isGrounded)
        {
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));//sets the direction the player is going
            moveDirection = transform.TransformDirection(moveDirection);//makes the player go in a direction
            moveDirection *= speed;//makes the player go a certain speed
            if (Input.GetButton("Jump"))
            {
                moveDirection.y = jumpSpeed;//makes the player jump
            }
        }
        moveDirection.y -= gravity * Time.deltaTime;//gives gravity
        controller.Move(moveDirection * Time.deltaTime);//makes the player move
        #endregion
        #region Axis'
        //switches between which axis the camera can move
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
        //respawn the player if he fall off the world
        if (transform.position.y < -10)
        {
            transform.position = spawnPoint;
        }
        #endregion
        #region Pausing
        //pauses the game
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Time.timeScale = 1;
                isPaused = false;
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
            else
            {
                Time.timeScale = 0;
                isPaused = true;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }

        }
        #endregion
    }
    #endregion
    #region Axis functons
    void MouseXAndY()
    {
        if (Time.deltaTime == 0)//if the game is paused: lock vertical axis
        {
            rotationY = rotationY;
        }
        else
        {
            rotationY += Input.GetAxis("Mouse Y") * sensitivityY;//gets input for y axis
        }
        rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);//make the player not break his neck if he looks too far up or down
        myCamera.transform.localEulerAngles = new Vector3(-rotationY, 0, 0);//sets the cameras vertical direction
        transform.Rotate(0, Input.GetAxis("Mouse X") * (sensitivityX * 100) * Time.deltaTime, 0);//sets the horizontal direction
    }
    void MouseX()
    {
        transform.Rotate(0, Input.GetAxis("Mouse X") * (sensitivityX * 100) * Time.deltaTime, 0);//sets the horizontal direction
    }
    void MouseY()
    {
        rotationY += Input.GetAxis("Mouse Y") * sensitivityY;//gets input for y axis
        rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);//make the player not break his neck if he looks too far up or down
        myCamera.transform.localEulerAngles = new Vector3(-rotationY, 0, 0);//sets the cameras vertical direction
        if (Time.deltaTime == 0)
        {
            myCamera.transform.localEulerAngles = new Vector3(myCamera.transform.localEulerAngles.y, 0, 0);//if the game is paused: lock vertical axis
        }
    }
    #endregion

}
#region RotationalAxis
public enum RotationalAxis//different possible axis to move the camera
{
    MouseXAndY,
    MouseX,
    MouseY
}
#endregion


