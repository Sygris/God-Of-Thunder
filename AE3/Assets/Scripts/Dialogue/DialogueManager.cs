using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    private Queue<string> Sentences;
    private Thor ThorInput;

    [Header("Dialogue UI")]
    [SerializeField] private GameObject DialogueUI;
    [SerializeField] private GameObject TextDialogue;
    [SerializeField] private TextMeshProUGUI DialogueText;

    [Header("Animation Settings")]
    [SerializeField] private GameObject AnimationIcon;
    [SerializeField] private string AnimationParameter;

    [Header("Buttons Settings")]
    [SerializeField] private GameObject ContinueButton;
    [SerializeField] private TextMeshProUGUI ContinueButtonText;
    [SerializeField] private GameObject SkipButton;
    [SerializeField] private string EndText; // When the dialogue shows the last message what text to print in the continue button
    private string BeginText; // Default text of the continue button;

    [Header("Typewriter Effect Settings")]
    [SerializeField] private float Velocity;

    [Header("Audio")]
    [SerializeField] private string TypeWriterClipName;
    [SerializeField] private string EndMessageClipName;


    private string CurrentSentence;

    private void Start()
    {
        Sentences = new Queue<string>();
        BeginText = ContinueButtonText.text;

        ThorInput = GameObject.Find("Thor").GetComponent<Thor>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        ThorInput.ToggleInput();
        Sentences.Clear();
        ContinueButtonText.text = BeginText;
        Time.timeScale = 0;

        foreach (string sentence in dialogue.Sentences)
        {
            Sentences.Enqueue(sentence);
        }

        DialogueUI.SetActive(true);
        TextDialogue.SetActive(true);

        DisplayNextSentence();
    }

    public void Skip()
    {
        StopAllCoroutines();

        if (Sentences.Count == 0 && DialogueText.text == CurrentSentence)
        {
            EndDialogue();
        }

        DialogueText.text = CurrentSentence;
        AnimationIcon.GetComponent<Animator>().SetBool(AnimationParameter, false);

        ContinueButton.SetActive(true);

    }

    public void DisplayNextSentence()
    {
        ContinueButton.SetActive(false);
        AnimationIcon.GetComponent<Animator>().SetBool(AnimationParameter, true);

        if (Sentences.Count == 1) // Final message
        {
            ContinueButtonText.text = EndText;
        }

        if (Sentences.Count == 0)
        {
            ContinueButton.SetActive(true);

            EndDialogue();
            return;
        }

        CurrentSentence = Sentences.Dequeue();

        StopAllCoroutines();
        StartCoroutine(TypeWriter());
    }

    IEnumerator TypeWriter()
    {
        DialogueText.text = "";

        foreach (char letter in CurrentSentence.ToCharArray())
        {
            DialogueText.text += letter;
            SFXAudioManager.SFXManager.PlaySFX(TypeWriterClipName);

            yield return new WaitForSecondsRealtime(Velocity);
        }

        SFXAudioManager.SFXManager.PlaySFX(EndMessageClipName);
        AnimationIcon.GetComponent<Animator>().SetBool(AnimationParameter, false);
        ContinueButton.SetActive(true);
    }

    public void EndDialogue()
    {
        StopAllCoroutines();

        DialogueUI.SetActive(false);
        TextDialogue.SetActive(false);

        Time.timeScale = 1;
        ThorInput.ToggleInput();
    }

}
