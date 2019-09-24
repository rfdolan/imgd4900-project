using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActScript : MonoBehaviour
{
    public GameObject Activator;
    public GameObject Door;
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
        Debug.Log("Hit something");

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

    private void GrabObj(GameObject obj)
    {
        Debug.Log("Grabbing " + obj);
        holdingSomething = !holdingSomething;
        obj.transform.position = new Vector3(this.transform.position.x, 2.0f, this.transform.position.z);

    }
}
