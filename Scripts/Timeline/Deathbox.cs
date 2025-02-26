using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Deathbox : MonoBehaviour
{
    [Header("Jump Scare Settings")]
    public GameObject jumpScareObject; 
    public PlayableDirector timelineDirector; 
    public float waitTime = 3f; 

    private bool isPlayerInside = false; 
    private bool isProcessing = false; 

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isProcessing)
        {
            isPlayerInside = true;
            StartCoroutine(TriggerEffect());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInside = false;
        }
    }

    private IEnumerator TriggerEffect()
    {
        isProcessing = true;

        // 3s wait
        yield return new WaitForSeconds(waitTime);

        if (!isPlayerInside)
        {
            isProcessing = false;
            yield break;
        }

        if (jumpScareObject != null)
        {
            jumpScareObject.SetActive(true);
        }

        if (timelineDirector != null)
        {
            timelineDirector.Play();
        }

        yield return new WaitForSeconds(1.5f);
        if (jumpScareObject != null)
        {
            jumpScareObject.SetActive(false);
        }

        isProcessing = false;
    }
}