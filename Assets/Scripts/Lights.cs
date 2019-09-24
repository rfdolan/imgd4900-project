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

    public GameObject lightParent;
    
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
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
