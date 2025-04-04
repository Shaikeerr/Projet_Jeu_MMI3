using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CooldownBar : MonoBehaviour
{
    private SummonEnnemies summonEnnemies;
    public Text CooldownText;
    public Slider CooldownSlider;

    // Start is called before the first frame update
    void Start()
    {
        GameObject player = GameObject.FindWithTag("Player");

        if (player != null)
        {
            summonEnnemies = player.GetComponent<SummonEnnemies>();
            UpdateCooldownDisplay();
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateCooldownDisplay();
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
            CooldownSlider.value = 1 - (float)summonEnnemies.timeSinceLastWave / summonEnnemies.timeBetweenWaves;
        }
        else
        {
            CooldownSlider.value = 0;
        }
    }
}