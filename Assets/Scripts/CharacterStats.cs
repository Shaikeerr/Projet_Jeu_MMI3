using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharacterStats : MonoBehaviour
{

    [Header("Player Stats")]
    public float XP = 0f;
    public float Level = 1f;
    public float LevelMultiplier = 1.5f;
    public float XPTillLevelUp = 2;
    public int Damage = 10;
    public float Speed = 3f;
    public int baseHealth = 100;
    public int Health = 100;
    public float fireRate = 1f;
    public float MagnetRange = 0f;


    [Header("UI Elements")]
    public GameObject levelUpPopup;
    public GameObject gameOverScreen;
    public GameObject audioManager;

    [Header("Upgrade Values")]
    [SerializeField] private int BaseHealthUpgrade = 10;
    [SerializeField] private int DamageUpgrade = 5;
    [SerializeField] private float FireRateUpgrade = 1f;
    [SerializeField] private float SpeedUpgrade = 2f;
    [SerializeField] private float MagnetRangeUpgrade = 1f;

    public void ApplyUpgrade(string upgrade)
    {
        switch (upgrade)
        {
            case "Health":
                baseHealth += BaseHealthUpgrade;
                Health = baseHealth;
                break;
            case "FireRate":
                fireRate += FireRateUpgrade;
                break;
            case "Damage":
                Damage += DamageUpgrade;
                break;
            case "Speed":
                Speed += SpeedUpgrade;
                break;
            case "Magnet":
                MagnetRange += MagnetRangeUpgrade;
                break;
            default:
                Debug.LogError("Invalid upgrade: " + upgrade);
                break;
        }
        ResumeGame();
    }

    public void TakeDamage(int damage)
    {
        Health -= damage;
        if (Health <= 0)
        {
            gameOverScreen.SetActive(true);
            audioManager.GetComponent<AudioSource>().enabled = false;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Time.timeScale = 0;
        }
    }

    public void Retry()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void Start()
    {

    }

    void Update()
    {
        if (XP >= XPTillLevelUp)
        {
            CharacterManager.CharacterInstance.LevelUp();
        }
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        levelUpPopup.SetActive(false);
    }


}
