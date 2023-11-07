using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TimerScript : MonoBehaviour
{
    [SerializeField] float TimeLeft;
    [SerializeField] bool TimerOn = false;

    public Text timerText;
    static bool gameStart;

    // Start is called before the first frame update
    void Start()
    {
        if (gameStart)
        {
            TimerOn = true;
        }
    }

    static public void SetGameStart(bool b)
    {
        gameStart = b;
    }

    // Update is called once per frame
    void Update()
    {
        Timer();
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
            SceneManager.LoadScene("WinScene");
        }
    }

    void UpdateTimer(float currentTime)
    {
        currentTime += 1;

        float minutes = Mathf.FloorToInt(currentTime / 60);
        float seconds = Mathf.FloorToInt(currentTime % 60);

        timerText.text = string.Format("{0:00} : {1:00}", minutes, seconds);
    }
}
