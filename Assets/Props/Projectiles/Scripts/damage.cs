using UnityEngine;

namespace Props.Projectiles.Scripts
{
    public class Damage : MonoBehaviour
    {
        // When the projectile collides with the player, destroy the player object
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                Destroy(collision.gameObject);
            }
        }
    }
}