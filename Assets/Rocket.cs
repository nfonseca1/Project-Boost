using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    Rigidbody rigidbody;
    AudioSource audioSource;
    const float thrust = 1200;
    const float torque = 100;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Thrust();
        Rotate();
    }

    private void Thrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rigidbody.AddRelativeForce(Vector3.up * Time.deltaTime * thrust);
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        else
        {
            audioSource.Stop();
        }
    }

    private void Rotate()
    {
        rigidbody.freezeRotation = true;

        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(-Vector3.forward * Time.deltaTime * torque);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(Vector3.forward * Time.deltaTime * torque);
        }

        rigidbody.freezeRotation = false;
    }
}
