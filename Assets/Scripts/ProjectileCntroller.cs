using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;


public class ProjectileController : MonoBehaviour
{
    [Header("Param�tres du Projectile")]
    public GameObject projectilePrefab;
    public float projectileSpeed = 20f;
    public Transform firePoint;

    [Header("Param�tres de Tir")]
    float fireRate;
    private float nextFireTime = 0f;
    public float disapearTime = 5f;

    public int projectileDamage;

    public InputAction shootAction;

    private Coroutine shootingCoroutine;

    private float rotationSpeed = 50f; 
    private PlayerControls inputActions;
    private Vector2 aimInput; 

    void Awake()
    {
        var inputActionAsset = new InputActionAsset();
        shootAction.performed += ctx => StartShooting();
        shootAction.canceled += ctx => StopShooting();
        inputActions = new PlayerControls();
    }

    private void OnEnable()
    {
        shootAction.Enable();
        inputActions.Player.Enable();
        inputActions.Player.Aim.performed += OnAim;
        inputActions.Player.Aim.canceled += OnAim;
    }

    private void OnDisable()
    {
        shootAction.Disable();
        inputActions.Player.Aim.performed -= OnAim;
        inputActions.Player.Aim.canceled -= OnAim;
    }

     private void OnAim(InputAction.CallbackContext context)
    {
        aimInput = context.ReadValue<Vector2>();
    }

    void Start()
    {
        fireRate = CharacterManager.CharacterInstance.fireRate;
    }

void Update()
{
    if (Mouse.current != null && Mouse.current.delta.ReadValue().sqrMagnitude > 0.01f)
    {
        // Show the cursor when the mouse is moved
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        
        // Rotate the character using the mouse
        RotateCharacterToMouse();
    }
    else if (aimInput.sqrMagnitude > 0.01f)
    {
        // Rotate the character using the joystick
        RotateCharacter();
    }
    
    fireRate = CharacterManager.CharacterInstance.fireRate;
}

    void StartShooting()
    {
        if (shootingCoroutine == null) {
            shootingCoroutine = StartCoroutine(Shoot());
        }
    }

    void StopShooting()
    {
        if (shootingCoroutine != null)
        {
            StopCoroutine(shootingCoroutine);
            shootingCoroutine = null;
        }
    }

    IEnumerator Shoot()
    {
        while (true)
        {
            if (Time.time >= nextFireTime)
            {
                ShootProjectile();
                nextFireTime = Time.time + 1f / fireRate;
            }
            yield return new WaitForSeconds(1f / fireRate); 
        }
    }

    void ShootProjectile()
    {
        if (firePoint == null)
        {
            Debug.LogError("FirePoint non assign� dans l'inspecteur.");
            return;
        }
        CharacterStats playerStats = CharacterManager.CharacterInstance.GetComponent<CharacterStats>();
        if (playerStats != null)
        {
            projectileDamage = Mathf.RoundToInt(playerStats.Damage);
        }
        else
        {
            Debug.LogError("CharacterStats non trouv� sur le joueur.");
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
            Debug.LogError("Le prefab du projectile n'a pas de Rigidbody attach�.");
        }
        ProjectileStats projectileStats = projectile.GetComponent<ProjectileStats>();
        if (projectileStats != null)
        {
            projectileStats.damage = projectileDamage;
        }
        else
        {
            Debug.LogError("Le prefab du projectile n'a pas de script ProjectileStats attach�.");
        }
        Destroy(projectile, disapearTime);
    }
void RotateCharacterToMouse() // Rotate the character to face the mouse position (Lot of comments in this function)
{
    Vector2 mousePosition = Mouse.current.position.ReadValue(); // Get the mouse position in screen space

    Ray ray = Camera.main.ScreenPointToRay(mousePosition); // Create a ray from the camera to the mouse position

    Plane groundPlane = new Plane(Vector3.up, firePoint.position);  

    float rayDistance;

    if (groundPlane.Raycast(ray, out rayDistance))
    {
        Vector3 targetPoint = ray.GetPoint(rayDistance); // Get the point where the ray intersects the ground plane

        // Calculate direction from firePoint to the target
        Vector3 direction = (targetPoint - firePoint.position).normalized;

        if (direction.sqrMagnitude > 0.01f)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);

            // Rotate the character to align with the firePoint's direction
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                lookRotation,
                Time.deltaTime * rotationSpeed
            );
        }
    }
}

private void RotateCharacter() // Rotate the character to face the joystick direction
{
    Vector3 direction = new Vector3(aimInput.x, 0f, aimInput.y);

    if (direction.sqrMagnitude > 0.01f)
    {
        // Adjust the direction to account for the firePoint's offset
        Vector3 adjustedDirection = (firePoint.position + direction) - firePoint.position;

        Quaternion lookRotation = Quaternion.LookRotation(adjustedDirection);

        // Rotate the character smoothly
        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            lookRotation,
            Time.deltaTime * rotationSpeed
        );
    }
}
}