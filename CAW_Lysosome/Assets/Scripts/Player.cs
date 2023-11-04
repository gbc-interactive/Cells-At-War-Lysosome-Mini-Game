using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEditor.Search;
using UnityEngine;

public class Player : MonoBehaviour
{
    bool isAtPressStation;
    GameObject currentBond;
    int clicks;
    float time;

    int multiplierForTimeDecrease = 0;

    // Start is called before the first frame update
    void Start()
    {
        isAtPressStation= false;
    }

    // Update is called once per frame
    public void Update()
    {
        if (!isAtPressStation)
        {
            clicks= 0;
            MoveRight(3);
        }
        else if (isAtPressStation) 
        {
            time++;
            MoveRight(0.005f);

            if (time > 1000 / multiplierForTimeDecrease)
            {
                Debug.Log("Dead");
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                clicks++;
                
                if (clicks >= 10) 
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
            multiplierForTimeDecrease++;
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


