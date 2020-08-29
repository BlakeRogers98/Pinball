using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class LauncherScript : MonoBehaviour
{
    public float compressSpeed;
    public float expandSpeed;

    private Rigidbody launcher;
    private Vector3 rotation;
    

    // Start is called before the first frame update
    void Start()
    {
        launcher = GetComponent<Rigidbody>();
        float x = launcher.transform.eulerAngles.x;
        float y =launcher.transform.eulerAngles.y;
        float z =launcher.transform.eulerAngles.z;
        rotation = new Vector3(x, y, z);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //Vector3 movement = new Vector3(movementX, 0.0F, movementY);

            launcher.AddForce(rotation * compressSpeed);
        }
        else {
            //Vector3 movement = new Vector3(movementX, 0.0F, movementY);

            launcher.AddForce(rotation * expandSpeed);
        }
    }
}
