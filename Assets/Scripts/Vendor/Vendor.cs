using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vendor : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        Debug.Log("Interact");
    }

    public void StopInteract()
    {
        Debug.Log("StopInteract");
    }
}
