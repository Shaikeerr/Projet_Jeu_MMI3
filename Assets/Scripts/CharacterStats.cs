using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharacterStats : MonoBehaviour
{
    public float XP = 0f;
    public float Level = 1f;
    public float LevelMultiplier = 1.5f;
    public float XPTillLevelUp = 2;
    public int Damage = 10;
    public float Speed = 10f;
    private int baseHealth = 100;
    public int Health = 100;

    public float fireRate = 1f;

    public GameObject levelUpPopup;

    public GameObject gameOverScreen;

    public float MagnetRange = 0f;

    public void ApplyUpgrade(string upgrade)
    {
        switch (upgrade)
        {
            case "Health":
                Debug.Log("Amélioration : Health");
                baseHealth += 10;
                Health = baseHealth;
                break;
            case "FireRate":
                fireRate += 1f;
                Debug.Log("Amélioration : FireRate");
                break;
            case "Damage":
                Damage += 5;
                Debug.Log("Amélioration : Damage");
                break;
            case "Speed":
                Speed += 10f;
                Debug.Log("Amélioration : Speed");
                break;
            case "Magnet":
                MagnetRange += 1f;
                Debug.Log("Amélioration : Magnet");
                break;
            default:
                Debug.LogError("Invalid upgrade: " + upgrade);
                break;
        }
        ResumeGame();
    }

    public void TakeDamage(int damage)
    {
        Debug.Log("Dégâts infligés au joueur : " + damage);
        Health -= damage;
        Debug.Log("Santé restante : " + Health);
        if (Health <= 0)
        {
            Debug.Log("Le joueur est mort !");
            gameOverScreen.SetActive(true);
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
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1;
        levelUpPopup.SetActive(false);
    }


}
