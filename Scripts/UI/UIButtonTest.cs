using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIButtonTest : MonoBehaviour
{
    public Button myButton;

    private void Start()
    {
        myButton.onClick.AddListener(LogMessage);
    }

    void LogMessage()
    {
        Debug.Log("Ang");
    }
}
