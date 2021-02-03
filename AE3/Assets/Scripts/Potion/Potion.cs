using UnityEngine;

public class Potion : MonoBehaviour
{
    [Header("Potion Settings")]
    [SerializeField] private int Quantity;

    [Header("Player Settings")]
    [SerializeField] private string PlayerTag;

    [Header("Audio")]
    [SerializeField] private string ClipName;

    private ThorStats PlayerStats;

    private void Start()
    {
        PlayerStats = GameObject.FindGameObjectWithTag(PlayerTag).GetComponent<ThorStats>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(PlayerTag))
        {
            if (!PlayerStats.DoesPlayerHasMaxMagic())
            {
                SFXAudioManager.SFXManager.PlaySFX(ClipName);

                PlayerStats.GetMagic(Quantity);

                Destroy(gameObject);
            }
        }
    }
}
