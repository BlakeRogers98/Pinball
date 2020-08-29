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
    private Vector3 position;
    private Vector3 moveDirection;

    // Start is called before the first frame update
    void Start()
    {
        launcher = GetComponent<Rigidbody>();
        rotation = launcher.transform.eulerAngles;
        position = launcher.transform.position;
        print(rotation);
        print(position);
        Direction();
    }

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

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            //Vector3 movement = new Vector3(movementX, 0.0F, movementY);

            //transform.localPosition = new Vector3(pos_x+1*compressSpeed, pos_y, pos_z);
            transform.position += moveDirection * Time.deltaTime * compressSpeed;
        }
        else {
            //Vector3 movement = new Vector3(movementX, 0.0F, movementY);
            transform.position -= moveDirection * Time.deltaTime * expandSpeed;
        }
    }
}
