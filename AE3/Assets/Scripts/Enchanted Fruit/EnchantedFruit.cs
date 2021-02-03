using UnityEngine;

public class EnchantedFruit : MonoBehaviour
{
    [Header("Player Settings")]
    [SerializeField] private string PlayerTag;
    private ThorStats PlayerStats;

    private void Start()
    {
        PlayerStats = GameObject.FindGameObjectWithTag(PlayerTag).GetComponent<ThorStats>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(PlayerTag))
        {
            if (!PlayerStats.IsHoldingItem())
            {
                PlayerStats.GetItem();
                Destroy(gameObject);
            }
        }
    }
}
