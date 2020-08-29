using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class LauncherScript : MonoBehaviour
{
    public float compressSpeed;
    public float expandSpeed;
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
                if (startPosition.x < currentPostion.x)
                {
                    value = true;
                }
                else if (startPosition.x > currentPostion.x) {
                    transform.position = startPosition;
                }
                break;
            case 2:
                if (startPosition.x > currentPostion.x)
                {
                    value = true;
                }
                else if (startPosition.x < currentPostion.x)
                {
                    transform.position = startPosition;
                }
                break;
            case 3:
                if (startPosition.y < currentPostion.y)
                {
                    value = true;
                }
                else if (startPosition.y > currentPostion.y)
                {
                    transform.position = startPosition;
                }
                break;
            case 4:
                if (startPosition.y > currentPostion.y)
                {
                    value = true;
                }
                else if (startPosition.y < currentPostion.y)
                {
                    transform.position = startPosition;
                }
                break;
            case 5:
                if (startPosition.z < currentPostion.z)
                {
                    value = true;
                }
                else if (startPosition.z > currentPostion.z)
                {
                    transform.position = startPosition;
                }
                break;
            case 6:
                if (startPosition.z > currentPostion.z)
                {
                    value = true;
                }
                else if (startPosition.z < currentPostion.z)
                {
                    transform.position = startPosition;
                }
                break;
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
