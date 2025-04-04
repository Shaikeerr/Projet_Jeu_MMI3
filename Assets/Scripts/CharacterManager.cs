using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

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

        private PlayerControls inputActions;
    private Button currentButton;


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
    inputActions.Player.UpgradeButtons.performed += OnUpgradeButtons;

    // Set the default selected button
    currentButton = LeftUpgradeButton;
    currentButton.Select();
}

private void OnDisable()
{
    inputActions.Player.Disable();
    inputActions.Player.UpgradeButtons.performed -= OnUpgradeButtons;
}

private void OnUpgradeButtons(InputAction.CallbackContext context)
{
    float input = context.ReadValue<float>();

    if (input > 0) // RB pressed
    {
        Debug.Log("RB pressed");
        RightUpgradeButton.onClick.Invoke(); // Trigger the right button's onClick event
    }
    else if (input < 0) // LB pressed
    {
        Debug.Log("LB pressed");
        LeftUpgradeButton.onClick.Invoke(); // Trigger the left button's onClick event
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

        int firstRandomUpgrade = Random.Range(0, UpgradeList.Length);
        int secondRandomUpgrade = Random.Range(0, UpgradeList.Length);

        while (firstRandomUpgrade == secondRandomUpgrade)
        {
            secondRandomUpgrade = Random.Range(0, UpgradeList.Length);
        }

        Debug.Log("Am�lioration 1 : " + UpgradeList[firstRandomUpgrade]);
        Debug.Log("Am�lioration 2 : " + UpgradeList[secondRandomUpgrade]);

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

