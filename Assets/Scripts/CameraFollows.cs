using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Vector3 offset;   
    void Update()
    {
        transform.position = player.position + offset; // Update camera position based on player position and offset 
    }
}
