using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    public float invicibilityTimeAfterHit = 3f;
    public float invicibilityFlashDelay = 0.2f;
    public bool isInvicible = false;

    public SpriteRenderer graphics;
    public HealthBar healthBar;
    public static PlayerHealth instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de PlayerHealth dans la scène");
            return;
        }
        instance = this;
    }
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            TakeDamage(60);
        }
    }

    // Set les pv max a 100 et ajoute les Pv au joueur
    public void HealPlayer (int amount)
    {
        if ((currentHealth + amount) > maxHealth)
        {
            currentHealth = maxHealth;
        }
        else
        {
            currentHealth += amount;
        }
        healthBar.SetHealth(currentHealth);
    }

    public void TakeDamage(int damage)
    {
        if (!isInvicible)
        {
            currentHealth -= damage;
            healthBar.SetHealth(currentHealth);

            // vérifier si le joueur st toujours vivant
            if(currentHealth <= 0)
            {
                Die();
                return;
            }


            isInvicible = true;
            StartCoroutine(InvicibilityFalsh());
            StartCoroutine(HandleInvicibilityDelay());
        }

    }
    public void Die()
    {
        Debug.Log("le joueur est éliminé");
        // Bloquer les mouvement du personnage
        PlayerMovement.instance.enabled = false;
        // jouer l'animation d'élimination
        PlayerMovement.instance.animator.SetTrigger("Death");
        // empêcher les interaction physique avec les autres éléments de la scène
        PlayerMovement.instance.rb.bodyType = RigidbodyType2D.Kinematic;
        PlayerMovement.instance.playerCollider.enabled = false;
    }
    public IEnumerator InvicibilityFalsh()
    {
        while (isInvicible)
        {
            graphics.color = new Color(1f, 1f, 1f, 0f);
            yield return new WaitForSeconds(invicibilityFlashDelay);
            graphics.color = new Color(1f, 1f, 1f, 1f);
            yield return new WaitForSeconds(invicibilityFlashDelay);
        }
    }

    public IEnumerator HandleInvicibilityDelay()
    {
        yield return new WaitForSeconds(invicibilityTimeAfterHit);
        isInvicible = false;
    }
}
