using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(WaitMilisecond());
    }

    IEnumerator WaitMilisecond()
    {
        yield return new WaitForSeconds(0.1f);

        GameObject b = GameObject.FindWithTag("BManager");
        b.GetComponent<BondManager>().SetGameStart(true);

        GameObject t = GameObject.FindWithTag("TManager");
        t.GetComponent<TimerScript>().SetGameStart(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
