using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorBlock : MonoBehaviour
{
    /*
    
        Hello, this is some information about the script

    */
    GameObject[] collidingWith = new GameObject[2];
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionStay(Collision other)
    {
        if(other.gameObject.tag == "Door" && collidingWith[0] == null)
        {
            Debug.Log("Adding door to the list");
            collidingWith[0] = other.gameObject;
        }
        if(other.gameObject.tag == "Wall" && collidingWith[1] == null)
        {
            Debug.Log("Adding wall to the list");
            collidingWith[1] = other.gameObject;

        }
        if(collidingWith[0] != null && collidingWith[1] != null)
        {
            Debug.Log("This door should stop moving!");
            collidingWith[0].GetComponent<DoorScript>().shouldMove = false;
        }


    }
    private void OnCollisionExit(Collision other)
    {
        if(other.gameObject.tag == "Door" )
        {
            Debug.Log("Leaving collision with door");
            if(collidingWith[0] != null)
            {
                collidingWith[0].GetComponent<DoorScript>().shouldMove = true;
                collidingWith[0] = null;
            }
        }
        if(other.gameObject.tag == "Wall" && collidingWith[1] != null)
        {
            Debug.Log("Leaving collision with wall");
            if(collidingWith[0] != null)
            {
                collidingWith[0].GetComponent<DoorScript>().shouldMove = true;
            }
            collidingWith[1] = null;

        }


    }
}
