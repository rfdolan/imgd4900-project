using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activator : MonoBehaviour
{
    public bool enter = true;
    public bool exit = true;
    public Material cubeOffMat;
    public Material cubeOnMat;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (enter && (other.gameObject.tag == "Activatable"))
        {
            Debug.Log("activating");
            GameObject ourObject = other.gameObject;
            string name = ourObject.name;
            switch(name)
            {
                case "Activatable Cube":
                    activateCube(ourObject);
                    break;
                default:
                    break;
            }

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (exit && (other.gameObject.tag == "Activatable"))
        {
            Debug.Log("deactivating");
            GameObject ourObject = other.gameObject;
            string name = ourObject.name;
            switch(name)
            {
                case "Activatable Cube":
                    deactivateCube(ourObject);
                    break;
                default:
                    break;
            }
        }
    }

    private void activateCube(GameObject toActivate)
    {
        toActivate.GetComponent<Renderer>().material = cubeOnMat;

    }

    private void deactivateCube(GameObject toActivate)
    {
        toActivate.GetComponent<Renderer>().material = cubeOffMat;

    }

}
