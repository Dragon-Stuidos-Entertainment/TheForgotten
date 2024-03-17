using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bookCase: MonoBehaviour
{
    
    [SerializeField] float gravity = 4f;
    [SerializeField] float mass = 4.5f;
    [SerializeField] float drag = 1f;
    
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>(); // Get the Rigidbody component from the object
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the object has been thrown
        if (rb.velocity.magnitude > 2)
        {
            // Apply gravity to the object
            rb.AddForce(Vector3.down * gravity, ForceMode.Acceleration);
            rb.AddForce(Vector3.back * drag, ForceMode.Acceleration);
            rb.AddForce(Vector3.down * mass, ForceMode.Acceleration);
           
            // Add your code here to affect the object when thrown
        }
    }
}
