using UnityEngine;

public class Pushable : MonoBehaviour
{
    [Header("Block Settings")]
    [SerializeField] private float Speed;
    [SerializeField] private bool IsPushable;

    [Header("Raycast Settings")]
    [SerializeField] private float Distance;
    [SerializeField] private string PlayerTag;
    [SerializeField] private LayerMask LayerMask;

    private Rigidbody2D MyRigidbody2D;

    private void Start()
    {
        MyRigidbody2D = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (IsPushable)
        {
            RaycastHit2D Top = Physics2D.Raycast(transform.position, Vector2.up, Distance, LayerMask);
            RaycastHit2D Down = Physics2D.Raycast(transform.position, Vector2.down, Distance, LayerMask);
            RaycastHit2D Left = Physics2D.Raycast(transform.position, Vector2.left, Distance, LayerMask);
            RaycastHit2D Right = Physics2D.Raycast(transform.position, Vector2.right, Distance, LayerMask);

            Vector2 direction = Vector2.zero;

            if (Top)
            {
                direction = Vector2.down;
            }
            else if (Down)
            {
                direction = Vector2.up;
            }
            else if (Left)
            {
                direction = Vector2.right;
            }
            else if (Right)
            {
                direction = Vector2.left;
            }

            MyRigidbody2D.MovePosition(MyRigidbody2D.position + direction * Speed * Time.fixedDeltaTime);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(PlayerTag))
        {
            IsPushable = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(PlayerTag))
        {
            IsPushable = false;
        }
    }
}
