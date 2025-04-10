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

            GetComponent<Collider>().enabled = false; // Disable the collider to prevent multiple hits
            gameObject.SetActive(false);

            Destroy(gameObject, 0.1f); 

        }
    }
}