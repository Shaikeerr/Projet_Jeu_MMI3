using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemyCollider : MonoBehaviour
{
    private const string PROJECTILE_TAG = "Projectile";
    public GameObject XPOrbPrefab;
    public Transform firePoint;
    public float XPSummonChance = 30f;

    private Collider enemyCollider;
    private EnemyStats enemyStats;

    private void Awake()
    {
        enemyCollider = GetComponent<Collider>();
        enemyStats = GetComponentInParent<EnemyStats>(); // Utilisez GetComponentInParent pour accéder à EnemyStats sur le parent
        if (enemyCollider == null)
        {
            Debug.LogError("Collider non trouvé sur l'ennemi.");
        }
        if (enemyStats == null)
        {
            Debug.LogError("EnemyStats non trouvé sur l'ennemi.");
        }
    }

    private void OnTriggerEnter(Collider projectile)
    {
        if (projectile.gameObject.tag == PROJECTILE_TAG)
        {
            if (enemyStats != null)
            {
                enemyStats.TakeDamage(10); // Remplacez 10 par la valeur de dégâts du projectile
            }

            // Détruire le projectile
            Destroy(projectile.gameObject);
        }
    }

    public void summonXP()
    {
        if (firePoint == null)
        {
            Debug.LogError("FirePoint non assigné dans l'inspecteur.");
            return;
        }

        if (XPSummonChance > 0)
        {
            float RandomXP = Random.Range(0, 100);
            Debug.Log(RandomXP);
            if (RandomXP <= XPSummonChance)
            {
                GameObject projectile = Instantiate(XPOrbPrefab, firePoint.position, firePoint.rotation);
            }
        }
    }
}