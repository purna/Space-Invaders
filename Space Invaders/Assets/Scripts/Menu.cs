using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public GameObject flashingText;
    public float interval = 0.5f;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        InvokeRepeating("Flash", interval, interval);
    }

    void Flash()
    {
        flashingText.SetActive(!flashingText.activeSelf);
    }

    private void Update()
    {
        if (Input.GetButton("Submit"))
            UnityEngine.SceneManagement.SceneManager.LoadScene("Game");
        if (Input.GetButton("Cancel"))
            Application.Quit();
    }
}
