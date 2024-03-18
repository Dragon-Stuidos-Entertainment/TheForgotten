using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectileDamage : MonoBehaviour
{
    [SerializeField] private int damage = 10;
    // Start is called before the first frame update
   
    //detects collision with enemy
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            //collision.gameObject.GetComponent<EnemyHealth>().TakeDamage(damage);
            Destroy(collision.gameObject);
        }
    }
}
