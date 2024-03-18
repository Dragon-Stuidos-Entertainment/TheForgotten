using UnityEngine;

namespace Props.Projectiles.Scripts
{
    public class Damage : MonoBehaviour
    {
        public int damageAmount = 10; // Amount of damage the player should take

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                P_Health playerHealth = collision.gameObject.GetComponent<P_Health>();
                if (playerHealth != null)
                {
                    playerHealth.TakeDamage(damageAmount);
                }
            }
        }
    }
}