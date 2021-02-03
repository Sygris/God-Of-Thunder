using UnityEngine;

public class Eyeball : MonoBehaviour
{
    [Header("Eyebal Settings")]
    [SerializeField] private GameObject AttackPrefab;
    [SerializeField] private float AttackSpeed;

    [Header("Raycast Settings")]
    [SerializeField] private float Distance;
    [SerializeField] private LayerMask LayerMask;

    private GameObject AttackInstance;

    private RaycastHit2D[] Hits = new RaycastHit2D[4];

    private void Update()
    {
        if (AttackInstance == null)
        {
            Hits[0] = Physics2D.Raycast(transform.position, Vector2.up, Distance, LayerMask);
            Hits[1] = Physics2D.Raycast(transform.position, Vector2.down, Distance, LayerMask);
            Hits[2] = Physics2D.Raycast(transform.position, Vector2.left, Distance, LayerMask);
            Hits[3] = Physics2D.Raycast(transform.position, Vector2.right, Distance, LayerMask);

            for (int i = 0; i < Hits.Length; i++)
            {
                if (Hits[i])
                {
                    switch (i)
                    {
                        case 0:
                            Attack(Vector2.up);
                            break;
                        case 1:
                            Attack(Vector2.down);
                            break;
                        case 2:
                            Attack(Vector2.left);
                            break;
                        case 3:
                            Attack(Vector2.right);
                            break;
                        default:
                            break;
                    }
                }
            }
        }
    }

    private void Attack(Vector2 direction)
    {
        AttackInstance = Instantiate(AttackPrefab, transform.position, Quaternion.identity);

        AttackInstance.GetComponent<Rigidbody2D>().velocity = direction * AttackSpeed;
    }
}
