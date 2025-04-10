using UnityEngine;

public class XPOrb : MonoBehaviour
{
    public const string MAGNET = "Magnet";

    private GameObject player;
    private CharacterStats characterStats;
    public float moveSpeed = 10f; // Speed at which the XP orb moves towards the player
    private Transform target;    
    private bool isMovingToTarget = false;

    [Header("XP Range")]
    public int minXpRange;
    public int maxXpRange;


    private void Awake ()
    {
        player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            characterStats = player.GetComponent<CharacterStats>();
            UpdateSpeed(); 
        }
        else
        {
            Debug.LogWarning("Player object not found in the scene.");
        }
    }
    
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

    private void UpdateSpeed()
    {
        if (characterStats != null)
        {
            moveSpeed = characterStats.Speed + 5f; // Adjust the speed based on the character's speed
        }
        else
        {
            Debug.LogWarning("CharacterStats component not found on parent object.");
        }
    }

    private void Update()
    {

        if (characterStats != null)
        {
            UpdateSpeed(); 
        }
        else
        {
            Debug.LogWarning("CharacterStats component not found on parent object.");
        }

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
