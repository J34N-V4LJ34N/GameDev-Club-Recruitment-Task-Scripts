using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CheckpointManager : MonoBehaviour
{ 
    public int score = 0;
    public TextMeshProUGUI scoreTxt;
    public TextMeshProUGUI highscoreTxt;
    Dictionary<string, List<string>> track = new Dictionary<string, List<string>>()
    {
        {"0",new List<string>(){"1","3","6"}},
        {"1",new List<string>(){"0","2"}},
        {"2",new List<string>(){"1","3"}},
        {"3",new List<string>(){"2","0","4"}},
        {"4",new List<string>(){"3","5","9"}},
        {"5",new List<string>(){"4","6","8"}},
        {"6",new List<string>(){"5","0","7"}},
        {"7",new List<string>(){"6","8"}},
        {"8",new List<string>(){"5","9","7"}},
        {"9",new List<string>(){"8","4"}},
    };
    public Timer timer;
    public float dist;
    public string lastCheckpoint="-1";
    public string currentCheckpoint = "0";
    public string nextCheckpoint;
    List<string> children = new List<string>();
    public GameObject endMenuUI;
    public TextMeshProUGUI reason;
    private void Start()
    {
        highscoreTxt.text = "Highscore: "+PlayerPrefs.GetInt("Highscore", 0).ToString();
        int childrenCount = transform.childCount;
        for (int i = 0; i < childrenCount; ++i)
        {
            children.Add(transform.GetChild(i).gameObject.name);
        }
        transform.GetChild(int.Parse(currentCheckpoint)).GetChild(0).gameObject.SetActive(true);
        setNextCheckpoint();
        score = 0;
        scoreTxt.text = "Score: 0";

        Vector3 dir = transform.GetChild(int.Parse(nextCheckpoint)).position-transform.GetChild(int.Parse(currentCheckpoint)).position ;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        transform.GetChild(int.Parse(currentCheckpoint)).GetChild(1).gameObject.SetActive(true);
        Vector3 rotation = Quaternion.Lerp(transform.GetChild(int.Parse(currentCheckpoint)).GetChild(1).rotation, lookRotation, Time.deltaTime * 100).eulerAngles;
        transform.GetChild(int.Parse(currentCheckpoint)).GetChild(1).rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }
    void Update()
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
        if (!transform.GetChild(int.Parse(currentCheckpoint)).GetChild(0).gameObject.activeSelf)
        {
            transform.GetChild(int.Parse(currentCheckpoint)).GetChild(1).gameObject.SetActive(false);
            ++score;
            scoreTxt.text = "Score: " + score.ToString();
            if (score>PlayerPrefs.GetInt("Highscore",0))
            {
                PlayerPrefs.SetInt("Highscore", score);
                highscoreTxt.text = "Highscore: "+score.ToString();
            }
            if (lastCheckpoint == "-1")
            {
                dist = 180f;
            }
            else
            {
                dist = Vector3.Distance(transform.GetChild(int.Parse(lastCheckpoint)).transform.position, transform.GetChild(int.Parse(currentCheckpoint)).transform.position);
            }
            lastCheckpoint = currentCheckpoint;
            currentCheckpoint = nextCheckpoint;
            setNextCheckpoint();
            float bump= score < 20 ? (float)(dist * (0.04 - 0.02 * score / 20)) : (float)(dist * (0.02));
            timer.timeLeft += bump;
        }
    }
    void setNextCheckpoint()
    {
        List<string> possibleNextCheckpoints = track[currentCheckpoint];
        possibleNextCheckpoints.Remove(lastCheckpoint);
        int index = Random.Range(0, possibleNextCheckpoints.Count);
        nextCheckpoint = possibleNextCheckpoints[index];
        transform.GetChild(int.Parse(nextCheckpoint)).GetChild(0).gameObject.SetActive(true);

        Vector3 dir = transform.GetChild(int.Parse(nextCheckpoint)).position - transform.GetChild(int.Parse(currentCheckpoint)).position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        transform.GetChild(int.Parse(currentCheckpoint)).GetChild(1).gameObject.SetActive(true);
        Vector3 rotation = Quaternion.Lerp(transform.GetChild(int.Parse(currentCheckpoint)).GetChild(1).rotation, lookRotation, 1).eulerAngles;
        transform.GetChild(int.Parse(currentCheckpoint)).GetChild(1).rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }
    //public void Reset()
    //{
    //    PlayerPrefs.DeleteAll();
    //    highscoreTxt.text = "0";
    //}
}
