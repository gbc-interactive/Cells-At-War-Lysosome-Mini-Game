using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    [SerializeField] Sprite greenPressed;
    [SerializeField] Sprite greenUnPressed;
    [SerializeField] Sprite redPressed;
    [SerializeField] Sprite redUnPressed;
    [SerializeField] Sprite bluePressed;
    [SerializeField] Sprite blueUnPressed;
    [SerializeField] Sprite pinkPressed;
    [SerializeField] Sprite pinkUnPressed;

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

    KeyCode currentKey;
    bool callFuncOnce;

    bool blueShineOn;
    bool GreenShineOn;
    bool RedShineOn;
    bool PinkShineOn;

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

    private Vector3 mouseWorldPosition;

    // Start is called before the first frame update
    void Start()
    {
        blueShineOn = false; GreenShineOn = false;
        RedShineOn = false; PinkShineOn = false;

        animator = GetComponent<Animator>();
        animator.Play("idle");

        canClick = true;
        callFuncOnce = false;
        isAtPressStation= false;

        mouseWorldPosition.z = 0.0f;
    }

    // Update is called once per frame
    public void Update()
    {
        if (blueShineOn)
        {
            GameObject b = button1.gameObject.transform.GetChild(0).gameObject;
            b.SetActive(true);
        }
        if (!blueShineOn)
        {
            GameObject b = button1.gameObject.transform.GetChild(0).gameObject;
            b.SetActive(false);
        }

        if (GreenShineOn)
        {
            GameObject b = button2.gameObject.transform.GetChild(0).gameObject;
            b.SetActive(true);
        }
        if (!GreenShineOn)
        {
            GameObject b = button2.gameObject.transform.GetChild(0).gameObject;
            b.SetActive(false);
        }

        if (RedShineOn)
        {
            GameObject b = button3.gameObject.transform.GetChild(0).gameObject;
            b.SetActive(true);
        }
        if (!RedShineOn)
        {
            GameObject b = button3.gameObject.transform.GetChild(0).gameObject;
            b.SetActive(false);
        }

        if (PinkShineOn)
        {
            GameObject b = button4.gameObject.transform.GetChild(0).gameObject;
            b.SetActive(true);
        }
        if (!PinkShineOn)
        {
            GameObject b = button4.gameObject.transform.GetChild(0).gameObject;
            b.SetActive(false);
        }

        // player position set to the mouse position 
        mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = mouseWorldPosition;

        //BondStationCheck();
        //transform.position = new Vector3(transform.position.x, BondManager.GetRandomY() - 1.5f, 0);

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

            button1.GetComponent<Image>().sprite = blueUnPressed;
            button2.GetComponent<Image>().sprite = greenUnPressed;
            button3.GetComponent<Image>().sprite = redUnPressed;
            button4.GetComponent<Image>().sprite = pinkUnPressed;

            button1.GetComponent<Image>().color = Color.white;
            button2.GetComponent<Image>().color = Color.white;
            button3.GetComponent<Image>().color = Color.white;
            button4.GetComponent<Image>().color = Color.white;

            blueShineOn = false;
            GreenShineOn = false;
            RedShineOn = false;
            PinkShineOn = false;
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
                        blueShineOn = true;
                        break; 
                    case KeyCode.DownArrow:
                        currentKeyText.text = "D".ToString();
                        GreenShineOn = true;

                        break;
                    case KeyCode.LeftArrow:
                        currentKeyText.text = "L".ToString();
                        RedShineOn= true;

                        break;
                    case KeyCode.RightArrow:
                        currentKeyText.text = "R".ToString();
                        PinkShineOn=true;

                        break;
                }

                callFuncOnce = true;
            }
        }
    }

    public void FinalizeDestructionOfBond()
    {
        animator.Play("expl");
        StartCoroutine(WaitForSec());

        StartCoroutine(FindObjectOfType<BondManager>().SendBondToLeft(currentBond));

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

    IEnumerator SwitchBackToPng(GameObject g)
    {
        yield return new WaitForSeconds(0.1f);

        var button = g.GetComponent<Button>().GetComponent<Image>().sprite.name;

        switch(button)
        {
            case "Button_Controller_BlueLysosomePressed":
                g.GetComponent<Button>().GetComponent<Image>().sprite = blueUnPressed;
                break;
            case "Button_Controller_GreenLysosomePressed":
                g.GetComponent<Button>().GetComponent<Image>().sprite = greenUnPressed;
                break;
            case "Button_Controller_RedLysosomePressed":
                g.GetComponent<Button>().GetComponent<Image>().sprite = redUnPressed;
                break;
            case "Button_Controller_PinkLysosomePressed":
                g.GetComponent<Button>().GetComponent<Image>().sprite = pinkUnPressed;
                break;

        }
    }

    //IEnumerator SwitchBackToWhite(GameObject b)
    //{
    //    yield return new WaitForSeconds(0.1f);
    //    b.GetComponent<Button>().GetComponent<Image>().color = Color.white;

    //    yield return new WaitForSeconds(0.1f);
    //    b.GetComponent<Button>().GetComponent<Image>().color = Color.black;

    //    yield return new WaitForSeconds(0.1f);
    //    b.GetComponent<Button>().GetComponent<Image>().color = Color.white;
    //}

    IEnumerator WaitForSec()
    {
        yield return new WaitForSeconds(0.2f);

        animator.Play("idle");
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
        if (isAtPressStation && canClick)
        {
            clicks++;
            button1.GetComponent<Image>().sprite = bluePressed;
            blueShineOn = false;

            switch (clicks)
            {
                case 1:
                    animator.Play("click1");
                    break;
                case 2:
                    animator.Play("click2");
                    break;
                case 3:
                    animator.Play("click3");
                    break;
                case 4:
                    animator.Play("click4");
                    break;
                case 5:
                    animator.Play("click5");
                    break;
                case 6:
                    animator.Play("click6");
                    break;
                case 7:
                    animator.Play("click7");
                    break;
                case 8:
                    animator.Play("click8");
                    break;
                case 9:
                    animator.Play("click9");
                    break;
                case 10:
                    animator.Play("click10");
                    break;
                default: 
                    
                    break;
            }
            StartCoroutine(SwitchBackToPng(button1));

            if (clicks >= clicksHigherThan)
            {
                FinalizeDestructionOfBond();
            }
        }
    }
    public void Button2Clicked()
    {
        if (isAtPressStation && canClick)
        {
            clicks++;
            button2.GetComponent<Image>().sprite = greenPressed;
            GreenShineOn = false;

            switch (clicks)
            {
                case 1:
                    animator.Play("click1");
                    break;
                case 2:
                    animator.Play("click2");
                    break;
                case 3:
                    animator.Play("click3");
                    break;
                case 4:
                    animator.Play("click4");
                    break;
                case 5:
                    animator.Play("click5");
                    break;
                case 6:
                    animator.Play("click6");
                    break;
                case 7:
                    animator.Play("click7");
                    break;
                case 8:
                    animator.Play("click8");
                    break;
                case 9:
                    animator.Play("click9");
                    break;
                case 10:
                    animator.Play("click10");
                    break;
            }
            StartCoroutine(SwitchBackToPng(button2));

            if (clicks >= clicksHigherThan)
            {
                FinalizeDestructionOfBond();
            }
        }
    }
    public void Button3Clicked()
    {
        if (isAtPressStation && canClick)
        {
            clicks++;
            button3.GetComponent<Image>().sprite = redPressed;
            RedShineOn = false;

            switch (clicks)
            {
                case 1:
                    animator.Play("click1");
                    break;
                case 2:
                    animator.Play("click2");
                    break;
                case 3:
                    animator.Play("click3");
                    break;
                case 4:
                    animator.Play("click4");
                    break;
                case 5:
                    animator.Play("click5");
                    break;
                case 6:
                    animator.Play("click6");
                    break;
                case 7:
                    animator.Play("click7");
                    break;
                case 8:
                    animator.Play("click8");
                    break;
                case 9:
                    animator.Play("click9");
                    break;
                case 10:
                    animator.Play("click10");
                    break;
            }
            StartCoroutine(SwitchBackToPng(button3));

            if (clicks >= clicksHigherThan)
            {
                FinalizeDestructionOfBond();
            }
        }
    }
    public void Button4Clicked()
    {
        if (isAtPressStation && canClick)
        {
            clicks++;
            button4.GetComponent<Image>().sprite = pinkPressed;
            PinkShineOn = false;

            switch (clicks)
            {
                case 1:
                    animator.Play("click1");
                    break;
                case 2:
                    animator.Play("click2");
                    break;
                case 3:
                    animator.Play("click3");
                    break;
                case 4:
                    animator.Play("click4");
                    break;
                case 5:
                    animator.Play("click5");
                    break;
                case 6:
                    animator.Play("click6");
                    break;
                case 7:
                    animator.Play("click7");
                    break;
                case 8:
                    animator.Play("click8");
                    break;
                case 9:
                    animator.Play("click9");
                    break;
                case 10:
                    animator.Play("click10");
                    break;
            }
            StartCoroutine(SwitchBackToPng(button4));

            if (clicks >= clicksHigherThan)
            {
                FinalizeDestructionOfBond();
            }
        }
    }
}


