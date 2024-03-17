using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberGenerator : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        Debug.Log("Number: " + Random.Range(0, 100));
    }
}
