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
    private EnnemyCollider ennemyCollider;

    // Start is called before the first frame update
    void Start()
    {
        Health = BaseHealth;

        // Utilisez GetComponentInChildren pour accéder au MeshRenderer dans le sous-élément
        enemyMeshRenderer = GetComponent<MeshRenderer>();
        if (enemyMeshRenderer != null)
        {
            baseEnemyColor = enemyMeshRenderer.material.color;
        }

        ennemyCollider = GetComponent<EnnemyCollider>();


        ChangeColorBasedOnHealth();
    }

    // Update is called once per frame
    void Update()
    {
        // Appel de ChangeColorBasedOnHealth pour tester
        ChangeColorBasedOnHealth();
    }

    public void TakeDamage(int damage)
    {
        Health -= damage;
        ChangeColorBasedOnHealth();
        if (Health <= 0)
        {
            if (ennemyCollider != null)
            {
                ennemyCollider.summonXP();
            }

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
}