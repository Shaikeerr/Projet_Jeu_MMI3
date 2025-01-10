using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class XPOrb : MonoBehaviour
{
    public float fireRateIncrease = 0.5f; 
    public const string PLAYER_TAG = "Player";
    public float duration = 5f;

    private void OnTriggerEnter(Collider other)
    {
        CharacterStats xporb = other.GetComponent<CharacterStats>();

        if (other.gameObject.tag == PLAYER_TAG)

        {

            int XPNumber = Random.Range(1, 4); 

            xporb.XP += XPNumber;

            gameObject.SetActive(false);
        }
    }
}
