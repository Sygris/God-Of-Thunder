using UnityEngine;

public class Jewels : MonoBehaviour
{
    [Header("Jewel Settings")]
    [SerializeField] private int Value;
    [SerializeField] private string TargetTag;

    [Header("Audio")]
    [SerializeField] private string ClipName;

    private ThorStats Stats;

    private void Start()
    {
        Stats = GameObject.FindGameObjectWithTag(TargetTag).GetComponent<ThorStats>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(TargetTag))
        {
            SFXAudioManager.SFXManager.PlaySFX(ClipName);
            Stats.CollectJewel(Value);

            Destroy(gameObject);
        }
    }
}
