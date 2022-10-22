using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDetector : MonoBehaviour
{
    public Action<bool> PlayerOnTarget;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerOnTarget?.Invoke(true);
        Debug.Log("true");
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        PlayerOnTarget?.Invoke(false);
        Debug.Log("false");

    }


}
