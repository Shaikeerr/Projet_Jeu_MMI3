using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveEnemy : MonoBehaviour
{

    public float speed = 5f;
    private Transform player; 

    void Start()
    {
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

    void Update()
    {
        if (player != null) {

            Vector3 targetPosition = new Vector3(player.position.x, transform.position.y, player.position.z);

            Vector3 newPosition = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

            transform.position = newPosition;
        }
    }
}
