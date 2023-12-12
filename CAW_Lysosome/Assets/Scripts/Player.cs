using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
//using UnityEngine.UIElements;
using TMPro;
using Unity.VisualScripting;

public class Player : MonoBehaviour
{
    Animator animator;

    [SerializeField] Canvas canvas;
    [SerializeField] TextMeshProUGUI currentKeyText;

    [SerializeField] UnityEngine.UI.Image clicksBlock;
    [SerializeField] UnityEngine.UI.Image timeLeftBlock;
    [SerializeField] UnityEngine.UI.Image TimeBeforeStunBlock;

    bool isAtPressStation;
    GameObject currentBond;

    [SerializeField] GameObject button1;
    [SerializeField] GameObject button2;
    [SerializeField] GameObject button3;
    [SerializeField] GameObject button4;

    Color r = Color.red;
    Color g = Color.green;
    Color b = Color.blue;
    Color c = Color.magenta;

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
        animator= GetComponent<Animator>();

        canClick = true;
        callFuncOnce = false;
        isAtPressStation= false;
    }

    // Update is called once per frame
    public void Update()
    {
        BondStationCheck();
        transform.position = new Vector3(transform.position.x, BondManager.GetRandomY() - 1.5f, 0);

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

            button1.GetComponent<Image>().color = r;
            button2.GetComponent<Image>().color = g;
            button3.GetComponent<Image>().color = b;
            button4.GetComponent<Image>().color = c;
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

                switch(currentKey)
                {
                    case KeyCode.UpArrow:
                        currentKeyText.text = "U".ToString();
                        button1.GetComponent<Image>().color = Color.black;
                        break; 
                    case KeyCode.DownArrow:
                        currentKeyText.text = "D".ToString();
                        button2.GetComponent<Image>().color = Color.black;
                        break;
                    case KeyCode.LeftArrow:
                        currentKeyText.text = "L".ToString();
                        button3.GetComponent<Image>().color = Color.black;
                        break;
                    case KeyCode.RightArrow:
                        currentKeyText.text = "R".ToString();
                        button4.GetComponent<Image>().color = Color.black;
                        break;
                }

                callFuncOnce = true;
            }

            if (Input.GetKeyDown(currentKey) && canClick)
            {
                clicks++;

                if (clicks >= clicksHigherThan)
                {
                    StartCoroutine(FinalizeDestructionOfBond());
                }
            }
        }
    }

    IEnumerator FinalizeDestructionOfBond()
    {
        StartCoroutine(Animation());
        yield return new WaitForSeconds(0);

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

    IEnumerator Animation()
    {
        animator.SetBool("bond", true);
        yield return new WaitForSeconds(0.8f);
        animator.SetBool("bond", false);
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
                currentKey = KeyCode.UpArrow;
                break;
            case 1:
                currentKey = KeyCode.LeftArrow;
                break;
            case 2:
                currentKey = KeyCode.DownArrow;
                break;
            case 3:
                currentKey = KeyCode.RightArrow;
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

    public void Button1Clicked()
    {
        if (isAtPressStation && button1.GetComponent<Image>().color == Color.black && canClick)
        {
            clicks++;

            if (clicks >= clicksHigherThan)
            {
                StartCoroutine(FinalizeDestructionOfBond());
            }
        }
    }
    public void Button2Clicked()
    {
        if (isAtPressStation && button2.GetComponent<Image>().color == Color.black && canClick)
        {
            clicks++;

            if (clicks >= clicksHigherThan)
            {
                StartCoroutine(FinalizeDestructionOfBond());
            }
        }
    }
    public void Button3Clicked()
    {
        if (isAtPressStation && button3.GetComponent<Image>().color == Color.black && canClick)
        {
            clicks++;

            if (clicks >= clicksHigherThan)
            {
                StartCoroutine(FinalizeDestructionOfBond());
            }
        }
    }
    public void Button4Clicked()
    {
        if (isAtPressStation && button4.GetComponent<Image>().color == Color.black && canClick)
        {
            clicks++;

            if (clicks >= clicksHigherThan)
            {
                StartCoroutine(FinalizeDestructionOfBond());
            }
        }
    }
}


