using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

 public class SendScore : MonoBehaviour {


    public Text playerDisplay;


    [System.Serializable]
    public class LeaderBoard
    {
        public string id;
        public string name;
        public string score;
    }

 
     // Use this for initialization
     void Start () {
 
         //StartCoroutine(InsertScore("Test", 100));

         StartCoroutine(ReadScore());
     }

     IEnumerator ReadScore()
     {
     string url = "http://localhost/scores.php";
 
         WWWForm formRead = new WWWForm();
         formRead.AddField("action", "read");

         playerDisplay.text = "Debug";

          using (UnityWebRequest www = UnityWebRequest.Post(url, formRead))
            {

                yield return www.SendWebRequest();

                var result = www.downloadHandler.text;

                if (result != null)
                {
                    Debug.Log("Debug" + result);

                }
    
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

                playerDisplay.text = "Debug" + result;

                foreach (float value in result)
                {
                    playerDisplay.text = "Debug" + value;
                }

                LeaderBoard[] leaderboard;
                leaderboard = JsonHelper.FromJson<LeaderBoard>(result);

                // for every employee in the array print every employee's data.
                foreach (LeaderBoard player in leaderboard)
                {
                    DisplayLeaderboards(player);
                }



            }

     }
 
     
     IEnumerator InsertScore(string name, int score)
     {

        string url = "http://localhost/scores.php";
 
         WWWForm formInsert = new WWWForm();
         formInsert.AddField("action", "write");
         formInsert.AddField("name", name);
         formInsert.AddField("score", score.ToString());


          using (UnityWebRequest www = UnityWebRequest.Post(url, formInsert))
            {


                yield return www.SendWebRequest();

                var result = www.downloadHandler.text;

                if (result != null)
                {
                    Debug.Log("Debug" + result);

                }
    
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

                playerDisplay.text = "Debug" + result;
            }
     }  


    // Display the leaderboards 1 by 1.
    void DisplayLeaderboards(LeaderBoard player)
    {
        Debug.Log("ID: " + player.id);
        Debug.Log("Name: " + player.name);
        Debug.Log("Score: " + player.score);
    }


    public static class JsonHelper
    {
        public static T[] FromJson<T>(string json)
        {
            Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
            return wrapper.Items;
        }

        public static string ToJson<T>(T[] array)
        {
            Wrapper<T> wrapper = new Wrapper<T>();
            wrapper.Items = array;
            return JsonUtility.ToJson(wrapper);
        }

        public static string ToJson<T>(T[] array, bool prettyPrint)
        {
            Wrapper<T> wrapper = new Wrapper<T>();
            wrapper.Items = array;
            return JsonUtility.ToJson(wrapper, prettyPrint);
        }

        [System.Serializable]
        private class Wrapper<T>
        {
            public T[] Items;
        }
    }
 

  }

