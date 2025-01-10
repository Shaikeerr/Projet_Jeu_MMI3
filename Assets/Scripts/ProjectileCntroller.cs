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
