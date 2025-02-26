using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class TimelineController : MonoBehaviour
{
    public PlayableDirector timeline;
    public Canvas uiCanvas;
    public double pauseTime ; 
    public double SkipTime;
    public GameObject skipButton;

    private bool isPaused = false;

    void Update()
    {
        if (!isPaused && timeline.time >= pauseTime && timeline.state == PlayState.Playing)
        {
            PauseTimeline();
        }
    }

    void PauseTimeline()
    {
        timeline.Pause();
        isPaused = true;

        if (uiCanvas != null)
        {
            uiCanvas.gameObject.SetActive(true);
        }
    }

    public void ResumeTimeline()
    {
        if (uiCanvas != null)
        {
            uiCanvas.gameObject.SetActive(false);
        }

        if (timeline != null)
        {
            timeline.Play();
        }

        isPaused = false;
    }

    public void SkipToPauseTime()
    {
        if (timeline != null)
        {
            timeline.time = SkipTime;
            timeline.Evaluate(); 
        }

        if (skipButton != null)
        {
            skipButton.SetActive(false);
        }
    }
}