﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActScript : MonoBehaviour
{
    public GameObject Activator;
    public DoorScript Door;
    private bool holdingSomething = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }

    private void OnTriggerStay(Collider other)
    {

        if(holdingSomething)
        {
            return;
        }
        GameObject hitObj = other.transform.gameObject;
        if(hitObj == Activator)
        {
            if(!hitObj.GetComponent<BeingHeld>().enabled)
            {
                GrabObj(hitObj);
            }

        }

    }
    private void OnTriggerExit(Collider other)
    {
        if(holdingSomething){
            GameObject hitObj = other.transform.gameObject;
            if(hitObj == Activator)
            {
                if(hitObj.GetComponent<BeingHeld>().enabled)
                {
                    holdingSomething = false;
                    hitObj.GetComponent<Grabbed>().enabled = false;
                    Door.CloseDoor();
                    Debug.Log("Releasing");

                }
            }
        }
    }

    private void GrabObj(GameObject obj)
    {
        Debug.Log("Grabbing " + obj);
        holdingSomething = true;
        obj.GetComponent<Grabbed>().targetPoint = this.transform.position;
        obj.GetComponent<Grabbed>().enabled = true;
        Door.OpenDoor();

    }
}
