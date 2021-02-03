using UnityEngine;

public class Hammer : MonoBehaviour
{
    [Header("Hammer Settings")]
    [SerializeField] private float Speed = 0;
    public int Damage = 0;
    [SerializeField] private string EnemyTag;

    [Header("Audio")]
    [SerializeField] private string ColliderClipName;
    [SerializeField] private string ThorwClipName;

    private Rigidbody2D MyRigidBody2D;

    private bool HasCollided;

    private void Start()
    {
        MyRigidBody2D = GetComponent<Rigidbody2D>();

        SFXAudioManager.SFXManager.PlaySFX(ThorwClipName);
    }

    void FixedUpdate()
    {
        if (!HasCollided)
        {
            MyRigidBody2D.velocity = transform.right * Speed;
        }
        else
        {
            if (Vector3.Distance(transform.parent.position, transform.position) < 0.1f)
            {
                Destroy(gameObject);
            }
            else
            {
                Vector2 direction = (transform.parent.position - transform.position).normalized;

                MyRigidBody2D.velocity = direction * Speed;
            }

        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        SFXAudioManager.SFXManager.PlaySFX(ColliderClipName);

        if (!HasCollided)
        {
            HasCollided = true;
            transform.Rotate(new Vector3(0, 0, 180));
        }

        if (collision.gameObject.CompareTag(EnemyTag))
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();

            enemy.TakeDamage(Damage);
        }
    }
}
