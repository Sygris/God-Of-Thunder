using System.Collections;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [Header("Camera Movement Settings")]
    [SerializeField] private GameObject Camera;
    [SerializeField] private Vector2 Offset; // Camera height - The UI height 
    [SerializeField] private float LerpSmoothness = 2.0f;

    private enum Direction { Vertical, Horizontal };

    [Header("Camera Direction Settings")]
    [SerializeField] private Direction CameraDirection;

    [Header("Player Settings")]
    [SerializeField] private GameObject Player;
    [SerializeField] private Vector2 PlayerOffset; // How much the player should move when the camera transitions

    private Thor Thor;
    private ThorStats ThorStats;
    private AreaManager AreaManager;

    private void Start()
    {
        Thor = Player.GetComponent<Thor>();
        ThorStats = Player.GetComponent<ThorStats>();
        AreaManager = GetComponent<AreaManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(Player.tag))
        {
            Transform target = collision.transform;

            Vector3 NewPlayerPosition = Player.transform.position;
            Vector3 NewCameraPosition = Camera.transform.position;

            switch (CameraDirection)
            {
                case Direction.Vertical:
                    if (target.position.y < transform.position.y)
                    {
                        NewPlayerPosition.y += PlayerOffset.y;
                        NewCameraPosition.y += Offset.y;
                    }
                    else if (target.position.y > transform.position.y)
                    {
                        NewPlayerPosition.y -= PlayerOffset.y;
                        NewCameraPosition.y -= Offset.y;
                    }

                    break;
                case Direction.Horizontal:
                    if (target.position.x < transform.position.x)
                    {
                        NewPlayerPosition.x += PlayerOffset.x;
                        NewCameraPosition.x += Offset.x;
                    }
                    else if (target.position.x > transform.position.x)
                    {
                        NewPlayerPosition.x -= PlayerOffset.x;
                        NewCameraPosition.x -= Offset.x;
                    }
                    break;
                default:
                    break;
            }

            StartCoroutine(MoveCamera(NewCameraPosition, NewPlayerPosition));
        }
    }

    IEnumerator MoveCamera(Vector3 NewCameraPosition, Vector3 NewPlayerPosition)
    {
        Player.transform.position = NewPlayerPosition;

        Thor.ToggleInput();

        AreaManager.EnableNextArea();

        float timeElapsed = 0;

        while (timeElapsed < LerpSmoothness)
        {
            Camera.transform.position = Vector3.Lerp(Camera.transform.position, NewCameraPosition, timeElapsed / LerpSmoothness);
            timeElapsed += Time.deltaTime;

            yield return null;
        }

        AreaManager.DisablePreviousArea();

        Thor.ToggleInput();
        ThorStats.EnteredArea(Player.transform.position);
    }
}
