using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    Rigidbody rigidbody;
    const float thrust = 1000;
    const float torque = 50;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInput();
    }

    private void ProcessInput()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rigidbody.AddRelativeForce(Vector3.up * Time.deltaTime * thrust);
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(-Vector3.forward * Time.deltaTime * torque);
        } 
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(Vector3.forward * Time.deltaTime * torque);
        }
    }
}
