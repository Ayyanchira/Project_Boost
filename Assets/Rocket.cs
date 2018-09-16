using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour {

    Rigidbody rigidbody;
    AudioSource engineSound;
    [SerializeField] float rotationThrust = 150f;
    [SerializeField] float liftingThrust = 10f;
	// Use this for initialization
	void Start () {
        rigidbody = GetComponent<Rigidbody>();
        engineSound = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        Thrust();
        Rotate();
        //MakeEngineSound();
	}

    void OnCollisionEnter(Collision collision)
    {
        //foreach (ContactPoint contact in collision.contacts)
        //{
        //    Debug.DrawRay(contact.point, contact.normal, Color.white);
        //}
        //if (collision.relativeVelocity.magnitude > 2)
        //audioSource.Play();
        switch (collision.gameObject.tag)
        {
            case "Friendly":
                print("OK"); //todo remove this line
                break;

            default:
                print("Dead");
                break;
        }
    }

    //private void MakeEngineSound()
    //{
    //    if (isEngineThrusting){
    //        if (!engineSound.isPlaying){
    //            engineSound.Play();
    //        }
    //    }else{
    //        if (engineSound.isPlaying)
    //        {
    //            engineSound.Pause();
    //        }
    //    }
    //}

    private void Rotate()
    {
        rigidbody.freezeRotation = true; //take manual control of rotation
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward * rotationThrust * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-Vector3.forward * rotationThrust * Time.deltaTime);
        }
        rigidbody.freezeRotation = false;
    }

    private void Thrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rigidbody.AddRelativeForce(Vector3.up * liftingThrust * Time.deltaTime);
            if (!engineSound.isPlaying)
            {
                engineSound.Play();
            }
        }
        else
        {
            engineSound.Stop();
        }
    }
}
