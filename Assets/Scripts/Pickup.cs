using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*  Ok here's the deal with this. Right now you click once to pick up an object and click again to put it down.
    The item can be clipped into a wall while you are holding it, but you can only drop it if the center is out of the wall.
    So if you drop it inside of a wall, it'll fly back out. I can keep working on it but for now this works.
    I can also change it to a button press instead of mouse click if we want.
*/
public class Pickup : MonoBehaviour
{

    public Transform onHand;
    private Rigidbody rigidbody;
    private bool isHeld = false;
    //private MeshRenderer mesh;

    void Start() 
    {
        rigidbody = GetComponent<Rigidbody>();
        //mesh = GetComponent<MeshRenderer>();
    }

    void OnMouseDown() {
        isHeld = !isHeld;
        if(isHeld)
        {
            rigidbody.useGravity = false;
            rigidbody.constraints = RigidbodyConstraints.FreezeAll;
            this.transform.position = onHand.transform.position;
            this.transform.parent = GameObject.Find("Player").transform;
        }
        else
        {
            this.transform.parent = null;
            rigidbody.useGravity = true;
            rigidbody.constraints = RigidbodyConstraints.None;

        }

    }
}