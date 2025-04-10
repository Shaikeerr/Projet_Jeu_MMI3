using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileDisapear : MonoBehaviour
{
    private const string ENEMY_TAG = "Enemy";
    private void OnCollisionEnter(Collision enemy)
    {
        if (enemy.gameObject.tag == ENEMY_TAG)
        {
            Destroy(this.gameObject);
        }
    }
}
