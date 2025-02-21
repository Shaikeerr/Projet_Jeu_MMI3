using System;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    [Header("Paramètres du Projectile")]
    public GameObject projectilePrefab;
    public float projectileSpeed = 20f;
    public Transform firePoint;

    [Header("Paramètres de Tir")]
    float fireRate;
    public float nextFireTime = 0f;
    public float disapearTime = 5f;

    public int projectileDamage;

    void Start()
    {
        fireRate = CharacterManager.CharacterInstance.fireRate;
    }

    void Update()
    {
        fireRate = CharacterManager.CharacterInstance.fireRate;

        if (Input.GetMouseButton(0) && Time.time >= nextFireTime)
        {
            ShootProjectile();
            nextFireTime = Time.time + 1f / fireRate;
        }
    }

    void ShootProjectile()
    {
        if (firePoint == null)
        {
            Debug.LogError("FirePoint non assigné dans l'inspecteur.");
            return;
        }

        CharacterStats playerStats = CharacterManager.CharacterInstance.GetComponent<CharacterStats>();
        if (playerStats != null)
        {
            projectileDamage = Mathf.RoundToInt(playerStats.Damage);
        }
        else
        {
            Debug.LogError("CharacterStats non trouvé sur le joueur.");
            return;
        }

        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);

        Rigidbody rb = projectile.GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.velocity = firePoint.forward * projectileSpeed;
        }
        else
        {
            Debug.LogError("Le prefab du projectile n'a pas de Rigidbody attaché.");
        }

        ProjectileStats projectileStats = projectile.GetComponent<ProjectileStats>();
        if (projectileStats != null)
        {
            projectileStats.damage = projectileDamage;
        }
        else
        {
            Debug.LogError("Le prefab du projectile n'a pas de script ProjectileStats attaché.");
        }

        Destroy(projectile, disapearTime);
    }

    void IncreaseFireRate()
    {
        fireRate += 20f;
    }

    internal void IncreaseFireRate(float fireRateIncrease, float duration)
    {
        throw new NotImplementedException();
    }

    internal void IncreaseFireRate(float fireRateIncrease)
    {
        throw new NotImplementedException();
    }
}