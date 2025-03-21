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
        Debug.Log("OnTriggerEnter appel� pour " + other.gameObject.name);

        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Collision d�tect�e avec le joueur !");
            CharacterStats characterStats = other.gameObject.GetComponent<CharacterStats>();
            if (characterStats != null)
            {
                Debug.Log("CharacterStats trouv�, modification des stats...");
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
        Debug.Log("Stat " + statToModify + " modifi�e de " + modificationAmount);
    }
}