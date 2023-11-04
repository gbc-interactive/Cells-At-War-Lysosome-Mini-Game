using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEditor.Search;
using UnityEngine;

public class Player : MonoBehaviour
{
    bool isAtPressStation;
    // Start is called before the first frame update
    void Start()
    {
        isAtPressStation= false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isAtPressStation)
        {
            MoveRight(3);
        }
        else if (isAtPressStation) 
        {
            MoveRight(0.005f);
            Debug.Log("At station");

        }
    }

    private void MoveRight(float speed)
    {
        float x = transform.position.x;
        transform.position = new Vector3(x += speed * Time.deltaTime, -2, 0);
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Station")
        {
            isAtPressStation= true;
            if (Input.GetKey(KeyCode.Space))
            {
                Destroy(collision.gameObject);
            }
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


