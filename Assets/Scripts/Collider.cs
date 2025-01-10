using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    private const string PLAYER_TAG = "Player";

    private void OnTriggerEnter(Collider other) // 'Collider' ici fait référence au type Unity
    {
        if (other.gameObject.tag == PLAYER_TAG)
        {
            Debug.Log("Collision avec le joueur détectée !");
        }
    }
}
