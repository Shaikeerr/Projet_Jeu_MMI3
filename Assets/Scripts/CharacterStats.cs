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
    public Button UpgradeHealthButton;
    public Button UpgradeFireRateButton;

    public float MagnetRange = 0f;

    private void Start()
    {
        UpgradeHealthButton.onClick.AddListener(CharacterManager.CharacterInstance.upgradeHealth);
        UpgradeFireRateButton.onClick.AddListener(CharacterManager.CharacterInstance.upgradeFireRate);
        UpgradeFireRateButton.onClick.AddListener(CharacterManager.CharacterInstance.upgradeFireRate);

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
