using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 
using DG.Tweening; 
using UnityEngine.UI;

public class NpcDialogSystem : MonoBehaviour
{
    public TextMeshProUGUI dialogueText; 
    [SerializeField] private List<string> dialogues = new List<string>(); 
    [SerializeField] private List<AudioSource> voiceOverSources = new List<AudioSource>();
    public float typingSpeed = 0.05f; 
    public float fadeDuration = 1.0f;
    private bool isPlayerNear = false;
    private bool isTyping = false; 
    private int currentDialogueIndex = 0; 
    private Coroutine typingCoroutine;

    public NpcAimSystem npcAimSystem;

    void Update()
    {
        if (isPlayerNear && Input.GetKeyDown(KeyCode.E) && !isTyping)
        {
            if (typingCoroutine != null)
            {
                StopCoroutine(typingCoroutine);
                typingCoroutine = null;
                dialogueText.text = dialogues[currentDialogueIndex];
                PlayVoiceOver(); 
                NextDialogue();
            }
            else
            {
                typingCoroutine = StartCoroutine(DisplayDialogueWithEffects());
                StartConversation();
            }
        }
    }

    IEnumerator DisplayDialogueWithEffects()
    {
        isTyping = true;
        dialogueText.text = "";
        dialogueText.alpha = 1f;

        string currentDialogue = dialogues[currentDialogueIndex];

        foreach (char letter in currentDialogue)
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        PlayVoiceOver(); 
        isTyping = false; 

        yield return new WaitForSeconds(3.0f);
        NextDialogue(); 
    }

    void PlayVoiceOver()
    {
        if (currentDialogueIndex < voiceOverSources.Count && voiceOverSources[currentDialogueIndex] != null)
        {
            voiceOverSources[currentDialogueIndex].Play(); 
        }
    }

    void NextDialogue()
    {
        currentDialogueIndex++;
        if (currentDialogueIndex >= dialogues.Count)
        {
            currentDialogueIndex = 0;
            EndConversation();
        }
    }

    void StartConversation()
    {
        if (npcAimSystem != null)
        {
            npcAimSystem.StartConversation();
        }
    }

    void EndConversation()
    {
        if (npcAimSystem != null)
        {
            npcAimSystem.EndConversation(); 
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
            if (typingCoroutine != null)
            {
                StopCoroutine(typingCoroutine);
                typingCoroutine = null;
            }
            EndConversation(); 
            StartCoroutine(FadeOutDialogue());
            if (currentDialogueIndex < voiceOverSources.Count && voiceOverSources[currentDialogueIndex] != null)
            {
                voiceOverSources[currentDialogueIndex].Stop(); 
            }
        }
    }

    IEnumerator FadeOutDialogue()
    {
        dialogueText.DOFade(0f, fadeDuration);
        yield return new WaitForSeconds(fadeDuration);
        dialogueText.text = "";
    }
}