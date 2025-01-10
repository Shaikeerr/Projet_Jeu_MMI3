using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    public CharacterStats characterStats;
    public GameObject levelUpPopup;

    public static CharacterManager CharacterInstance { get; private set; }

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

        levelUpPopup.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void upgradeHealth()
    {
        Debug.Log("Am�lioration : Points de vie augment�s");
        characterStats.ResumeGame();
    }

    public void upgradeFireRate()
    {
        Debug.Log("Am�lioration : Vitesse de tir augment�e");
        fireRate += 1f;
        Debug.Log(fireRate);
        characterStats.ResumeGame();
    }

    public void upgradeDamage()
    {
        Debug.Log("Am�lioration : D�gats augment�s");
        characterStats.Damage += 1f;
        characterStats.ResumeGame();
    }

    public void upgradeSpeed()
    {
        Debug.Log("Am�lioration : Vitesse");
        characterStats.Speed += 10f;
        characterStats.ResumeGame();
    }

}

