using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{
    public Timer timer;
    public GameObject endMenuUI;
    public TextMeshProUGUI reason;
    private void Update()
    {
        if (timer != null)
        {
            if (timer.timeLeft == 0)
            {
                Time.timeScale = 0f;
                endMenuUI.SetActive(true);
                reason.text = "Time's up!";
            }
        }
    }
}
