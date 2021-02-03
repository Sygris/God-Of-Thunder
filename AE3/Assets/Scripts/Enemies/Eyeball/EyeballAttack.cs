using UnityEngine;

public class EyeballAttack : MonoBehaviour
{
    [Header("Eyeball Attack Settings")]
    [SerializeField] private int Damage;
    [SerializeField] private string PlayerTag;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(PlayerTag))
        {
            ThorStats PlayerStats = GameObject.FindGameObjectWithTag(PlayerTag).GetComponent<ThorStats>();

            PlayerStats.TakeDamage(Damage);
        }
           
        Destroy(gameObject);
    }
}
