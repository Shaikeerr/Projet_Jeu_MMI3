using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharacterStats : MonoBehaviour
{
    public float XP = 0f;
    public float Level = 1f;
    public float LevelMultiplier = 1.2f;
    public float XPTillLevelUp = 2;
    public float Damage = 1f;
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
                //logique pour am�liorer la sant� 
                Debug.Log("Am�lioration : Health");
                baseHealth += 10;
                Health = baseHealth;
                break;
            case "FireRate":
                fireRate += 1f;
                Debug.Log("Am�lioration : FireRate");
                break;
            case "Damage":
                Damage += 1f;
                Debug.Log("Am�lioration : Damage");
                break;
            case "Speed":
                Speed += 10f;
                Debug.Log("Am�lioration : Speed");
                break;
            case "Magnet":
                MagnetRange += 1f;
                Debug.Log("Am�lioration : Magnet");
                break;
            default:
                Debug.LogError("Invalid upgrade: " + upgrade);
                break;
        }
        ResumeGame();
    }

    public void TakeDamage(int damage)
    {
        Debug.Log("D�g�ts inflig�s au joueur : " + damage);
        Health -= damage;
        Debug.Log("Sant� restante : " + Health);
        if (Health <= 0)
        {
            Debug.Log("Le joueur est mort !");
            gameOverScreen.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
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
