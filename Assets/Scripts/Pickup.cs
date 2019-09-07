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

    void Start() 
    {
        dimensionScript = gameObject.GetComponent<PlayerController>();
        handsFull = false;
        cam = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
    }

    void Update()
    {
        // Create the ray and raycast that we are going to use
        RaycastHit hit;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        if(Input.GetKeyDown("e"))
        {
            // See if it hit. To change the range, change the last number.
            if (Physics.Raycast(ray, out hit, 2)) {
                Transform objectHit = hit.transform;
                Debug.Log("We hit "+ objectHit);
                if(objectHit.tag == "Liftable")
                {
                    GameObject obj = objectHit.gameObject;
                    rb = obj.GetComponent<Rigidbody>();
                    // If we are not carrying anything, pick the item up.
                    if(!handsFull)
                    {
                        Debug.Log("I am going to pick this up");
                        rb.useGravity = false;
                        rb.constraints = RigidbodyConstraints.FreezeAll;
                        objectHit.position = onHand.transform.position;
                        objectHit.parent = this.transform;
                    }
                    // We are carrying something, which is what we hit. So put it down.
                    else
                    {
                        Debug.Log("I am putting this down");
                        // When we put it down make it a part of the dimension that we put it down in.
                        if(dimensionScript.dimension == 1)
                        {
                            objectHit.parent = GameObject.FindWithTag("HumanDim").GetComponent<Transform>();
                        }
                        else
                        {
                            objectHit.parent = GameObject.FindWithTag("OtherDim").GetComponent<Transform>();

                        }
                        rb.useGravity = true;
                        rb.constraints = RigidbodyConstraints.None;

                    }
                    // Change how full out hands are.
                    handsFull = !handsFull;
                }
            }
        }
    }
}