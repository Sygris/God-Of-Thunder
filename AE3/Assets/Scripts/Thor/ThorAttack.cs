using UnityEngine;

public class ThorAttack : MonoBehaviour
{
    [Header("Attack Settings")]
    [SerializeField] private GameObject Weapon;

    private Thor ThorScript;
    private bool IsAttacking;

    private void Start()
    {
        ThorScript = gameObject.GetComponent<Thor>();
    }

    void Update()
    {
        if (transform.childCount == 0 && IsAttacking) // If the Hammer is destroyed (No childs) and the player is attacking toggle the IsAttacking
        {
            IsAttacking = !IsAttacking;
        }
    }

    public void Attack()
    {
        if (!IsAttacking)
        {
            IsAttacking = !IsAttacking;

            Instantiate(Weapon, transform.position, Quaternion.Euler(0, 0, Direction()), transform);
        }
    }

    private float Direction()
    {
        Vector2 ThorFacingDirection = ThorScript.GetFacingDirection();

        float angle = -90; // Default is Down because that's the direction the player is facing when the game starts

        if (Mathf.Abs(ThorFacingDirection.x) > Mathf.Abs(ThorFacingDirection.y)) // If player last movement was horizontally
        {
            if (ThorFacingDirection.x > 0)
                angle = 0; // Right
            else
                angle = 180; // Left
        }
        else // If player last movement was vertically
        {
            if (ThorFacingDirection.y > 0)
                angle = 90; // Up
        }

        return angle;
    }
}
