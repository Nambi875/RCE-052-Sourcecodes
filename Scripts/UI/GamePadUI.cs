using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class GamePadUI : MonoBehaviour
{
    public GameObject firstSelectedButton;

    private void Start()
    {
        EventSystem.current.SetSelectedGameObject(firstSelectedButton);
    }

    private void Update()
    {
        if (EventSystem.current.currentSelectedGameObject == null)
        {
            EventSystem.current.SetSelectedGameObject(firstSelectedButton);
        }

        if (Gamepad.current != null)
        {
            if (EventSystem.current.currentSelectedGameObject == null)
            {
                EventSystem.current.SetSelectedGameObject(firstSelectedButton);
            }
        }
    }
}