using UnityEngine;

public class XPOrb : MonoBehaviour
{
    public const string MAGNET = "Magnet";
    public float moveSpeed = 10f; 
    private Transform target;    
    private bool isMovingToTarget = false;

    public int minXpRange;
    public int maxXpRange;


    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag(MAGNET))
        {
            CharacterStats playerStats = other.GetComponentInParent<CharacterStats>();

            if (playerStats != null)
            {
                target = other.transform; 
                isMovingToTarget = true;  
            }
        }
    }

    private void Update()
    {
        if (isMovingToTarget && target != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, target.position) < 0.1f)
            {
                CharacterStats playerStats = target.GetComponentInParent<CharacterStats>();
                if (playerStats != null)
                {
                    int XPNumber = Random.Range(minXpRange, maxXpRange);
                    playerStats.XP += XPNumber;
                }

                isMovingToTarget = false;
                gameObject.SetActive(false); 
            }
        }
    }
}
