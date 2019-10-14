using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActScript : MonoBehaviour
{
    public GameObject Activator;
    public DoorScript Door;
    private bool holdingSomething = false;

    // OnTriggerStay is called as long as there is something within grabbing range.
    private void OnTriggerStay(Collider other)
    {

        // If we are already holding something, return.
        if(holdingSomething)
        {
            return;
        }
        GameObject hitObj = other.transform.gameObject;
       
        
        // If the object that we are hitting is the correct activator, grab it.
        if(hitObj == Activator)
        {
            if(!hitObj.GetComponent<BeingHeld>().enabled)
            {
                GrabObj(hitObj);
            }

        }

    }

    // OnTriggerExit is called when an object leaves the grabbing range.
    private void OnTriggerExit(Collider other)
    {
        // If we are holding something, check if the thing that is leaving is the activator.
        if(holdingSomething){
            GameObject hitObj = other.transform.gameObject;
            if(hitObj == Activator)
            {
                // If the player is holding it up, let it go.
                if(hitObj.GetComponent<BeingHeld>().enabled)
                {
                    ReleaseObj(hitObj);
                }
            }
        }
    }

    // GrabObj grabs the given object and opens the corresponding door.
    private void GrabObj(GameObject obj)
    {
        holdingSomething = true;
        Vector3 target = this.transform.position;

        // We want them to target a point near the middle of the act, so adjust accordingly.
        target.y = target.y + 0.35f;
        obj.GetComponent<Grabbed>().targetPoint = target;
        obj.GetComponent<Grabbed>().enabled = true;

        // Open the corresponding door.
        Door.OpenDoor();

    }

    // ReleaseObj releases the given object and closes the corresponding door.
    private void ReleaseObj(GameObject obj)
    {
        holdingSomething = false;
        obj.GetComponent<Grabbed>().enabled = false;

        // Close the corresponding door.
        Door.CloseDoor();

    }
}
