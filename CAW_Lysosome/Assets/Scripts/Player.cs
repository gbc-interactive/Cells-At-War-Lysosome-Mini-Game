using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEditor.Search;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField]Canvas canvas;

    [SerializeField] Image clicksBlock;
    [SerializeField] Image timeLeftBlock;

    bool isAtPressStation;
    GameObject currentBond;
    
    int clicks;
    int clicksHigherThan = 10;
    int timeForDecreasing = 2000;
    private const int MoveSpeed = 3;

    private const float addedToMultiplierTimeDecrease = 0.33f;
    private const float StuckSpeed = 0.005f;
    
    float time;
    float multiplierForTimeDecrease = 0;

    // Start is called before the first frame update
    void Start()
    {
        isAtPressStation= false;
    }

    // Update is called once per frame
    public void Update()
    {
        BondStationCheck();

        clicksBlock.fillAmount = clicks / 10f;
        timeLeftBlock.fillAmount = time / 60f;
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
            if (time > test)
            {
                SceneManager.LoadScene("LoseScene");
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                clicks++;

                if (clicks >= clicksHigherThan)
                {
                    Destroy(currentBond);
                    time = 0;
                    currentBond = null;
                }
            }
        }
    }

    private void MoveRight(float speed)
    {
        float x = transform.position.x;
        transform.position = new Vector3(x += speed * Time.deltaTime, -2, 0);
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


