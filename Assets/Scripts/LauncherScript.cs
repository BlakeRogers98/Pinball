using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class LauncherScript : MonoBehaviour
{
    public float compressSpeed;
    public float expandSpeed;
    /*Sets the direction of the launcher
     * 0 = no direction
     * 1 = forward
     * 2 = backward
     * 3 = up
     * 4 = down
     * 5 = left
     * 6 = right
     */
    public int direction=0;

    private Rigidbody launcher;
    private Vector3 rotation;
    private Vector3 startPosition;
    private Vector3 moveDirection;

    // Start is called before the first frame update
    void Start()
    {
        launcher = GetComponent<Rigidbody>();
        rotation = launcher.transform.eulerAngles;
        startPosition = launcher.transform.position;
        Direction();
    }

    //Sets the Vector3 based on the direction the object is moving
    void Direction()
    {
        switch (direction)
        {
            case 1:
                moveDirection = new Vector3(1, 0, 0);
                break;
            case 2:
                moveDirection = new Vector3(-1, 0, 0);
                break;
            case 3:
                moveDirection = new Vector3(0, 1, 0);
                break;
            case 4:
                moveDirection = new Vector3(0, -1, 0);
                break;
            case 5:
                moveDirection = new Vector3(0, 0, 1);
                break;
            case 6:
                moveDirection = new Vector3(0, 0, -1);
                break;
            default:
                moveDirection = new Vector3(0, 0, 0);
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }


    //Determines whether the launcher is allowed to move, and resets to original position if move in the wrong direction
    bool allowedToMove() {
        bool value=false;
        Vector3 currentPostion = launcher.transform.position;
        switch (direction) {
            case 1:
                comparePositions(startPosition.x, currentPostion.x, true);
                break;
            case 2:
                comparePositions(startPosition.x, currentPostion.x, false);
                break;
            case 3:
                comparePositions(startPosition.y, currentPostion.y, true);
                break;
            case 4:
                comparePositions(startPosition.y, currentPostion.y, false);
                break;
            case 5:
                comparePositions(startPosition.z, currentPostion.z, true);
                break;
            case 6:
                comparePositions(startPosition.z, currentPostion.z, false);
                break;
        }

        return value;
    }

    //Compares the position of the start and current positions and determines whether the object is allowed to move
    private bool comparePositions(float start, float current, bool greaterThan) {
        bool value = false;

        if (greaterThan)
        {
            if (start > current)
            {
                value = true;
            }
            else if (start < current) {
                transform.position = startPosition;
            }
        }
        else {
            if (start < current)
            {
                value = true;
            }
            else if (start > current) {
                transform.position = startPosition;
            }
        }

        return value;
    }

    private void FixedUpdate()
    {
        //Determines if the launcher should move, and which direction to
        if (Input.GetKey(KeyCode.Space))
        {
            transform.position += moveDirection * Time.deltaTime * compressSpeed;
        }
        else {
            if (allowedToMove())
            {
                transform.position -= moveDirection * Time.deltaTime * expandSpeed;
            }
        }
    }
}
