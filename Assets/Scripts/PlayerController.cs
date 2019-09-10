﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Haha I'm ignoring the lock
// Movement based off of http://holistic3d.com/tutorials/?ytid=blO039OzUZc
public class PlayerController : MonoBehaviour
{
    public float speed = 10.0f;
    private AudioSource footfall;
    public bool isWalking = false;

    DimensionSwap dimensionScript; 
    public int dimension; //1 = we are in the human dimension, -1 = we are in the other (ghost/fae)
    //parent of list of objects for each dimension 
    Transform forHuman;
    Transform forOther;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        footfall = GetComponent<AudioSource>();

        dimensionScript = GameObject.Find("Player").GetComponent<DimensionSwap>(); //get dimension script
        dimension = 1; //we start in the humna dimension

        //load human dimension first 
        forHuman = dimensionScript.humanObjects.GetComponentInChildren<Transform>();
        forOther = dimensionScript.otherObjects.GetComponentInChildren<Transform>();
        ActivateDim(forHuman);
        DeactivateDim(forOther);
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
        float translation = Input.GetAxis("Vertical") * speed;
        float straffe = Input.GetAxis("Horizontal") * speed;
        translation *= Time.deltaTime;
        straffe *= Time.deltaTime;
        transform.Translate(straffe, 0, translation);
        
        if(Input.GetKeyDown("escape"))
        {
            Cursor.lockState = CursorLockMode.None;
        }//https://answers.unity.com/questions/140798/how-to-set-variable-value-of-a-different-script.html

        //for changing dimensions 
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //get list of game objects for each dimension
            forHuman = dimensionScript.humanObjects.GetComponentInChildren<Transform>();
            forOther = dimensionScript.otherObjects.GetComponentInChildren<Transform>();

            //Change dimension
            dimension *= -1;

            if (dimension == 1) //we are in the human dimension 
            {
                ActivateDim(forHuman);
                DeactivateDim(forOther);
            }
            else //assume we are in other dimension 
            {
                ActivateDim(forOther);
                DeactivateDim(forHuman);
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
}