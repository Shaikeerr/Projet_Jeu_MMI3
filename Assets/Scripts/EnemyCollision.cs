using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollision : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            CharacterStats playerStats = other.gameObject.GetComponent<CharacterStats>();
            if (playerStats != null)
            {
                playerStats.TakeDamage(10);
            }

            // Détruire l'ennemi
            Destroy(gameObject);
        }
    }
}