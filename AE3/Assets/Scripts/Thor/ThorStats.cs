using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class ThorStats : MonoBehaviour
{
    [Header("Health Settings")]
    [SerializeField] private int Health = 0;
    [SerializeField] private int MinHealth = 0;
    [SerializeField] private int MaxHealth = 0;
    [SerializeField] private Slider HealthBar;

    [Header("Magic Settings")]
    [SerializeField] private int Magic = 0;
    [SerializeField] private int MinMagic = 0;
    [SerializeField] private int MaxMagic = 0;
    [SerializeField] private Slider MagicBar;

    [Header("Death Settings")]
    [SerializeField] private string DeathParameter;
    [SerializeField] private float BlinkDuration;
    [SerializeField] private float BlinkInterval;
    [SerializeField] private string PlayerLayer;
    [SerializeField] private string InvenurableLayer;
    private Thor Player;
    private Animator MyAnimator;
    private SpriteRenderer MyRenderer;
    private Vector3 EnteredAreaPosition;
    private int EnterededAreaHealth;

    [Header("Keys Settings")]
    [SerializeField] private int Keys = 0;
    [SerializeField] private int MinKeys = 0;
    [SerializeField] private TextMeshProUGUI KeysText;

    [Header("Jewels Settings")]
    [SerializeField] private int Jewels = 0;
    [SerializeField] private int MinJewels = 0;
    [SerializeField] private TextMeshProUGUI JewelsText;

    [Header("Score Settings")]
    [SerializeField] private int Score = 0;
    [SerializeField] private TextMeshProUGUI ScoreText;

    [Header("Item Settings")]
    [SerializeField] private Image ItemImage;
    [SerializeField] private Sprite ItemSprite;
    [SerializeField] private bool HasItem;
    [SerializeField] private GameObject ItemButton;

    [Header("Audio")]
    [SerializeField] private string DeathClipName;
    [SerializeField] private string HurtClipName;

    void Start()
    {
        HealthBar.maxValue = MaxHealth;
        HealthBar.value = Health;

        Player = GetComponent<Thor>();
        MyAnimator = GetComponent<Animator>();
        MyRenderer = GetComponent<SpriteRenderer>();

        MagicBar.maxValue = MaxMagic;
        MagicBar.value = MinMagic;

        KeysText.text = Keys.ToString();
        JewelsText.text = Jewels.ToString();
        ScoreText.text = Score.ToString();
    }

    #region Death
    public void EnteredArea(Vector3 position)
    {
        EnteredAreaPosition = position;
        EnterededAreaHealth = Health;
    }

    public void Died()
    {
        SFXAudioManager.SFXManager.PlaySFX(DeathClipName);
        Player.ToggleInput();

        gameObject.layer = LayerMask.NameToLayer(InvenurableLayer);

        MyAnimator.SetTrigger(DeathParameter);
    }

    public void Revive()
    {
        gameObject.transform.position = new Vector3(EnteredAreaPosition.x, EnteredAreaPosition.y, gameObject.transform.position.z);
        Health = EnterededAreaHealth;
        HealthBar.value = Health;

        Player.ToggleInput();
        StartCoroutine(Blink());
    }

    IEnumerator Blink()
    {
        var EndTime = Time.time + BlinkDuration;

        while (Time.time < EndTime)
        {
            MyRenderer.enabled = false;
            yield return new WaitForSeconds(BlinkInterval);
            MyRenderer.enabled = true;
            yield return new WaitForSeconds(BlinkInterval);
        }

        gameObject.layer = LayerMask.NameToLayer(PlayerLayer);
    }

    #endregion

    #region Health
    public void TakeDamage(int damage)
    {
        Health -= damage;

        SFXAudioManager.SFXManager.PlaySFX(HurtClipName);

        if (Health <= MinHealth)
        {
            Died();
        }

        HealthBar.value = Health;
    }

    public void Heal(int heal)
    {
        Health += heal;

        if (Health >= MaxHealth)
        {
            Health = MaxHealth;
        }

        HealthBar.value = Health;
    }

    public bool DoesPlayerHasMaxHealth()
    {
        return Health >= MaxHealth;
    }
    #endregion

    #region Magic
    public void GetMagic(int value)
    {
        Magic += value;

        if (Magic >= MaxMagic)
        {
            Magic = MaxMagic;
        }

        MagicBar.value = Magic;
    }

    public void UseMagic(int value)
    {
        Magic -= value;

        if (Magic <= MinMagic)
        {
            Magic = MinMagic;
        }

        MagicBar.value = Magic;
    }

    public bool DoesPlayerHasMagic()
    {
        return Magic > MinMagic;
    }

    public bool DoesPlayerHasMaxMagic()
    {
        return Magic >= MaxMagic;
    }
    #endregion

    #region Jewels
    public int GetJewels()
    {
        return Jewels;
    }

    public void CollectJewel(int value)
    {
        Jewels += value;
        JewelsText.text = Jewels.ToString();
    }

    public void UseJewels(int quantity)
    {
        Jewels -= quantity;
        JewelsText.text = Jewels.ToString();
    }
    #endregion

    #region Keys
    public int GetKeys()
    {
        return Keys;
    }
    public void CollectKey()
    {
        ++Keys;
        KeysText.text = Keys.ToString();
    }

    public void UseKey()
    {
        --Keys;
        KeysText.text = Keys.ToString();
    }
    #endregion

    #region Score
    public void IncreaseScore(int value)
    {
        Score += value;
        ScoreText.text = Score.ToString();
    }
    #endregion

    #region Item
    public void GetItem()
    {
        if (!HasItem)
        {
            ItemImage.gameObject.SetActive(true);
            ItemImage.sprite = ItemSprite;

            ItemButton.SetActive(true);

            HasItem = !HasItem;
        }
    }

    public bool IsHoldingItem()
    {
        return HasItem;
    }
    #endregion

}
