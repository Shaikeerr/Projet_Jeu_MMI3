using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public int BaseHealth;
    public int Health;
    public int Damage;

    private Color baseEnemyColor;
    private MeshRenderer enemyMeshRenderer;

    public GameObject XPOrbPrefab;
    public Transform firePoint;
    public float XPSummonChance = 30f;

    void Start()
    {
        Health = BaseHealth;

        enemyMeshRenderer = GetComponent<MeshRenderer>();
        if (enemyMeshRenderer != null)
        {
            baseEnemyColor = enemyMeshRenderer.material.color;
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

    private void ChangeColorBasedOnHealth()
    {
        if (enemyMeshRenderer != null)
        {
            float healthPercentage = (float)Health / BaseHealth;
            enemyMeshRenderer.material.color = Color.Lerp(Color.black, baseEnemyColor, healthPercentage);
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