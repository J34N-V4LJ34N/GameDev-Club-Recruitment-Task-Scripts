using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Highscore : MonoBehaviour
{
    public TextMeshProUGUI highscoreTxt;
    private void Start()
    {
        highscoreTxt.text= "Highscore: "+PlayerPrefs.GetInt("Highscore",0).ToString();
    }
    public void setHighScore(int number)
    {
        PlayerPrefs.SetInt("Highscore", number);
    }
    public void Reset()
    {
        PlayerPrefs.DeleteAll();
        highscoreTxt.text = "Highscore: 0";
    }
}
