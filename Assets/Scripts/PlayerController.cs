using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Haha I'm ignoring the lock
// Movement based off of http://holistic3d.com/tutorials/?ytid=blO039OzUZc
public class PlayerController : MonoBehaviour
{
    public float speed = 10.0f;
    private AudioSource footfall;
    public bool isWalking = false;
    public AudioSource changeSound;
    Vector3 playerHeight;

    DimensionSwap dimensionScript; 
    public int dimension; //1 = we are in the human dimension, -1 = we are in the other (ghost/fae)
    //parent of list of objects for each dimension 
    Transform forHuman;
    Transform forOther;
    //to change light color 
    Lights lightScript;
    Light[] lightChildren;

    // Start is called before the first frame update
    void Start()
    {
        playerHeight = transform.position;
        Cursor.lockState = CursorLockMode.Locked;
        footfall = GetComponent<AudioSource>();

        dimensionScript = GameObject.Find("Player").GetComponent<DimensionSwap>(); //get dimension script
        lightScript = GameObject.Find("Player").GetComponent<Lights>();
        dimension = 1; //we start in the humna dimension

        //load human dimension first 
        //load objects
        forHuman = dimensionScript.humanObjects.GetComponentInChildren<Transform>();
        forOther = dimensionScript.otherObjects.GetComponentInChildren<Transform>();
        ActivateDim(forHuman);
        DeactivateDim(forOther);
        //make sure light is right color 
        lightChildren = lightScript.lightParent.GetComponentsInChildren<Light>(true);
        lightScript.changeToHuman(lightChildren);


    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0)
        {
            footfall.volume = 1f;
        }
        else
        {
            footfall.volume = 0.0f;
        }

        Vector3 move = new Vector3(Input.GetAxis("Horizontal"),0,Input.GetAxis("Vertical"));
        move = move * Time.deltaTime * speed;
        //transform.translate(move);
        gameObject.GetComponent<CharacterController>().Move(transform.TransformDirection(move));
        playerHeight = transform.position;
        playerHeight.y = Mathf.Clamp(playerHeight.y,1.05f,1.05f);
        transform.position = playerHeight;

        //float translation = Input.GetAxis("Vertical") * speed;
        //float straffe = Input.GetAxis("Horizontal") * speed;
        //translation *= Time.deltaTime;
        //straffe *= Time.deltaTime;
        //transform.Translate(straffe, 0, translation);
        
        if(Input.GetKeyDown("escape"))
        {
            Cursor.lockState = CursorLockMode.None;
        }//https://answers.unity.com/questions/140798/how-to-set-variable-value-of-a-different-script.html

        //for changing dimensions 
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(!changeSound.isPlaying)
            {
                changeSound.Play(0);
            }
            //get list of game objects for each dimension
            forHuman = dimensionScript.humanObjects.GetComponentInChildren<Transform>();
            forOther = dimensionScript.otherObjects.GetComponentInChildren<Transform>();
            //load children for lights 
            lightChildren = lightScript.lightParent.GetComponentsInChildren<Light>(true);


            //Change dimension
            dimension *= -1;

            if (dimension == 1) //we are in the human dimension 
            {
                //assets 
                ActivateDim(forHuman);
                DeactivateDim(forOther);

                //lights
                lightScript.changeToHuman(lightChildren);
            }
            else //assume we are in other dimension 
            {
                //assets
                ActivateDim(forOther);
                DeactivateDim(forHuman);

                //lights
                lightScript.changeToOther(lightChildren);
            }


        }
    }

    ///Activate all Game Objects in specified dimension.
    /// <param name="obj">Parent of list of objects in a dimension</param>
    void ActivateDim(Transform obj)
    {
        foreach(Transform child in obj) //get all the children of the object
        {
            child.gameObject.SetActive(true);
        }
    }

    ///Deactivates all Game Objects in specified dimension.
    /// <param name="obj">Parent of list of objects in a dimension</param>
    void DeactivateDim(Transform obj)
    {
        foreach (Transform child in obj) //get all the children of the object
        {
            child.gameObject.SetActive(false);
        }
    }

    float pushPower = 4.0f;
    void OnControllerColliderHit(ControllerColliderHit hit) {
        Rigidbody body = hit.collider.attachedRigidbody;

        if (body == null || body.isKinematic) {
            return;
        }
        if (hit.moveDirection.y < -0.3) {
            return;
        }
       

        Vector3 poushDir = new Vector3(hit.moveDirection.x,0,hit.moveDirection.z);
        body.velocity = poushDir * pushPower;
    }
}

