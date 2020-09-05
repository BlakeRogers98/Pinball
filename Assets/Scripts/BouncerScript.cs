using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncerScript : MonoBehaviour
{
  
    // Start is called before the first frame update
    void Start()
    {
        
    }

    float explosionStrength = 1500f;

    void OnCollisionEnter(Collision _other)
    {
        _other.rigidbody.AddExplosionForce(explosionStrength, this.transform.position, 5);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
