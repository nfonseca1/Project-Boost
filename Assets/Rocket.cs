﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour
{
    [SerializeField] float thrust = 1750f;
    [SerializeField] float torque = 150f;
    [SerializeField] float levelLoadDelay = 2f;

    [SerializeField] ParticleSystem mainEngineParticles;
    [SerializeField] ParticleSystem explodeParticles;
    [SerializeField] ParticleSystem successParticles;
    [SerializeField] LevelLoader levelLoader;
    [SerializeField] AudioClip successAudio;

    Rigidbody rigidbody;
    AudioSource audioSource;

    float currentVolume = 0;
    bool collisionOn = true;

    enum State { Alive, Dying, Transcending }
    State state = State.Alive;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (state == State.Alive)
        {
            RespondToThrustInput();
            RespondToRotateInput();
        }
        if (Debug.isDebugBuild)
        {
            RespondToDebugKeys();
        }
    }

    private void RespondToDebugKeys()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            collisionOn = !collisionOn;
        }
    }

    private void RespondToThrustInput()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            ManageVolume(true);
            ApplyThrust();
        }
        else
        {
            ManageVolume(false);
            mainEngineParticles.Stop();
        }
    }

    private void ManageVolume(bool status)
    {
        if (status)
        {
            var volume = Mathf.Lerp(currentVolume, 0.5f, 0.5f);
            currentVolume = volume;
            audioSource.volume = currentVolume;
        }
        else
        {
            var volume = Mathf.Lerp(currentVolume, 0, 0.5f);
            currentVolume = volume;
            audioSource.volume = currentVolume;
        }
    }

    private void ApplyThrust()
    {
        float frameThrustSpeed = thrust * Time.deltaTime;
        rigidbody.AddRelativeForce(Vector3.up * frameThrustSpeed);
        mainEngineParticles.Play();
    }

    private void RespondToRotateInput()
    {
        float frameRotationSpeed = torque * Time.deltaTime;

        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward * frameRotationSpeed);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-Vector3.forward * frameRotationSpeed);
        }
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (state != State.Alive || !collisionOn) { return; }

        switch (collision.gameObject.tag)
        {
            case "Friendly":
                print("OK");
                break;
            case "Finish":
                StartSuccessSequence();
                break;
            default:
                StartDeathSequence();
                break;
        }
    }

    private void StartSuccessSequence()
    {
        state = State.Transcending;
        successParticles.Play();
        audioSource.Stop();
        audioSource.volume = 0.5f;
        audioSource.PlayOneShot(successAudio);
        levelLoader.Invoke("LoadNextLevel", levelLoadDelay);
    }

    private void StartDeathSequence()
    {
        state = State.Dying;
        Instantiate(explodeParticles, transform.position, Quaternion.identity);
        levelLoader.RequestReloadLevel();
        Destroy(gameObject);
    }
}
