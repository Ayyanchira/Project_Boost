using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour {

    Rigidbody rigidbody;
    AudioSource engineSound;
    Boolean isEngineThrusting = false;
	// Use this for initialization
	void Start () {
        rigidbody = GetComponent<Rigidbody>();
        engineSound = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        ProcessInput();
        MakeEngineSound();
	}

    private void MakeEngineSound()
    {
        if (isEngineThrusting){
            if (!engineSound.isPlaying){
                engineSound.Play();
            }
        }else{
            if (engineSound.isPlaying)
            {
                engineSound.Pause();
            }
        }
    }

    private void ProcessInput()
    {
        if(Input.GetKey(KeyCode.Space)){
            rigidbody.AddRelativeForce(Vector3.up);
            isEngineThrusting = true;
        }else{
            isEngineThrusting = false;
        }

        if (Input.GetKey(KeyCode.A)){
            transform.Rotate(Vector3.forward);
        }else if(Input.GetKey(KeyCode.D)){
            transform.Rotate(-Vector3.forward);
        }
    }
}
