using UnityEngine;

public class KeyLock : MonoBehaviour
{
    [Header("Lock Settings")]
    [SerializeField] private int Price;
    [SerializeField] private string PlayerTag;

    [Header("Audio")]
    [SerializeField] private string ClipName;

    private ThorStats Stats;

    private void Start()
    {
        Stats = GameObject.FindGameObjectWithTag(PlayerTag).GetComponent<ThorStats>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(PlayerTag))
        {
            if (Stats.GetKeys() >= Price)
            {
                SFXAudioManager.SFXManager.PlaySFX(ClipName);

                Stats.UseKey();

                Destroy(gameObject);
            }
        }
    }
}
