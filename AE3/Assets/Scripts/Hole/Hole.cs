using System.Collections;
using UnityEngine;

public class Hole : MonoBehaviour
{
    [Header("Hole Settings")]
    [SerializeField] private GameObject ExitPoint;
    [SerializeField] private GameObject Player;

    [Header("Camera Settings")]
    [SerializeField] private Camera Camera;
    [SerializeField] private Vector3 CameraPosition;
    [SerializeField] private float LockInputDurantion;

    [Header("Audio")]
    [SerializeField] private string ClipName;

    private ThorStats PlayerStats;
    private Thor PlayerMovement;
    private AreaManager AreaManager;

    private void Start()
    {
        PlayerStats = Player.GetComponent<ThorStats>();
        PlayerMovement = Player.GetComponent<Thor>();
        AreaManager = GetComponent<AreaManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(Player.tag))
        {
            SFXAudioManager.SFXManager.PlaySFX(ClipName);

            StartCoroutine(Teletransport());
        }
    }

    IEnumerator Teletransport()
    {
        PlayerMovement.ToggleInput();
        AreaManager.EnableNextArea();

        Player.transform.position = ExitPoint.transform.position;

        PlayerStats.EnteredArea(ExitPoint.transform.position);

        Camera.transform.position = CameraPosition;

        yield return new WaitForSeconds(LockInputDurantion);

        AreaManager.DisablePreviousArea();
        PlayerMovement.ToggleInput();
    }

}
