using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TimerScript : MonoBehaviour
{
    [SerializeField] private GameObject timerText;
    [SerializeField] float TimeLeft;
    [SerializeField] bool TimerOn = false;

    static bool gameStart;

    float minutes;
    float seconds;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SetGameStart(bool b)
    {
        gameStart = b;

        if (gameStart)
        {
            TimerOn = true;
        }
    }

    public void IncreaseTimer(float addedTime)
    {
        TimeLeft+= addedTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameStart)
        {
            Timer();
        }
    }

    private void Timer()
    {
        if (TimerOn && TimeLeft > 0)
        {      
             TimeLeft -= Time.deltaTime;
             UpdateTimer(TimeLeft);
        }

        if (TimeLeft < 0.1f)
        {
            SceneManager.LoadScene("LevelOverScene", LoadSceneMode.Additive);
            TimeLeft = 10000;
        }
    }

    void UpdateTimer(float currentTime)
    {
        currentTime += 1;

        minutes = Mathf.FloorToInt(currentTime / 60);
        seconds = Mathf.FloorToInt(currentTime % 60);

        timerText.GetComponent<TextMeshProUGUI>().SetText(string.Format("{0:00} : {1:00}", minutes, seconds));
        
    }
}
