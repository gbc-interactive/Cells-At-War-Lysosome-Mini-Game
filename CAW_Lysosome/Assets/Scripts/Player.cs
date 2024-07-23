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
    Animator animator;

    [SerializeField] Canvas canvas;
    [SerializeField] TextMeshProUGUI currentKeyText;
    [SerializeField] UnityEngine.UI.Image clicksBlock;
    [SerializeField] UnityEngine.UI.Image timeLeftBlock;
    [SerializeField] UnityEngine.UI.Image TimeBeforeStunBlock;

    [SerializeField] int clicksHigherThan = 10;
    [SerializeField] int timeForDecreasing = 2000;
    [SerializeField] private float addedToMultiplierTimeDecrease = 0.1f;

    bool isAtPressStation;
    GameObject currentBond;
    KeyCode currentKey;
    bool callFuncOnce;
    float timeBeforeStun = 1500f;
    float stunTime = 2;
    bool canClick;
    int clicks;
    public int coroutineRuns;

    float time;
    float time_ForStun;
    float multiplierForTimeDecrease = 0;
    float timeIncrease;

    private Vector3 mouseWorldPosition;

    private Material playerMaterial;

    // Start is called before the first frame update
    void Start()
    {
        playerMaterial = gameObject.GetComponent<SpriteRenderer>().material;

        animator = GetComponent<Animator>();
        animator.Play("idle");

        canClick = true;
        callFuncOnce = false;
        isAtPressStation = false;

        mouseWorldPosition.z = 0.0f;
    }

    // Update is called once per frame
    public void Update()
    {
        // left click mouse to destroy bond
        if (Input.GetMouseButtonDown(0) && isAtPressStation)
        {
            clicks++;
            if (clicks >= clicksHigherThan)
            {
                FinalizeDestructionOfBond();
            }
        }

        // player position set to the mouse position 
        mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = mouseWorldPosition;

        BondStationCheck();

        clicksBlock.fillAmount = clicks / 10f;

        StartCoroutine(FadePlayer());
    }

    private void BondStationCheck()
    {
        if (!isAtPressStation)
        {
            currentKeyText.text = "";
            clicks = 0;
        }
        else if (isAtPressStation)
        {
            // TODO: timer/stun bar removed currently 

            time++;
            time_ForStun++;

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
        var randKey = Random.Range(0, 4);

        switch (randKey)
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

    public void SetCoroutineRuns(int runs, float increase)
    {
        coroutineRuns = runs;
        timeIncrease = increase;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Station")
        {
            isAtPressStation = true;
            currentBond = collision.gameObject;
            multiplierForTimeDecrease += addedToMultiplierTimeDecrease;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Station")
        {
            // reset red bar to 0 if player stops colliding with that bond
            time = 0.0f;
            isAtPressStation = false;
        }
    }

    // fades player opacity from 1.0 to 0.0 over 2mins(total game time)
    IEnumerator FadePlayer()
    {
        float fadeRatePerSecond = 0.000012f;
        Color playerColour = playerMaterial.color;
        playerColour.a -= fadeRatePerSecond; 
        playerMaterial.color = playerColour;
        yield return new WaitForSeconds(1.0f);

        Debug.Log(playerMaterial.color.a);

        // if players opacity is less or equal to 0.05 aka 5% (adjust accordingly) then lose game
        if (playerColour.a <= 0.05f)
        {
            SceneManager.LoadScene("LoseScene");
        }
    }
}


