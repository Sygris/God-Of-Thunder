using UnityEngine;

public class Keys : MonoBehaviour
{
    [Header("Key Settings")]
    [SerializeField] private string PlayerTag;

    private ThorStats Stats;

    private void Start()
    {
        Stats = GameObject.FindGameObjectWithTag(PlayerTag).GetComponent<ThorStats>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(PlayerTag))
        {
            Stats.CollectKey();

            Destroy(gameObject);
        }
    }
}
