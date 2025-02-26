using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorDoor : MonoBehaviour
{
    [Header("-----Elevator doors-----")]
    public Transform leftDoor; 
    public Transform rightDoor; 

    public Vector3 leftDoorOpenPosition;  
    public Vector3 rightDoorOpenPosition; 

    public Vector3 leftDoorClosedPosition;  
    public Vector3 rightDoorClosedPosition; 

    public float doorSpeed = 2f;  
    public float openDelay = 2f; 
    public float closeDelay = 2f;

    private bool isOpening = false;
    private bool isClosing = false;
    private bool hasOpened = false;     
    private bool isPlayerInside = false;
    private bool isDoorClosed = true;    
    private bool closeSoundPlayed = false;
    private Coroutine closeDoorCoroutine;

    AllAudio allAudio;
    private void Awake()
    {
        allAudio = GameObject.FindGameObjectWithTag("Audio").GetComponent<AllAudio>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInside = true;

            if (!hasOpened)
            {
                StartCoroutine(OpenDoorWithDelay());
                hasOpened = true;
            }

            if (closeDoorCoroutine != null)
            {
                StopCoroutine(closeDoorCoroutine);
                closeDoorCoroutine = null;
            }

            isDoorClosed = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInside = false;

            if (!isDoorClosed) 
            {
                closeDoorCoroutine = StartCoroutine(CloseDoorWithDelay());
            }
        }
    }

    private IEnumerator OpenDoorWithDelay()
    {
        allAudio.PlayBack(allAudio.openDelaySound);

        if (isOpening || isClosing) yield break;

        yield return new WaitForSeconds(openDelay);

        isOpening = true;
        isClosing = false;

        Vector3 initialLeftPos = leftDoor.localPosition;
        Vector3 initialRightPos = rightDoor.localPosition;

        allAudio.PlayBack(allAudio.openSound);

        float elapsedTime = 0f;
        while (elapsedTime < doorSpeed)
        {
            leftDoor.localPosition = Vector3.Lerp(initialLeftPos, leftDoorOpenPosition, elapsedTime / doorSpeed);
            rightDoor.localPosition = Vector3.Lerp(initialRightPos, rightDoorOpenPosition, elapsedTime / doorSpeed);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        leftDoor.localPosition = leftDoorOpenPosition;
        rightDoor.localPosition = rightDoorOpenPosition;

        isOpening = false;
    }

    private IEnumerator CloseDoorWithDelay()
    {
        yield return new WaitForSeconds(closeDelay);

        if (!isPlayerInside && !isDoorClosed)
        {
            StartCoroutine(CloseDoor());
        }

        closeDoorCoroutine = null; 
    }

    private IEnumerator CloseDoor()
    {
        if (isClosing || isOpening) yield break;

        isClosing = true;

        Vector3 initialLeftPos = leftDoor.localPosition;
        Vector3 initialRightPos = rightDoor.localPosition;

        if (!closeSoundPlayed)
        {
            allAudio.PlayBack(allAudio.closeSound);
            closeSoundPlayed = true;  
        }

        float elapsedTime = 0f;
        while (elapsedTime < doorSpeed)
        {
            leftDoor.localPosition = Vector3.Lerp(initialLeftPos, leftDoorClosedPosition, elapsedTime / doorSpeed);
            rightDoor.localPosition = Vector3.Lerp(initialRightPos, rightDoorClosedPosition, elapsedTime / doorSpeed);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        leftDoor.localPosition = leftDoorClosedPosition;
        rightDoor.localPosition = rightDoorClosedPosition;

        isClosing = false;
        isDoorClosed = true;
    }
}