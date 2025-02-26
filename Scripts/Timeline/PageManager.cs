using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class PageManager : MonoBehaviour
{
    public GameObject[] pages;
    public Button nextButton;
    public Button backButton;
    public Button closeButton;
    public Canvas mainCanvas; 
    public PlayableDirector timelineDirector;

    private int currentPageIndex = 0;

    AllAudio allAudio;

    private void Awake()
    {
        allAudio = GameObject.FindGameObjectWithTag("Audio").GetComponent<AllAudio>();
    }
    void Start()
    {
        UpdateUI();
    }

    public void NextPage()
    {
        allAudio.PlaySFX(allAudio.pageflip);
        if (currentPageIndex < pages.Length - 1)
        {
            pages[currentPageIndex].SetActive(false);
            currentPageIndex++;

            if (currentPageIndex < pages.Length)
            {
                pages[currentPageIndex].SetActive(true);
            }

            UpdateUI();
        }
        else
        {
            Debug.LogWarning("NextPage: No more pages available.");
        }
    }

    public void PreviousPage()
    {
        allAudio.PlaySFX(allAudio.pageflip);
        if (currentPageIndex > 0)
        {
            pages[currentPageIndex].SetActive(false);
            currentPageIndex--;
            pages[currentPageIndex].SetActive(true);
            UpdateUI();
        }
    }

    public void CloseUI()
    {
        allAudio.PlaySFX(allAudio.closeSound);
        foreach (var page in pages)
        {
            page.SetActive(false);
        }

        closeButton.gameObject.SetActive(false);

        if (mainCanvas != null)
        {
            mainCanvas.gameObject.SetActive(false);
        }

        if (timelineDirector != null)
        {
            timelineDirector.Play();
        }
    }

    private void UpdateUI()
    {
        if (currentPageIndex >= 0 && currentPageIndex < pages.Length)
        {
            var backColor = backButton.image.color;
            backColor.a = currentPageIndex == 0 ? 0.3f : 1.0f;
            backButton.image.color = backColor;
            backButton.interactable = currentPageIndex > 0;
        }

        if (currentPageIndex >= 0 && currentPageIndex < pages.Length)
        {
            var nextColor = nextButton.image.color;
            nextColor.a = currentPageIndex == pages.Length - 1 ? 0.3f : 1.0f;
            nextButton.image.color = nextColor;
            nextButton.interactable = currentPageIndex < pages.Length - 1;
        }

        if (closeButton != null)
        {
            closeButton.gameObject.SetActive(currentPageIndex == pages.Length - 1);
        }
    }
}