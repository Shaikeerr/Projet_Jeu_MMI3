using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileStats : MonoBehaviour
{
    public int damage;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            EnemyStats enemyStats = other.GetComponent<EnemyStats>();
            if (enemyStats != null)
            {
                enemyStats.TakeDamage(damage); 
            }

            Destroy(gameObject);
        }
    }
}