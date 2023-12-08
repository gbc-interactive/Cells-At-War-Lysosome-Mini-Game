using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using TMPro;

public class Player : MonoBehaviour
{
    [SerializeField] Canvas canvas;
    [SerializeField] TextMeshProUGUI currentKeyText;

    [SerializeField] UnityEngine.UI.Image clicksBlock;
    [SerializeField] UnityEngine.UI.Image timeLeftBlock;
    [SerializeField] UnityEngine.UI.Image TimeBeforeStunBlock;

    bool isAtPressStation;
    GameObject currentBond;

    KeyCode currentKey;
    bool callFuncOnce;

    float timeBeforeStun = 1500f;
    float stunTime = 2;
    bool canClick;

    int clicks;
    [SerializeField ]int clicksHigherThan = 10;
    [SerializeField] int timeForDecreasing = 2000;
    [SerializeField] private int MoveSpeed = 3;

    [SerializeField]private float addedToMultiplierTimeDecrease = 0.33f;
    private const float StuckSpeed = 0.005f;

    public int coroutineRuns;

    float time;
    float time_ForStun;
    float multiplierForTimeDecrease = 0;

    float timeIncrease;

    // Start is called before the first frame update
    void Start()
    {
        canClick = true;
        callFuncOnce = false;
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
            currentKeyText.text = "";
            clicks = 0;
            MoveRight(MoveSpeed);
        }
        else if (isAtPressStation)
        {
            time++;
            time_ForStun++;
            
            MoveRight(StuckSpeed);
            float test = timeForDecreasing / multiplierForTimeDecrease;
            
            timeLeftBlock.fillAmount = time / test;
            TimeBeforeStunBlock.fillAmount = time / timeBeforeStun;

            if (time > test)
            {
                SceneManager.LoadScene("LoseScene");
            }

            if (time_ForStun > timeBeforeStun)
            {
                StartCoroutine(CanClickFalse());
            }

            if (callFuncOnce == false)
            {
                currentKey = GetRandKey();
                currentKeyText.text = currentKey.ToString();
                callFuncOnce = true;
            }

            if (Input.GetKeyDown(currentKey) && canClick)
            {
                clicks++;

                if (clicks >= clicksHigherThan)
                {
                    FindObjectOfType<BondManager>().SendBondToLeft(currentBond);
                    
                    time = 0;
                    time_ForStun = 0;
                    
                    coroutineRuns--;
                    callFuncOnce = false;

                    if (coroutineRuns < 1)
                    {
                        GameObject go = GameObject.FindGameObjectWithTag("last");

                        StartCoroutine(FindObjectOfType<BondManager>().WaitForSecond(go));

                        BondManager.SetBondsCompleted();
                        //FindObjectOfType<TimerScript>().IncreaseTimer(timeIncrease);
                    }
                }
            }
        }
    }

    IEnumerator CanClickFalse()
    {
        canClick = false;
        FindObjectOfType<FollowPlayer>().start = true;
        yield return new WaitForSeconds(stunTime);
        canClick = true;

        time_ForStun = 0;
    }

    public KeyCode GetRandKey()
    {
        var randKey = (int)UnityEngine.Random.Range(0, 4);

        switch(randKey)
        {
            case 0:
                currentKey = KeyCode.W;
                break;
            case 1:
                currentKey = KeyCode.A;
                break;
            case 2:
                currentKey = KeyCode.S;
                break;
            case 3:
                currentKey = KeyCode.D;
                break;
        }

        return currentKey;
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


