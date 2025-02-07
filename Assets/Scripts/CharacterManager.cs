using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    public CharacterStats characterStats;
    public GameObject levelUpPopup;

    public static CharacterManager CharacterInstance { get; private set; }

    public string[] UpgradeList = { "Health", "FireRate", "Damage", "Speed", "Magnet" };


    private void Awake()
    {


        if (CharacterInstance == null)
        {
            CharacterInstance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public float fireRate
    {
        get => characterStats.fireRate;
        set => characterStats.fireRate = value;
    }

    public void LevelUp()
    {
        characterStats.Level += 1;
        characterStats.XP -= characterStats.XPTillLevelUp;
        characterStats.XPTillLevelUp *= characterStats.LevelMultiplier;

        Time.timeScale = 0;

        int FirstRandomUpgrade;
        int SecondRandomUpgrade;

        for (int upgradechoice = 0; upgradechoice < 2; upgradechoice++) {
            FirstRandomUpgrade = Random.Range(0, UpgradeList.Length);
            SecondRandomUpgrade = Random.Range(0, UpgradeList.Length);
            if (FirstRandomUpgrade != SecondRandomUpgrade)
            {
                Debug.Log(UpgradeList[FirstRandomUpgrade]);
                Debug.Log(UpgradeList[SecondRandomUpgrade]);
            }
            else
            {
                upgradechoice--;
            }

        Debug.Log("Amélioration 1 : " + UpgradeList[FirstRandomUpgrade]);
        Debug.Log("Amélioration 2 : " + UpgradeList[SecondRandomUpgrade]);
        }

        levelUpPopup.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void upgradeHealth()
    {
        Debug.Log("Amélioration : Points de vie augmentés");
        characterStats.ResumeGame();
    }

    public void upgradeFireRate()
    {
        Debug.Log("Amélioration : Vitesse de tir augmentée");
        fireRate += 1f;
        Debug.Log(fireRate);
        characterStats.ResumeGame();
    }

    public void upgradeDamage()
    {
        Debug.Log("Amélioration : Dégats augmentés");
        characterStats.Damage += 1f;
        characterStats.ResumeGame();
    }

    public void upgradeSpeed()
    {
        Debug.Log("Amélioration : Vitesse");
        characterStats.Speed += 10f;
        characterStats.ResumeGame();
    }

    public void upgradeMagnet()
    {
        Debug.Log("Amélioration : Aimant"); 
        characterStats.MagnetRange += 1f;    
        characterStats.ResumeGame();    
    }

}

