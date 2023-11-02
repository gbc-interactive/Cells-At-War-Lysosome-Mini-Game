using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float playerSpeed = 3f;
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
            MoveRight();
        }
        else if (isAtPressStation) 
        {
            Debug.Log("At station");
            if (Input.GetKeyUp(KeyCode.Space))
            {
                isAtPressStation= true;
            }
        }
    }

    private void MoveRight()
    {
        float x = transform.position.x;
        transform.position = new Vector3(x += playerSpeed * Time.deltaTime, -2, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Station")
        {
            isAtPressStation = true;
        }
    }
}
