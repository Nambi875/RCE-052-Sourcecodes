using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerJumpscare2 : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (Jumpscare.Instance != null)
        {
            Jumpscare.Instance.IsJumpscare = true;
        }
    }
}
