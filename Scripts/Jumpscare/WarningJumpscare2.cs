using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarningJumpscare2 : MonoBehaviour
{
    [Header("-----GameObject------")]
    public GameObject JumpscareSoundTrigger;

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = JumpscareSoundTrigger.GetComponent<AudioSource>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (audioSource != null && !audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
    }
}
