using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SubtitleManager : MonoBehaviour
{
    public TextMeshProUGUI subtitleText;

    public void UpdateSubtitle(string newSubtitle)
    {
        Debug.Log($"Subtitle updated to: {newSubtitle}");
        subtitleText.text = newSubtitle;
    }

    public void SetSubtitle(string subtitle)
    {
        subtitleText.text = subtitle.Replace("\\n", "\n");
    }

}
