using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatModifier : MonoBehaviour
{
    public enum StatType
    {
        Health,
        FireRate,
        Damage,
        Speed,
        Magnet
    }

    public StatType statToModify;
    public int modificationAmount;

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Player"))
        {
            CharacterStats characterStats = other.gameObject.GetComponent<CharacterStats>();
            if (characterStats != null)
            {
                ModifyStat(characterStats);
            }
            else
            {
                Debug.LogError("CharacterStats non trouv� sur le joueur.");
            }
        }
    }

    private void ModifyStat(CharacterStats characterStats)
    {
        switch (statToModify)
        {
            case StatType.Health:
                characterStats.Health += modificationAmount;
                characterStats.baseHealth += modificationAmount;
                break;
            case StatType.FireRate:
                characterStats.fireRate += modificationAmount;
                break;
            case StatType.Damage:
                characterStats.Damage += modificationAmount;
                break;
            case StatType.Speed:
                characterStats.Speed += modificationAmount;
                break;
            case StatType.Magnet:
                characterStats.MagnetRange += modificationAmount;
                break;
            default:
                Debug.LogError("Invalid stat: " + statToModify);
                break;
        }
    }
}