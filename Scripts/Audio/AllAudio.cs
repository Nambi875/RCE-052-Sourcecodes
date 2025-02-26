using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllAudio : MonoBehaviour
{
    //メインメニューサウンド
    [Header("----------Audio Source----------")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource backSource;
    [SerializeField] AudioSource SFXSource;
    [SerializeField] AudioSource footSource;
    [SerializeField] AudioSource voiceSource;

    [Header("----------Audio Clip----------")]
    public AudioClip mainmenu;
    public AudioClip roommusic;
    public AudioClip background;
    public AudioClip footstep;
    public AudioClip doorlocked;
    public AudioClip dooropening;
    public AudioClip doorclosing;
    public AudioClip pageflip;
    public AudioClip npc;
    public AudioClip voice;
    public AudioClip voice_firstentrance;
    public AudioClip openDelaySound;
    public AudioClip openSound;
    public AudioClip closeSound;
    public AudioClip UIclick;
    public AudioClip UIonthemouse;
    public AudioClip scream;

    private void Start()
    {
        backSource.clip = background;
        backSource.Play();

        SFXSource.ignoreListenerPause = true;
    }

    public void PlayBGM(AudioClip clip)
    {
        musicSource.PlayOneShot(clip);
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }

    public void PlayBack(AudioClip clip)
    {
        backSource.PlayOneShot(clip);
        backSource.clip = background;
        backSource.Play();
    }

    public void PlayFoot(AudioClip clip)
    {
        footSource.PlayOneShot(clip);
    }

    public void PlayVoice(AudioClip clip)
    {
        voiceSource.PlayOneShot(clip);
    }

}
