using UnityEngine;

public class Apple : MonoBehaviour
{
    [Header("Apple Settings")]
    [SerializeField] private int Quantity;
    [SerializeField] private bool IsRotten;

    [Header("Player Settings")]
    [SerializeField] private string PlayerTag;

    [Header("Audio")]
    [SerializeField] private string RottenClipName;
    [SerializeField] private string AppleClipName;


    private ThorStats PlayerStats;

    private void Start()
    {
        PlayerStats = GameObject.FindGameObjectWithTag(PlayerTag).GetComponent<ThorStats>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(PlayerTag))
        {
            if (!PlayerStats.DoesPlayerHasMaxHealth())
            {
                if (!IsRotten)
                {
                    SFXAudioManager.SFXManager.PlaySFX(AppleClipName);
                    PlayerStats.Heal(Quantity);

                }
                else
                {
                    SFXAudioManager.SFXManager.PlaySFX(RottenClipName);
                    PlayerStats.TakeDamage(Quantity);
                }

                Destroy(gameObject);
            }
            else
            {
                if (IsRotten)
                {
                    SFXAudioManager.SFXManager.PlaySFX(RottenClipName);
                    PlayerStats.TakeDamage(Quantity);
                    Destroy(gameObject);
                }
            }
        }
    }

}
