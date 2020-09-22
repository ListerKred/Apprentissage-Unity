using UnityEngine;

public class HealPowerUp : MonoBehaviour
{
    public int healthPoints;
    public AudioClip pickupSound;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //regarde si les pv sont au max ou non
            if (PlayerHealth.instance.currentHealth != PlayerHealth.instance.maxHealth)
            {
                AudioManager.instance.PlayClipAt(pickupSound, transform.position);
                //rend la vie au joueur
                PlayerHealth.instance.HealPlayer(healthPoints);
                Destroy(gameObject);
            }
        }
    }
}
