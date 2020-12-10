using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int lives = 3;

    static bool isPlayerDead;
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

    private void Start()
    {
        waveTimer = timeBetweenWaves;

        isPlayerDead = false;
        loseScreen.SetActive(false);
        livesTxt.text = lives.ToString();

        pause = true;
        StartCoroutine(StartGame());
    }

    private void Update()
    {
        if((waveTimer <= 0 || FindObjectsOfType<EnemyScript>().Length == 0) && pause == false)
        {
            Instantiate(waves[Random.Range(0, waves.Length)], waveSpawnPoint.position, Quaternion.identity);
            waveTimer = timeBetweenWaves;
        }
        waveTimer -= Time.deltaTime;

        if(isPlayerDead)
        {
            pause = true;

            if (lives == 0)
                loseScreen.SetActive(true);
            else
            {
                lives--;
                livesTxt.text = lives.ToString();
                isPlayerDead = false;

                StartCoroutine(Respawn());
            }
        }

        if (isPlayerDead && Input.GetButton("Submit"))
            Restart();
        if (Input.GetButton("Cancel"))
            Application.Quit();
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
        SceneManager.LoadScene(1);
    }

    public static void playerDead()
    {
        isPlayerDead = true;
    }
}
