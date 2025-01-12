using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetController : MonoBehaviour
{
    private SphereCollider magnetCollider; 
    private CharacterStats characterStats;

    private void Start()
    {
        magnetCollider = GetComponent<SphereCollider>();
        characterStats = GetComponentInParent<CharacterStats>();
    }

    private void Update()
    {
        if (magnetCollider != null && characterStats != null)
        {
            magnetCollider.radius = characterStats.MagnetRange;
        }
    }
}
