using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitalSetup : MonoBehaviour
{
    public UnityEngine.UI.InputField input;

    public void Continue()
    {
        PlayerPrefs.SetString("init", input.text);
        UnityEngine.SceneManagement.SceneManager.LoadScene(2);
    }
}
