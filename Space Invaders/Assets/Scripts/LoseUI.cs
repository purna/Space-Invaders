using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoseUI : MonoBehaviour
{
    public GameObject flashingText;
    public float interval = 0.5f;

    private void Start()
    {
        InvokeRepeating("Flash", interval, interval);
    }

    void Flash()
    {
        flashingText.SetActive(!flashingText.activeSelf);
    }
}
