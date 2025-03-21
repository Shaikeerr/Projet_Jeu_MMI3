using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healthbar : MonoBehaviour
{
    private CharacterStats characterStats;

    // Start is called before the first frame update
    void Start()
    {
        GameObject player = GameObject.FindWithTag("Player");

        if (player != null)
        {
            Debug.Log("Player trouvé !");
            characterStats = player.GetComponent<CharacterStats>();

            if (characterStats != null)
            {
                Debug.Log("CharacterStats trouvé !");
                Debug.Log("Vie du joueur : " + characterStats.Health);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
