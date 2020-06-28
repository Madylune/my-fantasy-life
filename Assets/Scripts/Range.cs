using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Range : MonoBehaviour
{
    private Enemy parent;

    private void Start() 
    {
        parent = GetComponentInParent<Enemy>();
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.tag == "Player")
        {
            parent.Target = other.transform;
        }    
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        if (other.tag == "Player")
        {
            parent.Target = null;
        }  
    }
}
