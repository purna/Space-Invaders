using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int lives = 3;

    static bool isPlayerDead;
    public GameObject Leaderboard;
    public GameObject loseScreen;
    public GameObject playerPrefab;
    public Text livesTxt;
    public Text startTxt;
    public float respawnTime = 1f;
    public GameObject[] waves;
    public Transform waveSpawnPoint;
    public int timeBetweenWaves = 20;
    float waveTimer;

    public static bool pause = false;

    HighscoreTable highscoreTable;

    bool hasUpdatedLeaderboard = false;

    private void Start()
    {
        highscoreTable = FindObjectOfType<HighscoreTable>();
        waveTimer = timeBetweenWaves;

        isPlayerDead = false;
        loseScreen.SetActive(false);
        livesTxt.text = lives.ToString();

        pause = true;
        StartCoroutine(StartGame());
    }

    private void Update()
    {

        if (isPlayerDead && Input.GetButtonDown("Submit") && loseScreen.activeSelf)
            Restart();
        if (Input.GetKeyDown(KeyCode.K)) isPlayerDead = true;

        if ((waveTimer <= 0 || FindObjectsOfType<EnemyScript>().Length == 0) && pause == false)
        {
            Instantiate(waves[Random.Range(0, waves.Length)], waveSpawnPoint.position, Quaternion.identity);
            waveTimer = timeBetweenWaves;
        }
        waveTimer -= Time.deltaTime;

        if(isPlayerDead)
        {
            pause = true;

            if (lives == 0)
            {
                if(hasUpdatedLeaderboard == false)
                    UpdateLeaderboard();

                if (Input.GetButtonDown("Submit") && !loseScreen.activeSelf)
                {
                    Leaderboard.SetActive(false);
                    loseScreen.SetActive(true);
                }

            }
            else
            {
                lives--;
                livesTxt.text = lives.ToString();
                isPlayerDead = false;

                StartCoroutine(Respawn());
                hasUpdatedLeaderboard = false;
            }
        }
    }

    IEnumerator StartGame()
    {
        yield return new WaitForSeconds(respawnTime);
        pause = false;
        startTxt.enabled = false;
    }

    IEnumerator Respawn()
    {
        yield return new WaitForSeconds(respawnTime);

        Instantiate(playerPrefab, playerPrefab.transform.position, Quaternion.identity);
        pause = false;
    }

    public void Restart()
    {
        isPlayerDead = false;
        pause = false;
        Score.score = 0;
        SceneManager.LoadScene(2);
    }

    public static void playerDead()
    {
        isPlayerDead = true;
    }

    void UpdateLeaderboard()
    {
        Leaderboard.SetActive(true);

        if (highscoreTable == null)
            highscoreTable = FindObjectOfType<HighscoreTable>();

        int pos = highscoreTable.CheckNewEntry(Score.score);
        Debug.Log(pos);
        if (pos != -1)
        {
            highscoreTable.AddNewEntry(pos, new HighscoreTable.HighscoreEntry(pos, Score.score, PlayerPrefs.GetString("init", "NUL")));
        }

        highscoreTable.UpdateLeaderBoard();
        highscoreTable.SaveBoard();

        hasUpdatedLeaderboard = true;
    }
}
