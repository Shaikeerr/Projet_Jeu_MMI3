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

    public Image textContainerLeftUpgrade;
    public Image textureLeftUpgrade;
    public Image textContainerRightUpgrade;
    public Image textureRightUpgrade;

    public Sprite HealthSprite;
    public Sprite HealthTextContainer;  
    public Sprite FireRateSprite;
    public Sprite FireRateTextContainer;
    public Sprite DamageSprite;
    public Sprite DamageTextContainer;
    public Sprite SpeedSprite;
    public Sprite SpeedTextContainer;
    public Sprite MagnetSprite;
    public Sprite MagnetTextContainer;


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

    public int Health
    {
        get => characterStats.Health;
        set => characterStats.Health = value;   
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

        switch (UpgradeList[firstRandomUpgrade])
        {
            case "Health":
                textureLeftUpgrade.sprite = HealthSprite;
                textContainerLeftUpgrade.sprite = HealthTextContainer;
                break;
            case "FireRate":
                textureLeftUpgrade.sprite = FireRateSprite;
                textContainerLeftUpgrade.sprite = FireRateTextContainer;
                break;
            case "Damage":
                textureLeftUpgrade.sprite = DamageSprite;
                textContainerLeftUpgrade.sprite = DamageTextContainer;
                break;
            case "Speed":
                textureLeftUpgrade.sprite = SpeedSprite;
                textContainerLeftUpgrade.sprite = SpeedTextContainer;
                break;
            case "Magnet":
                textureLeftUpgrade.sprite = MagnetSprite;
                textContainerLeftUpgrade.sprite = MagnetTextContainer;
                break;
            default:
                Debug.LogError("Invalid upgrade: " + UpgradeList[firstRandomUpgrade]);
                break;
        }

        switch (UpgradeList[secondRandomUpgrade])
        {
            case "Health":
                textureRightUpgrade.sprite = HealthSprite;
                textContainerRightUpgrade.sprite = HealthTextContainer;
                break;
            case "FireRate":
                textureRightUpgrade.sprite = FireRateSprite;
                textContainerRightUpgrade.sprite = FireRateTextContainer;
                break;
            case "Damage":
                textureRightUpgrade.sprite = DamageSprite;
                textContainerRightUpgrade.sprite = DamageTextContainer;
                break;
            case "Speed":
                textureRightUpgrade.sprite = SpeedSprite;
                textContainerRightUpgrade.sprite = SpeedTextContainer;
                break;
            case "Magnet":
                textureRightUpgrade.sprite = MagnetSprite;
                textContainerRightUpgrade.sprite = MagnetTextContainer;
                break;
            default:
                Debug.LogError("Invalid upgrade: " + UpgradeList[secondRandomUpgrade]);
                break;
        }

        LeftUpgradeButton.onClick.AddListener(() => ApplyUpgrade(UpgradeList[firstRandomUpgrade]));
        RightUpgradeButton.onClick.AddListener(() => ApplyUpgrade(UpgradeList[secondRandomUpgrade]));

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

