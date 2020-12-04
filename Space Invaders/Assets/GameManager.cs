using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    static bool isPlayerDead;
    static bool hasWon;
    public GameObject loseScreen;
    public GameObject winScreen;

    private void Start()
    {
        isPlayerDead = false;
        loseScreen.SetActive(false);
        winScreen.SetActive(false);
    }

    private void Update()
    {
        if(isPlayerDead)
        {
            loseScreen.SetActive(true);
        }
        else if(hasWon)
        {
            winScreen.SetActive(true);
        }
    }

    public static void WinGame()
    {
        hasWon = true;
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
    }

    public static void playerDead()
    {
        isPlayerDead = true;
    }
}
