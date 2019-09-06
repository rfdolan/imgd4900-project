using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activator : MonoBehaviour
{
    public bool enter = true;
    public bool exit = true;

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
        if (enter)
        {
            Debug.Log("activating");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (exit)
        {
            Debug.Log("deactivating");
        }
    }

}
