using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    private CharacterStats characterStats;
    public Text HealthText; 
    public Image HealthFillImage;

    // Start is called before the first frame update
    void Start()
    {
        GameObject player = GameObject.FindWithTag("Player");

        if (player != null)
        {
            characterStats = player.GetComponent<CharacterStats>();
            UpdateHealthDisplay();
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHealthDisplay();
    }

    void UpdateHealthDisplay()
    {
        if (characterStats == null)
        {
            return;
        }
        HealthText.text = $"{characterStats.Health} / {characterStats.baseHealth}";
        
        if (characterStats.baseHealth > 0)
        {
            HealthFillImage.fillAmount = (float)characterStats.Health / characterStats.baseHealth;
        }
        else
        {
            HealthFillImage.fillAmount = 0;
        }
    }
}
