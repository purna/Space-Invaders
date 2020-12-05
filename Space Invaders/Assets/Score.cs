using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public static int score;
    public static int hiScore;
    public Text scoreTxt;
    public Text hiscoreTxt;

    public static void UpdateScore(int value = 1)
    {
        score+= value;

        if (score > hiScore)
            hiScore = score;
    }

    private void LateUpdate()
    {
        scoreTxt.text = "SCORE: " + score;
        hiscoreTxt.text = "HI-SCORE: " + hiScore;
    }
}
