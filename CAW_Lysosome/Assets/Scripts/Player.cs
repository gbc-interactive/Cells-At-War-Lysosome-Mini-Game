using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEditor.Search;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using TreeEditor;

public class Player : MonoBehaviour
{
    [SerializeField]Canvas canvas;

    [SerializeField] UnityEngine.UI.Image clicksBlock;
    [SerializeField] UnityEngine.UI.Image timeLeftBlock;

    bool isAtPressStation;
    GameObject currentBond;
    
    int clicks;
    int clicksHigherThan = 10;
    int timeForDecreasing = 2000;
    private const int MoveSpeed = 3;

    private const float addedToMultiplierTimeDecrease = 0.33f;
    private const float StuckSpeed = 0.005f;

    public int coroutineRuns;

    float time;
    float multiplierForTimeDecrease = 0;

    float timeIncrease;

    // Start is called before the first frame update
    void Start()
    {
        isAtPressStation= false;
    }

    // Update is called once per frame
    public void Update()
    {
        BondStationCheck();
        transform.position = new Vector3(transform.position.x, BondManager.GetRandomY(), 0);

        clicksBlock.fillAmount = clicks / 10f;
    }

    public float GetX()
    {
        return transform.position.x;
    }

    private void BondStationCheck()
    {
        if (!isAtPressStation)
        {
            clicks = 0;
            MoveRight(MoveSpeed);
        }
        else if (isAtPressStation)
        {
            time++;
            MoveRight(StuckSpeed);
            float test = timeForDecreasing / multiplierForTimeDecrease;
            //Debug.Log(time + "/" + test);
            timeLeftBlock.fillAmount = time / test;

            if (time > test)
            {
                SceneManager.LoadScene("LoseScene");
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                clicks++;

                if (clicks >= clicksHigherThan)
                {
                    FindObjectOfType<BondManager>().SendBondToLeft(currentBond);
                    time = 0;
                    coroutineRuns--;

                    if (coroutineRuns < 1)
                    {
                        GameObject go = GameObject.FindGameObjectWithTag("last");
                        StartCoroutine(FindObjectOfType<BondManager>().WaitForSecond(go));

                        BondManager.SetBondsCompleted();
                        FindObjectOfType<TimerScript>().IncreaseTimer(timeIncrease);
                    }
                }
            }
        }
    }

    private void MoveRight(float speed)
    {
        float x = transform.position.x;
        transform.position = new Vector3(x += speed * Time.deltaTime, -2, 0);
    }

    public void SetCoroutineRuns(int runs, float increase)
    {
        coroutineRuns= runs;
        timeIncrease = increase;
    }

    public void SetNewYValue(float Y)
    {
        transform.position = new Vector3(transform.position.x, Y, 0);    
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Station")
        {
            isAtPressStation = true;
            currentBond= collision.gameObject;
            multiplierForTimeDecrease+= addedToMultiplierTimeDecrease;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Station")
        {
            isAtPressStation = false;
        }
    }
}


