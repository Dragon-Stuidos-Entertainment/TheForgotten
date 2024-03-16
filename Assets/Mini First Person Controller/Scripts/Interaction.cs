using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    private bool isPushing = false;

    private void Update()
    {
        if (isPushing)
        {
            // Perform pushing logic here
            Debug.Log("Player is pushing the object");
        }
    }

    private void OnTriggerStay(Collider other)
    {
        Debug.Log("Player is in trigger area with: " + other.name); // Check if OnTriggerStay is being called
        if (other.CompareTag("PushableObject"))
        {
            Debug.Log("Player is interacting with PushableObject: " + other.name); // Check if interaction with PushableObject is detected
            // Check for input to indicate player is pushing
            if (Input.GetKey(KeyCode.W))  // Assuming W key is used for pushing forward
            {
                isPushing = true;
            }
            else
            {
                isPushing = false;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("PushableObject"))
        {
            isPushing = false;
        }
    }
}