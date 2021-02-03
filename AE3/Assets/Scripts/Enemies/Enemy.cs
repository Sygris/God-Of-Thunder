using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Settings")]
    [SerializeField] protected int Health;
    [SerializeField] protected int Damage;
    [SerializeField] protected int Score;
    [SerializeField] protected float Speed;

    [Header("Death Effects")]
    [SerializeField] private GameObject DeathEffect;
    [SerializeField] private LootTable Loot;

    [Header("Player Settings")]
    [SerializeField] protected string PlayerTag;
    protected ThorStats PlayerStats;

    protected Rigidbody2D MyRigidBody2D;
    protected Animator MyAnimator;

    private Vector3 StartPosition;

    protected virtual void Start()
    {
        MyRigidBody2D = gameObject.GetComponent<Rigidbody2D>();
        MyAnimator = gameObject.GetComponent<Animator>();

        StartPosition = transform.position;

        PlayerStats = GameObject.FindGameObjectWithTag(PlayerTag).GetComponent<ThorStats>();
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(PlayerTag))
        {
            PlayerStats.TakeDamage(Damage);
        }
    }

    public void TakeDamage(int damage)
    {
        Health -= damage;

        if (Health <= 0)
        {
            gameObject.SetActive(false);

            PlayerStats.IncreaseScore(Score);

            GameObject GO = Instantiate(DeathEffect, transform.position, Quaternion.identity);
            float duration = GO.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length;

            Destroy(GO, duration);

            DropLoot();
        }
    }

    private void DropLoot()
    {
        if (Loot != null)
        {
            GameObject drop = Loot.Drop();

            if (drop != null)
            {
                Instantiate(drop, transform.position, Quaternion.identity);
            }
        }
    }

    public int GetScore()
    {
        return Score;
    }
}
