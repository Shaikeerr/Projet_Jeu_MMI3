using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class XPBar : MonoBehaviour
{

    private CharacterStats characterStats;
    public Text XPText; 
    public Slider XPSlider;

    // Start is called before the first frame update
    void Start()
    {
        GameObject player = GameObject.FindWithTag("Player");

        if (player != null)
        {
            characterStats = player.GetComponent<CharacterStats>();
            UpdateXPDisplay();
        }

    }

    // Update is called once per frame
    void Update()
    {
        UpdateXPDisplay();
    }

        void UpdateXPDisplay()
    {
        if (characterStats == null)
        {
            return;
        }
        XPText.text = $"Level: {characterStats.Level} - {characterStats.XP.ToString("F2")} / {characterStats.XPTillLevelUp.ToString("F2")} XP";
        if (characterStats.XPTillLevelUp > 0)
        {
            XPSlider.value = (float)characterStats.XP / characterStats.XPTillLevelUp;
        }
        else
        {
            XPSlider.value = 0;
        }

    }
}
