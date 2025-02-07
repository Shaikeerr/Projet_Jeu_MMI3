using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterManager : MonoBehaviour
{
    public CharacterStats characterStats;
    public GameObject levelUpPopup;

    public Button LeftUpgradeButton;
    public Button RightUpgradeButton;

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
        if (characterStats == null)
        {
            Debug.LogError("characterStats n'est pas assigné.");
            return;
        }

        if (levelUpPopup == null)
        {
            Debug.LogError("levelUpPopup n'est pas assigné.");
            return;
        }

        if (LeftUpgradeButton == null)
        {
            Debug.LogError("LeftUpgradeButton n'est pas assigné.");
            return;
        }

        if (RightUpgradeButton == null)
        {
            Debug.LogError("RightUpgradeButton n'est pas assigné.");
            return;
        }

        characterStats.Level += 1;
        characterStats.XP -= characterStats.XPTillLevelUp;
        characterStats.XPTillLevelUp *= characterStats.LevelMultiplier;

        Time.timeScale = 0;

        int firstRandomUpgrade = Random.Range(0, UpgradeList.Length);
        int secondRandomUpgrade = Random.Range(0, UpgradeList.Length);

        while (firstRandomUpgrade == secondRandomUpgrade)
        {
            secondRandomUpgrade = Random.Range(0, UpgradeList.Length);
        }

        Debug.Log("Amélioration 1 : " + UpgradeList[firstRandomUpgrade]);
        Debug.Log("Amélioration 2 : " + UpgradeList[secondRandomUpgrade]);

        // Configurez les boutons pour afficher les améliorations aléatoires
        LeftUpgradeButton.onClick.AddListener(() => ApplyUpgrade(UpgradeList[firstRandomUpgrade]));
        RightUpgradeButton.onClick.AddListener(() => ApplyUpgrade(UpgradeList[secondRandomUpgrade]));

        // Ajoutez des listeners pour appliquer les améliorations lorsqu'ils sont cliqués
        LeftUpgradeButton.onClick.RemoveAllListeners();
        LeftUpgradeButton.onClick.AddListener(() => ApplyUpgrade(UpgradeList[firstRandomUpgrade]));

        RightUpgradeButton.onClick.RemoveAllListeners();
        RightUpgradeButton.onClick.AddListener(() => ApplyUpgrade(UpgradeList[secondRandomUpgrade]));

        levelUpPopup.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void ApplyUpgrade(string upgrade)
    {
        characterStats.ApplyUpgrade(upgrade);
        characterStats.ResumeGame();
    }
    
}

