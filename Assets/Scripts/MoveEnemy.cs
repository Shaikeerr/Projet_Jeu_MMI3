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
    }

    void Update()
    {
        if (player != null) {

            Vector3 targetPosition = new Vector3(player.position.x, transform.position.y, player.position.z); 

            Vector3 newPosition = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

            transform.position = newPosition;

            transform.LookAt(new Vector3(player.position.x, transform.position.y, player.position.z));
        }
    }
}
