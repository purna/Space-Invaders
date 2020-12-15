using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeaderboardEntry : MonoBehaviour
{
    public Text position, initials, score;

    public void SetEntry(int position, string initials, int score)
    {
        this.position.text = position.ToString();
        this.initials.text = initials;
        this.score.text = score.ToString();
    }

    public void SetEntry(HighscoreTable.HighscoreEntry score)
    {
        position.text = score.id.ToString();
        initials.text = score.name;
        this.score.text = score.score.ToString();
    }
}
