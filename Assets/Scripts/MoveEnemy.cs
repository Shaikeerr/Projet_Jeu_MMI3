using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveEnemy : MonoBehaviour
{

    public float speed = 5f;
    private Transform player; 

    // Start is called before the first frame update
    void Start()
    {
        // Find the player by tag (ensure your player has the "Player" tag)
        GameObject playerObject = GameObject.FindWithTag("Player");

        if (playerObject != null)
        {
            player = playerObject.transform;
        }
        else
        {
            Debug.LogError("Player not found! Ensure your player GameObject has the 'Player' tag.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null) {

            // Keep the current Y-coordinate of the enemy
            Vector3 targetPosition = new Vector3(player.position.x, transform.position.y, player.position.z);

            // Move towards the adjusted target position
            Vector3 newPosition = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

            transform.position = newPosition;
        }
    }
}
