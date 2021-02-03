using UnityEngine;

public class Lock : MonoBehaviour
{
    [Header("Lock Settings")]
    [SerializeField] private int Price;
    [SerializeField] private string TargetTag;

    [Header("Audio")]
    [SerializeField] private string ClipName;

    private ThorStats Stats;

    private void Start()
    {
        Stats = GameObject.FindGameObjectWithTag(TargetTag).GetComponent<ThorStats>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(TargetTag))
        {
            if (Stats.GetJewels() >= Price)
            {
                SFXAudioManager.SFXManager.PlaySFX(ClipName);

                Stats.UseJewels(Price);

                Destroy(gameObject);
            }
        }
    }
}
