using UnityEngine;

public class Skunk : Enemy
{
    [Header("Animation Settings")]
    [SerializeField] private string MoveParameterX;
    [SerializeField] private string MoveParameterY;

    [Header("Direction Settings")]
    private Vector2 Direction;
    [SerializeField] private string EnviromentTag;
    [SerializeField] private float DistanceToWall;

    [Header("Attack Settings")]
    [SerializeField] private GameObject AttackPrefab;
    [SerializeField] private float AttackSpeed;
    [SerializeField] private float AttackDistance;
    [SerializeField] private LayerMask AttackLayer;

    private Vector2 FacingDirection;
    private GameObject AttackInstance = null;

    protected override void Start()
    {
        base.Start();
        Direction = ChooseDirection();
    }

    private void Update()
    {
        MyRigidBody2D.velocity = Direction * Speed;

        MyAnimator.SetFloat(MoveParameterX, MyRigidBody2D.velocity.x);
        MyAnimator.SetFloat(MoveParameterY, MyRigidBody2D.velocity.y);

        FacingDirection = MyRigidBody2D.velocity;

        if (AttackInstance == null)
        {
            if (Physics2D.Raycast(transform.position, -FacingDirection, AttackDistance, AttackLayer))
            {
                Attack(FacingDirection);
            }
        }
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
    }

    public void OnCollisionStay2D(Collision2D collison)
    {
        Direction = ChooseDirection();
    }

    Vector2 ChooseDirection()
    {
        int random = Random.Range(0, 4);

        Vector2 temp = Vector2.zero;

        switch (random)
        {
            case 0:
                temp = Vector2.up;
                break;
            case 1:
                temp = Vector2.down;
                break;
            case 2:
                temp = Vector2.left;
                break;
            case 3:
                temp = Vector2.right;
                break;
            default:
                break;
        }

        return temp;
    }

    private void Attack(Vector2 direction)
    {
        AttackInstance = Instantiate(AttackPrefab, transform.position, Quaternion.identity);

        AttackInstance.GetComponent<Rigidbody2D>().velocity = -direction.normalized * AttackSpeed;
    }
}
