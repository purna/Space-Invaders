using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public static int score;
    public Text scoreTxt;

    public static void UpdateScore(int value = 1)
    {
        score+= value;
    }

    private void LateUpdate()
    {
        scoreTxt.text = "Score: " + score;
    }
}
