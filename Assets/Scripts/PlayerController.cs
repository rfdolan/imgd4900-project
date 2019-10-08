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
    public Pickup pickupScript;
    Vector3 playerHeight;

    DimensionSwap dimensionScript; 
    public int dimension; //1 = we are in the human dimension, -1 = we are in the other (ghost/fae)
    //parent of list of objects for each dimension 
    Transform forHuman;
    Transform forOther;
    //to change light color 
    //Lights lightScript;
    //Light[] lightChildren;
    public Animator animator;


    // Start is called before the first frame update
    void Start()
    {
        playerHeight = transform.position;
        Cursor.lockState = CursorLockMode.Locked;
        footfall = GetComponent<AudioSource>();

        dimensionScript = GameObject.FindWithTag("Player").GetComponent<DimensionSwap>(); //get dimension script
        //lightScript = GameObject.Find("Player").GetComponent<Lights>();
        dimension = 1; //we start in the humna dimension

        //load human dimension first 
        //load objects
        forHuman = dimensionScript.humanObjects.GetComponentInChildren<Transform>();
        forOther = dimensionScript.otherObjects.GetComponentInChildren<Transform>();
        
        ActivateDim(forHuman);
        DeactivateDim(forOther);
        
        //make sure light is right color 
        //lightChildren = lightScript.lightParent.GetComponentsInChildren<Light>(true);
       // lightScript.changeToHuman(lightChildren);


    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0)
        {
            footfall.volume = 1f;
            animator.SetBool("isWalking", true);
            //Debug.Log("When u walkin");
        }
        else
        {
            animator.SetBool("isWalking", false);
            footfall.volume = 0.0f;
        }

        Vector3 move = new Vector3(Input.GetAxis("Horizontal"),0,Input.GetAxis("Vertical"));
        move = move * Time.deltaTime * speed;
        //transform.translate(move);
        gameObject.GetComponent<CharacterController>().Move(transform.TransformDirection(move));
        playerHeight = transform.position;
        playerHeight.y = Mathf.Clamp(playerHeight.y,1.25f,1.25f);
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
            if(changeSound.isPlaying)
            {
                return;
            }
            changeSound.Play(0);
            animator.SetTrigger("shifting");
            //get list of game objects for each dimension
            forHuman = dimensionScript.humanObjects.GetComponentInChildren<Transform>();
            forOther = dimensionScript.otherObjects.GetComponentInChildren<Transform>();
            if(pickupScript.handsFull )
            {
                if(pickupScript.heldTransform.tag == "Non-Transferrable")
                {
                    pickupScript.dropObject();
                }
            }
            //load children for lights 
           // lightChildren = lightScript.lightParent.GetComponentsInChildren<Light>(true);


            //Change dimension
            dimension *= -1;

            // Here we move the player to the correct dimension layer so they will collide with the correct things.
            // ADD THE CODE TO MAKE THE OBJECTS TRANSPARENT HERE THANKS.
            if (dimension == 1) //we are in the human dimension 
            {
                //assets 
                ActivateDim(forHuman);
                DeactivateDim(forOther);
                /*
                //lights
                //lightScript.changeToHuman(lightChildren);
                */
                this.gameObject.layer = LayerMask.NameToLayer("HumanDim");
                RenderSettings.ambientLight = new Color (.96f,0.90f,0.79f,1.0f);
            }
            else //assume we are in other dimension 
            {
                //assets
                RenderSettings.ambientLight = new Color (.99f,0.08f,0.16f,1.0f);
                ActivateDim(forOther);
                DeactivateDim(forHuman);
                /* 
                //lights
                //lightScript.changeToOther(lightChildren);
                */
                this.gameObject.layer = LayerMask.NameToLayer("OtherDim");
            }


        }
    }


    ///Activate all Game Objects in specified dimension.
    /// <param name="obj">Parent of list of objects in a dimension</param>
    void ActivateDim(Transform obj)
    {
        foreach(Transform child in obj) //get all the children of the object
        { 
            if(child.gameObject.GetComponent<MeshRenderer>() == null && child.childCount > 0) { // if the child has children of its own
                //Debug.Log("passed");
                foreach(Transform childsChild in child) {
                    if(childsChild.childCount > 0) { // if the child has children of its own
                        foreach(Transform childsChildsChild in childsChild)
                        {

                            if ( childsChild.gameObject.GetComponent<MeshRenderer>() != null ) { // if the childs child has a mesh
                                //Debug.Log("passed #3");
                                makeOpaque(childsChildsChild);
                                //break;
                            }
                        }
                    }
                    if (childsChild.gameObject.GetComponent<MeshRenderer>() != null)
                    {
                        makeOpaque(childsChild);

                    }
                }
            }
            else {
                //Debug.Log("Passed #4");
                makeOpaque( child );
            }
            /* 
            Material mat = child.gameObject.GetComponent<MeshRenderer>().material;
            mat.SetFloat("_Mode",3);
            mat.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
			mat.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
			mat.SetInt("_ZWrite", 1);
			mat.DisableKeyword("_ALPHATEST_ON");
			mat.DisableKeyword("_ALPHABLEND_ON");
			mat.DisableKeyword("_ALPHAPREMULTIPLY_ON");
			mat.renderQueue = -1;
            Color color = mat.color;
            color.a = 1.0f;
            mat.color = color;
            child.gameObject.GetComponent<MeshRenderer>().material = mat;
            //Debug.Log("heelo loser " + child.gameObject.GetComponent<MeshRenderer>().material);
            */
        }
    }

    ///Deactivates all Game Objects in specified dimension.
    /// <param name="obj">Parent of list of objects in a dimension</param>
    void DeactivateDim(Transform obj)
    {
        foreach (Transform child in obj) // get all the children of the Dimension Object
        {
            // if the child doesn't have its own mesh and it has children
            if(child.gameObject.GetComponent<MeshRenderer>() == null && child.childCount > 0) { // if the child has children of its own
                //Debug.Log("passed");
                foreach(Transform childsChild in child) {
                    //Debug.Log("passed #2");
                    //Transform childsChild = child[i];
                    if(childsChild.childCount > 0) { // if the child has children of its own
                        foreach(Transform childsChildsChild in childsChild)
                        {

                            if ( childsChild.gameObject.GetComponent<MeshRenderer>() != null ) { // if the childs child has a mesh
                                //Debug.Log("passed #3");
                                makeTransparent(childsChildsChild);
                                //break;
                            }
                        }
                    }
                    if (childsChild.gameObject.GetComponent<MeshRenderer>() != null)
                    {
                        makeTransparent(childsChild);

                    }
                }
            }
            else {
                //Debug.Log("Passed #4");
                makeTransparent( child );
            }
            /* 
            Material mat = child.gameObject.GetComponent<MeshRenderer>().material;
            mat.SetFloat("_Mode", 3);
            mat.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
            mat.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            mat.SetInt("_ZWrite", 0);
            mat.DisableKeyword("_ALPHATEST_ON");
            mat.DisableKeyword("_ALPHABLEND_ON");
            mat.EnableKeyword("_ALPHAPREMULTIPLY_ON");
            mat.renderQueue = 3000;
            Color color = mat.color;
            color.a = 0.1f;
            mat.color = color;
            child.gameObject.GetComponent<MeshRenderer>().material = mat;
            //Debug.Log("heelo loser X2 " + child.gameObject.GetComponent<MeshRenderer>().material);
            */
        }
    }
        
    void makeOpaque(Transform child) {
        Material mat = child.gameObject.GetComponent<MeshRenderer>().material;
        mat.SetFloat("_Mode",3);
        mat.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
		mat.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
		mat.SetInt("_ZWrite", 1);
		mat.DisableKeyword("_ALPHATEST_ON");
		mat.DisableKeyword("_ALPHABLEND_ON");
		mat.DisableKeyword("_ALPHAPREMULTIPLY_ON");
		mat.renderQueue = -1;
        Color color = mat.color;
        color.a = 1.0f;
        mat.color = color;
        child.gameObject.GetComponent<MeshRenderer>().material = mat;
    }

    void makeTransparent(Transform child) {
        Material mat = child.gameObject.GetComponent<MeshRenderer>().material;
        mat.SetFloat("_Mode", 3);
        mat.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
        mat.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        mat.SetInt("_ZWrite", 0);
        mat.DisableKeyword("_ALPHATEST_ON");
        mat.DisableKeyword("_ALPHABLEND_ON");
        mat.EnableKeyword("_ALPHAPREMULTIPLY_ON");
        mat.renderQueue = 3000;
        Color color = mat.color;
        color.a = 0.05f;
        mat.color = color;
        child.gameObject.GetComponent<MeshRenderer>().material = mat;
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

