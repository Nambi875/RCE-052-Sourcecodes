using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Jumpscare : MonoBehaviour
{
    public static Jumpscare Instance { get; private set; }

    [Header("Game Objects")]
    public GameObject monster;
    public GameObject flashlight;

    public bool IsJumpscare = false;
    private bool hasTriggered = false;
    AllAudio allAudio;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        allAudio = GameObject.FindGameObjectWithTag("Audio").GetComponent<AllAudio>();
    }

    private void Update()
    {
        if (IsJumpscare && !hasTriggered)
        {
            hasTriggered = true;
            TriggerJumpscare();
        }
        monster.SetActive(IsJumpscare);
    }

    public void TriggerJumpscare()
    {
        allAudio.PlayVoice(allAudio.scream);
        flashlight.SetActive(false);
        monster.SetActive(true);
        StartCoroutine(ShakeCamera(Camera.main));
        StartCoroutine(AfterChangeScene());

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            PlayerMovement movement = player.GetComponent<PlayerMovement>();
            PlayerFootstep footstep = player.GetComponent<PlayerFootstep>();
            if (movement != null)
            {
                footstep.enabled = false;
                movement.enabled = false;
            }
        }
    }

    private IEnumerator ShakeCamera(Camera cam)
    {
        float duration = 2.5f;
        float magnitude = 0.3f;
        Vector3 originalPos = cam.transform.localPosition;

        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            cam.transform.localPosition = originalPos + (Vector3)(Random.insideUnitCircle * magnitude);
            yield return null;
        }

        cam.transform.localPosition = originalPos;
    }

    private IEnumerator AfterChangeScene()
    {
        GameMng.Getins.ResetScore();
        yield return new WaitForSeconds(2f);
        IsJumpscare = false;
        hasTriggered = false;
        SceneManager.LoadScene("Room-0");
    }
}
