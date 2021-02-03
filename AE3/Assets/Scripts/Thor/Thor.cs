using UnityEngine;

public class Thor : MonoBehaviour
{
    [Header("Thor Movement")]
    public Joystick Joystick;
    [SerializeField] private float speed = 1;

    [Header("Thor Animator Variables")]
    [SerializeField] private string MoveParameterX = "";
    [SerializeField] private string MoveParameterY = "";
    [SerializeField] private string IdleParameterX = "";
    [SerializeField] private string IdleParameterY = "";

    [Header("Buttons")]
    [SerializeField] private GameObject PlayerButtons;

    private Rigidbody2D MyRigidbody2D;
    private Animator MyAnimator;

    public bool IsInputLocked = false;

    private void Awake()
    {
        gameObject.GetComponent<ThorStats>().EnteredArea(transform.position);
    }

    void Start()
    {
        MyRigidbody2D = GetComponent<Rigidbody2D>();
        MyAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        if (!IsInputLocked)
        {
            Move();
        }
        else
        {
            MyRigidbody2D.velocity = Vector3.zero;
        }
    }


    public void Move()
    {
        MyRigidbody2D.velocity = new Vector2(Joystick.Horizontal, Joystick.Vertical).normalized * speed;

        MyAnimator.SetFloat(MoveParameterX, MyRigidbody2D.velocity.x);
        MyAnimator.SetFloat(MoveParameterY, MyRigidbody2D.velocity.y);

        if (Joystick.Horizontal != 0 || Joystick.Vertical != 0)
        {
            MyAnimator.SetFloat(IdleParameterX, Joystick.Horizontal);
            MyAnimator.SetFloat(IdleParameterY, Joystick.Vertical);
        }
    }

    public Vector2 GetFacingDirection()
    {
        return new Vector2(MyAnimator.GetFloat(IdleParameterX), MyAnimator.GetFloat(IdleParameterY));
    }

    public void ToggleInput()
    {
        IsInputLocked = !IsInputLocked;

        PlayerButtons.SetActive(!IsInputLocked);

        // Sets these parameters to 0 so it transitions to the Idle animation
        MyAnimator.SetFloat(MoveParameterX, 0f); 
        MyAnimator.SetFloat(MoveParameterY, 0f);

    }
}
