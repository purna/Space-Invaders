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

    string jsonDir;
    string fileSuffix = "leaderboard.json";

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

        string result = System.IO.File.ReadAllText(jsonDir);
        return JsonUtility.FromJson<Highscores>(result);
    }

    private void WriteHighscores()
    {
        string json = JsonUtility.ToJson(highscores);
        System.IO.File.WriteAllText(jsonDir, json);
    }

    public void UpdateLeaderBoard()
    {
        for(int i = 0; i < 10; i++)
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
    }

    public void SaveBoard()
    {
        WriteHighscores();

        //SQL stuff here
    }

    void FileSetup()
    {
        jsonDir = Application.persistentDataPath + fileSuffix;

        if (!System.IO.File.Exists(jsonDir))
            System.IO.File.Create(jsonDir);

        if(System.IO.File.ReadAllLines(jsonDir).Length == 0)
        {
            ResetLeaderboard();
            Debug.Log("No values");
        }
    }
}
