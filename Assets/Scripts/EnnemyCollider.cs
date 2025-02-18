using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEditor.MPE;
using UnityEngine;

public class EnnemyCollider : MonoBehaviour
{
    private const string PROJECTILE_TAG = "Projectile";
    public GameObject XPOrbPrefab;
    public Transform firePoint;
    public float XPSummonChance = 30f;

    private Collider enemyCollider;

    private void Awake()
    {
        enemyCollider = GetComponent<Collider>();
        if (enemyCollider == null)
        {
            Debug.LogError("Collider non trouvé sur l'ennemi.");
        }
    }

    private void OnTriggerEnter(Collider projectile)

    {
        if (projectile.gameObject.tag == PROJECTILE_TAG)
        {
            if (enemyCollider != null)
            {
                enemyCollider.enabled = false;
            }

            // Désactiver l'objet de l'ennemi
            gameObject.SetActive(false);



            summonXP();
        }
    }

    void summonXP()
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