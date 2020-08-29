using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class LauncherScript : MonoBehaviour
{

    private Rigidbody launcher;

    // Start is called before the first frame update
    void Start()
    {
        launcher = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
