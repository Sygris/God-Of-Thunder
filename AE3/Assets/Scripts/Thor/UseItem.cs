using UnityEngine;

public class UseItem : MonoBehaviour
{
    [Header("Use Item Settings")]
    [SerializeField] private int HealthPoints;
    [SerializeField] private int MagicPoints;
    [SerializeField] private float Duration;

    [Header("Audio")]
    [SerializeField] private string ClipName;
    private bool IsClipPlaying = false;

    private ThorStats PlayerStats;
    private bool IsHolding;
    private float Timer;

    private void Start()
    {
        PlayerStats = GetComponent<ThorStats>();
    }

    private void Update()
    {
        if (IsHolding)
        {
            Timer += Time.deltaTime;

            if (Timer >= Duration && PlayerStats.IsHoldingItem() && !PlayerStats.DoesPlayerHasMaxHealth() && PlayerStats.DoesPlayerHasMagic())
            {
                PlayClip();

                PlayerStats.Heal(HealthPoints);
                PlayerStats.UseMagic(MagicPoints);

                Timer = 0;
            }
            else
            {
                SFXAudioManager.SFXManager.StopClip();
                IsClipPlaying = false;
            }

        }
    }

    public void Click()
    {
        IsHolding = true;
    }

    public void Release()
    {
        IsHolding = false;
    }

    private void PlayClip()
    {
        if (!IsClipPlaying)
        {
            SFXAudioManager.SFXManager.PlaySFXWithoutDestroying(ClipName);
            IsClipPlaying = true;
        }
    }
}
