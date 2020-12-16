using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class HighscoreTable : MonoBehaviour
{
    Highscores highscores;
    public LeaderboardEntry[] entries;

    //string jsonDir;
    //string fileSuffix = "leaderboard.json";
    string domain = "https://www.pipress.com/game/";


    [System.Serializable]
    public class HighscoreEntry
    {
        public int id = 0;
        public int score = 0;
        public string name = "";

        public HighscoreEntry()
        {
            id = 0;
            score = 100;
            name = "NUL";
        }

        public HighscoreEntry(int id, int score, string name)
        {
            this.id = id;
            this.score = score;
            this.name = name;
        }
    }

    public class Highscores
    {
        public List<HighscoreEntry> highscoreEntryList;
    }

    private Highscores ReadHighscores()
    {
        FileSetup();

        string result = PlayerPrefs.GetString("highscoreTable");
        return JsonUtility.FromJson<Highscores>(result);

        /*
        string result = System.IO.File.ReadAllText(jsonDir);
        return JsonUtility.FromJson<Highscores>(result);
        */
    }

    private void WriteHighscores()
    {

        string json = JsonUtility.ToJson(highscores);
        /*
        System.IO.File.WriteAllText(jsonDir, json);
        */
        PlayerPrefs.SetString("highscoreTable", json);
        PlayerPrefs.Save();


    }

    public void UpdateLeaderBoard()
    {
        for (int i = 0; i < 10; i++)
        {
            highscores.highscoreEntryList[i].id = i + 1;
            entries[i].SetEntry(highscores.highscoreEntryList[i]);
        }
    }

    public void ResetLeaderboard()
    {
        highscores = new Highscores();
        highscores.highscoreEntryList = new List<HighscoreEntry>();
        for (int i = 0; i < 10; i++)
            highscores.highscoreEntryList.Add(new HighscoreEntry());

        WriteHighscores();

        UpdateLeaderBoard();
    }

    private void Start()
    {
        FileSetup();

        highscores = ReadHighscores();
        UpdateLeaderBoard();


    }

    public int CheckNewEntry(int score)
    {
        bool isGreater = true;
        int i = 10;

        highscores = ReadHighscores();

        while (isGreater && i > 0)
        {
            i -= 1;

            if (score > highscores.highscoreEntryList[i].score)
            {
                Debug.Log(score + " > " + highscores.highscoreEntryList[i].score);
            }
            else
            {
                isGreater = false;
                i += 1;
            }
        }

        if (i == 9)
            return -1;
        else
            return i;
    }

    public void AddNewEntry(int pos, HighscoreEntry entry)
    {
        highscores.highscoreEntryList.Insert(pos, entry);

        //INSERT RECORD INTO DATABASE
        StartCoroutine(InsertScore(entry.name, entry.score));
    }

    public void SaveBoard()
    {
        WriteHighscores();

    }

    void FileSetup()
    {
        /*
        jsonDir = Application.persistentDataPath + fileSuffix;
        if (!System.IO.File.Exists(jsonDir))
            System.IO.File.Create(jsonDir);
        */

        //READ RECORD FROM DATABASE
        StartCoroutine(ReadScore());
    }


    IEnumerator ReadScore()
    {
        string url = domain + "scores.php";

        WWWForm formRead = new WWWForm();
        formRead.AddField("action", "read");

        using (UnityWebRequest www = UnityWebRequest.Post(url, formRead))
        {

            yield return www.SendWebRequest();

            string result = www.downloadHandler.text;

            // check for errors
            if (www.error == null)
            {
                Debug.Log("WWW Ok!");
            }
            else if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log("Error #" + result);
            }
            else
            {
                Debug.Log("Unknown Error #" + result);
            }

            /*
               {"highscoreEntryList":[
                    {"id":0,"score":1000000,"name":"CMK"},
                    {"id":0,"score":897621,"name":"JOE"}
                ]}
            */

            if (result != null)
            {
                result = "{\"highscoreEntryList\":" + result + "}";

                //Debug.Log(result);

                Highscores highscores = JsonUtility.FromJson<Highscores>(result);


                // Save updated Highscores
                string json = JsonUtility.ToJson(highscores);
                PlayerPrefs.SetString("highscoreTable", json);
                PlayerPrefs.Save();

            }



        }
    }

    IEnumerator InsertScore(string name, int score)
    {

        string url = domain + "scores.php";

        WWWForm formInsert = new WWWForm();
        formInsert.AddField("action", "write");
        formInsert.AddField("name", name);
        formInsert.AddField("score", score.ToString());

        using (UnityWebRequest www = UnityWebRequest.Post(url, formInsert))
        {

            yield return www.SendWebRequest();

            var result = www.downloadHandler.text;

            // check for errors
            if (www.error == null)
            {
                Debug.Log("WWW Ok!: " + result);
            }
            else if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log("Error #" + result);
            }
            else
            {
                Debug.Log("Unknown Error #" + result);
            }

        }
    }
}
