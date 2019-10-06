using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuHover : MonoBehaviour
{

    Text ourtext;
    // Start is called before the first frame update
    void Start()
    {
        ourtext = this.GetComponent<UnityEngine.UI.Text>();
        ourtext.material.color = Color.black;
        
    }
    public void OnPointerEnter()
    {
        ourtext.material.color = Color.red;
        ourtext.fontStyle = FontStyle.Bold;
        ourtext.fontSize = 24;
    }
    public void OnPointerExit()
    {
        ourtext.material.color = Color.black;
        ourtext.fontStyle = FontStyle.Normal;
        ourtext.fontSize = 12;
    }

}