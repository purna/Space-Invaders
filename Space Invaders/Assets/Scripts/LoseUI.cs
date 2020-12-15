using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoseUI : MonoBehaviour
{
    public GameObject restart;
    public float interval = 0.5f;

    private void Start()
    {
        InvokeRepeating("FlashRestart", 0, interval);
    }

    void FlashRestart()
    {
        restart.SetActive(!restart.activeSelf);
    }
}
