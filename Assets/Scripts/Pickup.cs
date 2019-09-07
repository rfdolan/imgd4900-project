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
    public Camera camera;
    private PlayerController dimensionScript;
    private bool handsFull;
    //private MeshRenderer mesh;

    void Start() 
    {
        dimensionScript = gameObject.GetComponent<PlayerController>();
        //mesh = GetComponent<MeshRenderer>();
        handsFull = false;
    }

    void Update()
    {
        RaycastHit hit;
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        if(Input.GetKeyDown("e"))
        {
            if (Physics.Raycast(ray, out hit)) {
                Transform objectHit = hit.transform;
                if(objectHit.tag == "Liftable")
                {
                    

                    GameObject obj = objectHit.gameObject;
                    rigidbody = obj.GetComponent<Rigidbody>();
                    if(!handsFull)
                    {
                        Debug.Log("I am going to pick this up");
                        rigidbody.useGravity = false;
                        rigidbody.constraints = RigidbodyConstraints.FreezeAll;
                        objectHit.position = onHand.transform.position;
                        objectHit.parent = this.transform;
                    }
                    else
                    {
                        Debug.Log("I am putting this down");
                        if(dimensionScript.dimension == 1)
                        {
                            objectHit.parent = GameObject.FindWithTag("HumanDim").GetComponent<Transform>();
                        }
                        else
                        {
                            objectHit.parent = GameObject.FindWithTag("OtherDim").GetComponent<Transform>();

                        }
                        rigidbody.useGravity = true;
                        rigidbody.constraints = RigidbodyConstraints.None;

                    }
                    handsFull = !handsFull;
                    Debug.Log("We hit "+ objectHit);
                    //objectHit.parent = GetComponent<GameObject>();
                }
            }
        }
    }

/*
    void OnMouseDown() {
        Debug.Log("We clicked");
        isHeld = !isHeld;
        if(isHeld)
        {
            rigidbody.useGravity = false;
            rigidbody.constraints = RigidbodyConstraints.FreezeAll;
            this.transform.position = onHand.transform.position;
            this.transform.parent = GameObject.Find("Player").transform;
            Debug.Log("isHeld is true, so we just picked up");
        }
        else
        {
            this.transform.parent = null;
            rigidbody.useGravity = true;
            rigidbody.constraints = RigidbodyConstraints.None;
            Debug.Log("isHeld is false, so we should put down");

        }

    }
    */
}