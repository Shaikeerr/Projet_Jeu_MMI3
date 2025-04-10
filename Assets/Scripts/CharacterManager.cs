using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class CharacterManager : MonoBehaviour
{

    [Header("Character Stats")]
    public CharacterStats characterStats;
    public GameObject levelUpPopup;

    [Header("Upgrade Buttons")]
    public Button LeftUpgradeButton;
    public Button RightUpgradeButton;

    [Header("Upgrade Containers")]
    [SerializeField] private Image textContainerLeftUpgrade;
    [SerializeField] private Image textureLeftUpgrade;
    [SerializeField] private Image textContainerRightUpgrade;
    [SerializeField] private Image textureRightUpgrade;

    [Header("Upgrade Text Textures")]
    [SerializeField] private Sprite HealthTextContainer;  
    [SerializeField] private Sprite FireRateTextContainer;
    [SerializeField] private Sprite DamageTextContainer;
    [SerializeField] private Sprite SpeedTextContainer;
    [SerializeField] private Sprite MagnetTextContainer;

    [Header("Upgrade Sprites")]
    [SerializeField] private Sprite HealthSprite;
    [SerializeField] private Sprite FireRateSprite;
    [SerializeField] private Sprite DamageSprite;
    [SerializeField] private Sprite SpeedSprite;
    [SerializeField] private Sprite MagnetSprite;

    private PlayerControls inputActions;

    public static CharacterManager CharacterInstance { get; private set; }

    public string[] UpgradeList = { "Health", "FireRate", "Damage", "Speed", "Magnet" };

    private void Awake()
    {

        inputActions = new PlayerControls();

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

private void OnEnable()
{
    inputActions.Player.Enable();
    inputActions.Player.LeftUpgrade.performed += OnLeftUpgrade;
    inputActions.Player.RightUpgrade.performed += OnRightUpgrade;
}

private void OnDisable()
{
    inputActions.Player.Disable();
    inputActions.Player.LeftUpgrade.performed -= OnLeftUpgrade;
    inputActions.Player.RightUpgrade.performed -= OnRightUpgrade; 
}

    private void OnLeftUpgrade(InputAction.CallbackContext context) // WHen Left Bumper is pressed 
    {
        LeftUpgradeButton.onClick.Invoke();
    }

    private void OnRightUpgrade(InputAction.CallbackContext context) // When Right Bumper is pressed
    {
        RightUpgradeButton.onClick.Invoke();
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
            Debug.LogError("characterStats n'est pas assign�.");
            return;
        }

        if (levelUpPopup == null)
        {
            Debug.LogError("levelUpPopup n'est pas assign�.");
            return;
        }

        if (LeftUpgradeButton == null)
        {
            Debug.LogError("LeftUpgradeButton n'est pas assign�.");
            return;
        }

        if (RightUpgradeButton == null)
        {
            Debug.LogError("RightUpgradeButton n'est pas assign�.");
            return;
        }

        characterStats.Level += 1;
        characterStats.XP -= characterStats.XPTillLevelUp;
        characterStats.XPTillLevelUp *= characterStats.LevelMultiplier;

        Time.timeScale = 0;

        int firstRandomUpgrade = Random.Range(0, UpgradeList.Length); // Randomly select the first upgrade
        int secondRandomUpgrade = Random.Range(0, UpgradeList.Length); // Randomly select the second upgrade

        while (firstRandomUpgrade == secondRandomUpgrade) // If the same upgrade is selected as the 2nd, select a new one
        {
            secondRandomUpgrade = Random.Range(0, UpgradeList.Length);
        }

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
        LeftUpgradeButton.onClick.RemoveAllListeners(); // Remove all listeners to avoid multiple calls (especially with a controler)
        RightUpgradeButton.onClick.RemoveAllListeners(); // Remove all listeners to avoid multiple calls (especially with a controler)
        levelUpPopup.SetActive(false); 
    }
    
}

