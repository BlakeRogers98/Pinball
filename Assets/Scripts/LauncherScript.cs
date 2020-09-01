using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using System;

public class LauncherScript : MonoBehaviour
{
    public float compressSpeed=0;
    public float expandSpeed=0;
    public float pullbackLimit=0;
    public bool isDebug;

    //Requires object to have a RigidBody that has a frozen position and rotation
    public Rigidbody ignoreCollider1;
    public Rigidbody ignoreCollider2;

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
    private Vector3 pullbackVector;
    private bool atRest=true;

    //Debug objects
    private object debug1;
    private object debug2;
    private object debug3;
    public TextMeshProUGUI debugTextLine1;
    public TextMeshProUGUI debugTextLine2;
    public TextMeshProUGUI debugTextLine3;

    // Start is called before the first frame update
    void Start()
    {
        launcher = GetComponent<Rigidbody>();
        rotation = launcher.transform.eulerAngles;
        startPosition = launcher.transform.position;
        Direction();

        //Ignores the colliders of any objects assigned
        if (ignoreCollider1 != null)
        {
            Physics.IgnoreCollision(launcher.GetComponent<Collider>(), ignoreCollider1.GetComponent<Collider>());
        }
        if (ignoreCollider2 != null)
        {
            Physics.IgnoreCollision(launcher.GetComponent<Collider>(), ignoreCollider2.GetComponent<Collider>());
        }
    }

    //Sets the Vector3 based on the direction the object is moving
    void Direction()
    {
        float localx = startPosition.x;
        float localy = startPosition.y;
        float localz = startPosition.z;
        switch (direction)
        {
            case 1:
                moveDirection = new Vector3(1, 0, 0);
                pullbackVector = new Vector3(localx + pullbackLimit, localy, localz);
                break;
            case 2:
                moveDirection = new Vector3(-1, 0, 0);
                pullbackVector = new Vector3(localx - pullbackLimit, localy, localz);
                break;
            case 3:
                moveDirection = new Vector3(0, 1, 0);
                pullbackVector = new Vector3(localx, localy + pullbackLimit, localz);
                break;
            case 4:
                moveDirection = new Vector3(0, -1, 0);
                pullbackVector = new Vector3(localx, localy - pullbackLimit, localz);
                break;
            case 5:
                moveDirection = new Vector3(0, 0, 1);
                pullbackVector = new Vector3(localx, localy, localz + pullbackLimit);
                break;
            case 6:
                moveDirection = new Vector3(0, 0, -1);
                pullbackVector = new Vector3(localx, localy, localz - pullbackLimit);
                break;
            default:
                moveDirection = new Vector3(0, 0, 0);
                pullbackVector = new Vector3(localx, localy, localz);
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    //Fills out the debug menu
    void debug() {
        if (debug1 != null)
        {
            debugTextLine1.text = debug1.ToString();
        }
        if (debug2 != null)
        {
            debugTextLine2.text = debug2.ToString();
        }
        if (debug3 != null)
        {
            debugTextLine3.text = debug3.ToString();
        }
    }


    //Determines which function to use to determine whether the launcher is allowed to move
    bool allowedToMove(bool compress) {
        bool value=false;
        Vector3 currentPostion = launcher.transform.position;
        switch (direction) {
            case 1:
                if (compress)
                {
                    value = comparePositionsExpand(startPosition.x, currentPostion.x, true);
                }
                else {
                    value = comparePositionsContract(startPosition.x, currentPostion.x, true);
                }
                break;
            case 2:
                if (compress)
                {
                    value = comparePositionsExpand(startPosition.x, currentPostion.x, false);
                }
                else
                {
                    value = comparePositionsContract(startPosition.x, currentPostion.x, false);
                }
                break;
            case 3:
                if (compress)
                {
                    value = comparePositionsExpand(startPosition.y, currentPostion.y, true);
                }
                else
                {
                    value = comparePositionsContract(startPosition.y, currentPostion.y, true);
                }
                break;
            case 4:
                if (compress)
                {
                    value = comparePositionsExpand(startPosition.y, currentPostion.y, false);
                }
                else
                {
                    value = comparePositionsContract(startPosition.y, currentPostion.y, false);
                }
                break;
            case 5:
                if (compress)
                {
                    value = comparePositionsExpand(startPosition.z, currentPostion.z, true);
                }
                else
                {
                    value = comparePositionsContract(startPosition.z, currentPostion.z, true);
                }
                break;
            case 6:
                if (compress)
                {
                    value = comparePositionsExpand(startPosition.z, currentPostion.z, false);
                }
                else
                {
                    value = comparePositionsContract(startPosition.z, currentPostion.z, false);
                }
                break;
        }

        return value;
    }

    //Compares the position of the start and current positions and determines whether the object is allowed to move closer to the start
    private bool comparePositionsExpand(float start, float current, bool greaterThan) {
        bool value = false;

        if (start != current)
        {
            if (greaterThan)
            {
                if (start < current)
                {
                    value = true;
                }
                else if (start > current)
                {
                    transform.position = startPosition;
                }
            }
            else
            {
                if (start > current)
                {
                    value = true;
                }
                else if (start < current)
                {
                    transform.position = startPosition;
                }
            }
        }
        else {
            atRest = true;
        }
        return value;
    }

    //Compares the position of the start and current positions and determines whether the object is allowed to move closer to the pullbackLimit
    private bool comparePositionsContract(float start, float current, bool greaterThan)
    {
        bool value = false;

        if (start != current)
        {
            if (greaterThan)
            {
                if ((start + pullbackLimit) > current)
                {
                    value = true;
                }
                else if (start + pullbackLimit < current)
                {
                    transform.position = pullbackVector;
                }
            }
            else
            {
                if (start + pullbackLimit > current)
                {
                    value = true;
                }
                else if (start + pullbackLimit < current)
                {
                    transform.position = pullbackVector;
                }
            }
        }
        return value;
    }

    //Runs every physics frame
    private void FixedUpdate()
    {
        if (isDebug)
        {
            debug();
        }

        //Updates limit incase changed in editor
        Direction();

        if (startPosition == launcher.transform.position)
        {
            atRest = true;
        }

        //Determines if the launcher should move, and which direction to do so
        if (Input.GetKey(KeyCode.Space))
        {
            if (allowedToMove(false))
            {
                Vector3 movement = moveDirection * Time.deltaTime * compressSpeed;
                float localx = (float)Math.Round(movement.x * 100f) / 100f;
                float localy = (float)Math.Round(movement.y * 100f) / 100f;
                float localz = (float)Math.Round(movement.z * 100f) / 100f;
                Vector3 moveVector = new Vector3(localx, localy, localz);
                transform.position += moveVector;
                atRest = false;
            }
        }
        else {
            if (allowedToMove(true) && !atRest)
            {
                Vector3 movement = moveDirection * Time.deltaTime * expandSpeed;
                float localx = (float)Math.Round(movement.x * 100f) / 100f;
                float localy = (float)Math.Round(movement.y * 100f) / 100f;
                float localz = (float)Math.Round(movement.z * 100f) / 100f;
                Vector3 moveVector = new Vector3(localx, localy, localz);
                transform.position -= moveVector;
            } else if (launcher.transform.position != startPosition) {
                launcher.transform.position=startPosition;
            }
        }
    }
}
