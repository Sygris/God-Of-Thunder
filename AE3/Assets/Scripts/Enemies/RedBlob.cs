using UnityEngine;

public class RedBlob : Enemy
{
    [Header("Movement Settings")]
    [SerializeField] private int MinDirections;
    [SerializeField] private int MaxDirections;

    [Header("Animation Settings")]
    [SerializeField] private string Parameter = "";
    [SerializeField] private float IdleInterval;
    [SerializeField] private float MoveInterval;

    private bool IsMoving;
    private float CurrentTime;

    private Vector2 OldVelocity;

    void Update()
    {
        CurrentTime += Time.deltaTime;

        if (!IsMoving)
        {
            if (CurrentTime > IdleInterval)
            {
                Move();
                MyAnimator.SetBool(Parameter, true);
                CurrentTime = 0;
                IsMoving = true;
            }
        }
        else
        {
            if (CurrentTime > MoveInterval)
            {
                MyAnimator.SetBool(Parameter, false);
                CurrentTime = 0;
                IsMoving = false;
                MyRigidBody2D.velocity = Vector2.zero;
            }
        }

        OldVelocity = MyRigidBody2D.velocity;
    }

    private void Move()
    {
        int direction = Random.Range(MinDirections, MaxDirections);

        switch (direction)
        {
            case 0:
                MyRigidBody2D.velocity = Vector2.up * Speed;
                break;
            case 1:
                MyRigidBody2D.velocity = Vector2.down * Speed;
                break;
            case 2:
                MyRigidBody2D.velocity = Vector2.left * Speed;
                break;
            case 3:
                MyRigidBody2D.velocity = Vector2.right * Speed;
                break;
            default:
                break;
        }
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
        MyRigidBody2D.velocity = OldVelocity * -1;
    }
}
