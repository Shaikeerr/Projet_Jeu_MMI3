using System;
using UnityEngine;
using UnityEngine.UI;

public class CharacterStats : MonoBehaviour
{
    public float XP = 0f;
    public float Level = 1f;
    public float LevelMultiplier = 1.2f;
    public float XPTillLevelUp = 2;
    public float Damage = 1f;
    public float Speed = 10f;

    public float fireRate = 1f;

    public GameObject levelUpPopup;

    public float MagnetRange = 0f;

    public void ApplyUpgrade(string upgrade)
    {
        switch (upgrade)
        {
            case "Health":
                //logique pour améliorer la santé 
                Debug.Log("Amélioration : Health");
                break;
            case "FireRate":
                fireRate += 1f;
                Debug.Log("Amélioration : FireRate");
                break;
            case "Damage":
                Damage += 1f;
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
