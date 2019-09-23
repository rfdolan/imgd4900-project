﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public float doorSpeed;
    Animator anim;
    AudioSource audioData;
    private bool isOpen;
    private Rigidbody rb;
    private Vector3 openPos;
    private Vector3 closedPos;

    // Start is called before the first frame update
    void Start()
    {
        isOpen = false;
         
        anim = GetComponent<Animator>();
       // anim.enabled = false;
       
       audioData = GetComponent<AudioSource>();
       rb = GetComponent<Rigidbody>();
       openPos = rb.position;
       closedPos = new Vector3(openPos.x-2, openPos.y, openPos.z);
       // Start with every door open and then close them.
       CloseDoor();

    }

    // Update is called once per frame
    void Update()
    {
        // Simple user controlled opening and closing of the door.
        if(Input.GetKeyDown("b"))
        {
            if(isOpen)
            {
                CloseDoor();
            }
            else
            {
                OpenDoor();
            }
        }
        // If the door is going to go too far, stop.
        if(((rb.position.x > openPos.x) && isOpen) || ((rb.position.x < closedPos.x) && !isOpen))
        {
            rb.velocity = new Vector3(0,0,0);
        }
        // If the door is trying to close but something is in the way, keep trying to close.
        else if(!isOpen && rb.position.x > closedPos.x)
        {
            rb.velocity = new Vector3(-doorSpeed, 0,0);

        }
        
    }


    private void OnTriggerEnter(Collider other)
    {
        /* 
        //Debug.Log("has collieded");
        //anim.enabled = true;
        if (other.CompareTag("Player") && !isOpen)
        {
            //Debug.Log("collided with player");
            OpenDoor();
        }
            // Vector3 move = new Vector3(0.45f, 0.0f, 0.01f);
        //move = move.normalized; //magnitute of 1 
        //move *= 0.5f;

        //Rigidbody body = this.GetComponent<Rigidbody>(); 
        //body.velocity = move;
        */
        
    }

    private void OnTriggerExit(Collider other)
    {
        //anim.enabled = true;
    }

    private void pauseAnimationEvent()
    {
        anim.enabled = false;
    }

    /*
    Function to open the door.
     */
    public void OpenDoor()
    {
        //anim.SetTrigger("DoorOpen");
        audioData.Play(0);
        isOpen = true;
        Vector3 currentPos = this.GetComponent<Transform>().position;
        Vector3 direction = openPos-currentPos;
        direction = direction.normalized;
        direction *= doorSpeed;
        rb.velocity = direction;
    }

    /*
    Function to close the door.
     */
    public void CloseDoor()
    {
        isOpen = false;
        Vector3 currentPos = this.GetComponent<Transform>().position;
        Vector3 direction = closedPos-currentPos;
        direction = direction.normalized;
        direction *= doorSpeed;
        rb.velocity = direction;

    }
}
