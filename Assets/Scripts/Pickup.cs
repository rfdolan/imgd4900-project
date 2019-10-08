using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{

    public Camera cam;
    public Material highlightMat;
    public AudioSource holdingSound;
    private Rigidbody rb;
    private PlayerController dimensionScript;
    public bool handsFull;
    private GameObject objectSeen = null;
    private Material objectSeenMat = null;
    public Transform heldTransform;
    public Animator animator;
    //public float yeetSpeed;

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
        Ray highlightRay = cam.ScreenPointToRay(Input.mousePosition);
        if(Input.GetKeyDown("e") || Input.GetMouseButtonDown(0))
        {
            if(handsFull)
            {
                animator.SetTrigger("pickingUp");
                dropObject();
                return;
            }
            // See if it hit. To change the range, change the last number.
            else if (Physics.Raycast(ray, out hit, 2)&&(hit.transform.gameObject.layer == this.gameObject.layer) ){
                Transform objectHit = hit.transform;
                //Debug.Log("We hit "+ objectHit);
                if(objectHit.tag == "Liftable" || objectHit.tag == "Non-Transferrable" || objectHit.tag == "Cube")
                {
                    liftObject(objectHit);
                }
            }
        }
        
        if(!handsFull)
        {
            //Debug.Log("There is nothing in my hands.");
            // We are seeing a pick upable object
            if(Physics.Raycast(highlightRay, out hit, 2) && (hit.transform.gameObject.layer == this.gameObject.layer) && ((hit.transform.tag == "Liftable" ) || (hit.transform.tag == "Non-Transferrable") || (hit.transform.tag == "Cube")))
            {
                if(objectSeen == null)
                {
                    //Debug.Log("Highlight");
                    // Store the object and it's material so we can change it back when we stop looking.
                    objectSeen = hit.transform.gameObject;
                    Highlight();
                }
            }
            // If we just stopped looking at something, unhighlight.
            else if(objectSeen != null)
            {
                //Debug.Log("Un-Highlight");
                UnHighlight();
            }
        }
    }

    private void Highlight()
    {
        if(!objectSeen.GetComponent<Renderer>().material.name.Contains(highlightMat.name))
        {
            objectSeenMat = objectSeen.GetComponent<Renderer>().material;
        }
        objectSeen.GetComponent<Renderer>().material = highlightMat;

    }

    private void UnHighlight()
    {
        objectSeen.GetComponent<Renderer>().material = objectSeenMat;
        objectSeen = null;
        objectSeenMat = null;

    }
    private void liftObject(Transform objHit)
    {
        animator.SetTrigger("pickingUp");
        UnHighlight();
        heldTransform = objHit;
        
        holdingSound.mute = false;
        objHit.parent = null;
        
        // If we are not carrying anything, pick the item up.
        if(!handsFull)
        {
            objHit.gameObject.GetComponent<BeingHeld>().enabled = true;
            //Debug.Log("I am going to pick this up");
            //rb.constraints = RigidbodyConstraints.FreezeAll;
            //objHit.position = onHand.position;
            //objHit.parent = this.transform;
        }
        
        handsFull = !handsFull;
    }

    public void dropObject()
    {
        GameObject heldGameObject = heldTransform.gameObject;
        holdingSound.mute = true;
        //Debug.Log("Drop it mr);
        handsFull = !handsFull;
        heldGameObject.GetComponent<BeingHeld>().enabled = false;
        //heldGameObject.GetComponent<Rigidbody>().velocity = heldGameObject.GetComponent<Rigidbody>().velocity.normalized * yeetSpeed;
        if(heldTransform.tag == "Cube")
        {
            heldTransform = null;
                return;
        }
        heldTransform = null;
        if(dimensionScript.dimension == 1)
        {
            //Debug.Log(objHolding);
            heldGameObject.GetComponent<Transform>().parent = GameObject.FindWithTag("HumanDim").GetComponent<Transform>();
        }
        else
        {
            heldGameObject.GetComponent<Transform>().parent = GameObject.FindWithTag("OtherDim").GetComponent<Transform>();

        }
        
        //rb.constraints = RigidbodyConstraints.None;
        

    }
}