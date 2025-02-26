using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SubtitleVoice : MonoBehaviour
{
    public string subtitleText;
    public TMP_Text subtitleUI;
    public float subtitleDuration = 3.0f;
    public float fadeDuration = 1.0f;

    private bool triggered = false;
    public AudioSource audioSource;

    private void OnTriggerEnter(Collider other)
    {
        if (!triggered && other.CompareTag("Player"))
        {
            triggered = true;
            PlayVoiceAndSubtitle();
        }
    }

    void PlayVoiceAndSubtitle()
    {
        audioSource.Play();

        if (subtitleUI != null)
        {
            subtitleUI.text = subtitleText;
            StartCoroutine(SubtitleFadeInOut());
        }
    }

    IEnumerator SubtitleFadeInOut()
    {
        Color originalColor = subtitleUI.color;
        float timer = 0f;

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            float alpha = Mathf.Lerp(0f, 1f, timer / fadeDuration);
            subtitleUI.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            yield return null;
        }

        yield return new WaitForSeconds(subtitleDuration);

        timer = 0f;

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, timer / fadeDuration);
            subtitleUI.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            yield return null;
        }

        subtitleUI.text = "";
    }
}