using UnityEngine;

public class NPC : MonoBehaviour
{
    [Header("Animation Settings")]
    [SerializeField] private string MoveParameterX;
    [SerializeField] private string MoveParameterY;

    [Header("Movement Settings")]
    [SerializeField] private float Speed;
    [SerializeField] private string EnviromentTag;
    [SerializeField] private float DistanceToWall;
    private Vector2 Direction;

    private Animator MyAnimator;
    private Rigidbody2D MyRigidBody2D;

    private void Start()
    {
        MyAnimator = GetComponent<Animator>();
        MyRigidBody2D = GetComponent<Rigidbody2D>();

        Direction = ChooseDirection();
    }
    private void Update()
    {
        MyRigidBody2D.velocity = Direction * Speed;

        var Hits = Physics2D.RaycastAll(transform.position, Direction, DistanceToWall);

        foreach (var hit in Hits)
        {
            if (hit.collider.CompareTag(EnviromentTag))
            {
                Direction = ChooseDirection();
                return;
            }
        }

        MyAnimator.SetFloat(MoveParameterX, MyRigidBody2D.velocity.x);
        MyAnimator.SetFloat(MoveParameterY, MyRigidBody2D.velocity.y);
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


}
