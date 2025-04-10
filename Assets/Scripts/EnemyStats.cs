using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{

    [Header("Enemy Stats")]
    public int BaseHealth;
    public int Health;
    public int Damage;
    public float XPSummonChance = 30f;

    [Header("Enemy Color & Material")]
    private Color baseEnemyColor;
    private SkinnedMeshRenderer enemyMeshRenderer;

    [Header("XP Orb & Spawn Point")]
    public GameObject XPOrbPrefab;
    public Transform firePoint;

void Start()
{
    Health = BaseHealth;

    SkinnedMeshRenderer skinnedMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
    if (skinnedMeshRenderer != null)
    {
        enemyMeshRenderer = skinnedMeshRenderer;
        baseEnemyColor = enemyMeshRenderer.material.color;
    }
    else
    {
        Debug.LogError("SkinnedMeshRenderer not found in the current or child objects.");
    }
}

    void Update()
    {
    }

    public void TakeDamage(int damage)
    {
        Health -= damage;
        ChangeColorBasedOnHealth();
        if (Health <= 0)
        {
            summonXP();
            Destroy(gameObject);
        }
    }

    private void ChangeColorBasedOnHealth() // Change color based on health percentage to black
    {
        if (enemyMeshRenderer != null)
        {
            float healthPercentage = (float)Health / BaseHealth;
            enemyMeshRenderer.material.color = Color.Lerp(Color.black, baseEnemyColor, healthPercentage); // Color.Lerp allows for smooth transition between two colors
        }
    }

    public void summonXP()
    {
        if (firePoint == null)
        {
            Debug.LogError("FirePoint non assign� dans l'inspecteur.");
            return;
        }

        if (XPSummonChance > 0)
        {
            float RandomXP = Random.Range(0, 100); // Pick a random number between 0 and 100
            if (RandomXP <= XPSummonChance) // If the random number is less than or equal to the chance, summon an XP orb
            {
                GameObject projectile = Instantiate(XPOrbPrefab, firePoint.position, firePoint.rotation);
            }
        }
    }
}