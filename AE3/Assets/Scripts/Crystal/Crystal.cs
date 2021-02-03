using UnityEngine;

public class Crystal : MonoBehaviour
{
    [Header("Crystal Settings")]
    [SerializeField] private string HammerTag;

    [Header("Log Seetings")]
    [SerializeField] private Animator Logs;
    [SerializeField] private string LogsAnimationParameter;
    [SerializeField] private bool LogState;

    [Header("Audio")]
    [SerializeField] private string ClipName;

    private void Start()
    {
        Logs.SetBool(LogsAnimationParameter, LogState);
    }

    private void OnEnable()
    {
        Logs.SetBool(LogsAnimationParameter, LogState);
    }

    private void ToggleAnimation()
    {
        LogState = !LogState;

        Logs.SetBool(LogsAnimationParameter, LogState);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(HammerTag))
        {
            SFXAudioManager.SFXManager.PlaySFX(ClipName);
            ToggleAnimation();
        }
    }
}
