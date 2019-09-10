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
    private Rigidbody rb;
    public Camera cam;
    private PlayerController dimensionScript;
    private bool handsFull;
    private GameObject objHolding;

    void Start() 
    {
        dimensionScript = gameObject.GetComponent<PlayerController>();
        handsFull = false;
    }

    void Update()
    {
        // Create the ray and raycast that we are going to use
        RaycastHit hit;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        if(Input.GetKeyDown("e"))
        {
            if(handsFull)
            {
                dropObject();

            }
            // See if it hit. To change the range, change the last number.
            else if (Physics.Raycast(ray, out hit, 2)) {
                Transform objectHit = hit.transform;
                //Debug.Log("We hit "+ objectHit);
                if(objectHit.tag == "Liftable")
                {
                    liftObject(objectHit);
                    // We are carrying something, which is what we hit. So put it down.
                }
            }
        }
    }

    private void liftObject(Transform objHit)
    {
        objHolding = objHit.gameObject;
        rb = objHolding.GetComponent<Rigidbody>();
        // If we are not carrying anything, pick the item up.
        if(!handsFull)
        {
            //Debug.Log("I am going to pick this up");
            rb.useGravity = false;
            rb.constraints = RigidbodyConstraints.FreezeAll;
            objHit.position = onHand.transform.position;
            objHit.parent = this.transform;
        }
        handsFull = !handsFull;

    }

    private void dropObject()
    {
        if(dimensionScript.dimension == 1)
        {
            objHolding.GetComponent<Transform>().parent = GameObject.FindWithTag("HumanDim").GetComponent<Transform>();
        }
        else
        {
            objHolding.GetComponent<Transform>().parent = GameObject.FindWithTag("OtherDim").GetComponent<Transform>();

        }
        rb.useGravity = true;
        rb.constraints = RigidbodyConstraints.None;
        handsFull = !handsFull;
        objHolding = null;

    }
}