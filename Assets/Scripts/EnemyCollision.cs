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
            EnemyStats enemyStats = GetComponent<EnemyStats>();
            if (playerStats != null && enemyStats !=null)
            {
                playerStats.TakeDamage(enemyStats.Damage);
            }

            // Détruire l'ennemi
            GetComponent<Collider>().enabled = false;
            gameObject.SetActive(false);

            Destroy(gameObject, 0.1f);

        }
    }
}