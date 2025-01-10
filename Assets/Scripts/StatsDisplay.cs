using UnityEngine;
using UnityEngine.UI;
using System;


public class StatsDisplay : MonoBehaviour
{
    public CharacterStats characterStats; 
    public Text levelText; 
    public Text xpText;    

    void Start()
    {
        if (characterStats == null)
        {
            characterStats = GetComponent<CharacterStats>();
        }
            
        UpdateStatsDisplay();
    }

    void Update()
    {
        UpdateStatsDisplay();
    }

    void UpdateStatsDisplay()
    {
        levelText.text = $"Level: {characterStats.Level}";
        xpText.text = $"XP: {characterStats.XP:F1} / {characterStats.XPTillLevelUp:F1}";
    }
}
