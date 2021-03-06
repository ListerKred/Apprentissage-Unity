﻿using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    public float invicibilityTimeAfterHit = 3f;
    public float invicibilityFlashDelay = 0.2f;
    public bool isInvicible = false;

    public AudioClip hitSound;
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
            AudioManager.instance.PlayClipAt(hitSound, transform.position);
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
        // Bloquer les mouvement du personnage
        PlayerMovement.instance.enabled = false;
        // jouer l'animation d'élimination
        PlayerMovement.instance.animator.SetTrigger("Death");
        // empêcher les interaction physique avec les autres éléments de la scène
        PlayerMovement.instance.rb.bodyType = RigidbodyType2D.Kinematic;
        // empêcher le deplacement de la caméra lors du GameOver
        PlayerMovement.instance.rb.velocity = Vector3.zero;
        PlayerMovement.instance.playerCollider.enabled = false;
        // Appel du menu
        GameOverManager.instance.OnPlayerDeath();
    }

    public void Respawn()
    {
        // Réactiver les mouvement du personnage
        PlayerMovement.instance.enabled = true;
        // Relancer le processus des animations
        PlayerMovement.instance.animator.SetTrigger("Respawn");
        // Redonner les mouvement au joueur
        PlayerMovement.instance.rb.bodyType = RigidbodyType2D.Dynamic;
        PlayerMovement.instance.playerCollider.enabled = true;
        // Redonner 100% des pv
        currentHealth = maxHealth;
        // Reset de la barre de vie
        healthBar.SetHealth(currentHealth);
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
