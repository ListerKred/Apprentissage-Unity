using UnityEngine;

public class HealPowerUp : MonoBehaviour
{
    public int healthPoints;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //regarde si les pv sont au max ou non
            if (PlayerHealth.instance.currentHealth != PlayerHealth.instance.maxHealth)
            {
                //rend la vie au joueur
                PlayerHealth.instance.HealPlayer(healthPoints);
                Destroy(gameObject);
            }
        }
    }
}
