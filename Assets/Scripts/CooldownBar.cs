using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CooldownBar : MonoBehaviour
{
    private SummonEnnemies summonEnnemies;
    public Text CooldownText;
    public Slider CooldownSlider;

    void Start()
    {
        GameObject player = GameObject.FindWithTag("Player");

        if (player != null)
        {
            summonEnnemies = player.GetComponent<SummonEnnemies>();
            UpdateCooldownDisplay(); // Initial call to set the display
        }
    }
    void Update()
    {
        UpdateCooldownDisplay(); // Update the cooldown display every frame
    }

    void UpdateCooldownDisplay()
    {
        if (summonEnnemies == null)
        {
            return;
        }
        CooldownText.text = $"Wave {summonEnnemies.currentWave}";
        if (summonEnnemies.timeBetweenWaves > 0)
        {
            CooldownSlider.value = 1 - (float)summonEnnemies.timeSinceLastWave / summonEnnemies.timeBetweenWaves; // Do maths to get a value between 0 and 1 and invert it to make a decreasing bar)
        }
        else
        {
            CooldownSlider.value = 0;
        }
    }
}