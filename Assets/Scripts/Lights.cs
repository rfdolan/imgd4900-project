using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lights : MonoBehaviour
{
    //Colors 
    // Color hdColor = new Color(250, 248, 242); //normal white-ish color
    Color hdColor = Color.blue;
    //new Color(181, 181, 181); //normal white-ish color 
    Color odColor = Color.green;
    //new Color(177, 255, 124); //greenish color 

    //change material instead of color 
    public Material humanMaterial;
    public Material otherMaterial;
    //Renderer rend;

    public GameObject lightParent;
    Light[] lightChildren;
    //keep track of dimension 
    int dimension;
    
    // Start is called before the first frame update
    void Start()
    {
        //renderer 
        //rend = GetComponent<Renderer>();
        //rend.enabled = true;
        lightParent.GetComponent<MeshRenderer>().material = humanMaterial;
        lightChildren = lightParent.GetComponentsInChildren<Light>(true);
        changeToHuman(lightChildren);
        dimension = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //change dimension 
            dimension *= -1;

            if(dimension == 1) //we have changed to human
            {
                lightParent.GetComponent<MeshRenderer>().material = humanMaterial;
                changeToHuman(lightChildren);

            } else //assume we changed to other 
            {
                lightParent.GetComponent<MeshRenderer>().material = otherMaterial;
                changeToOther(lightChildren);
            }
        }
    }

    ///Change the colors of all lights in game to a match human dimension 
    /// <param name="l"> Parent of all room lights in Game </param>
    public void changeToHuman(Light[] l)
    {
        foreach(Light child in l)
        {
            child.color = hdColor;
            
        }
    }


    ///Change the colors of all lights in game to a match other dimension 
    /// <param name="l"> Parent of all room lights in Game </param>
    public void changeToOther(Light[] l)
    {
        foreach(Light child in l)
        {
            child.color = odColor;
        }
    }

}
