using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class BondManager : MonoBehaviour
{
    [SerializeField] GameObject bondPref;
    [SerializeField] int minBondSpawnAmt = 3;
    [SerializeField] int maxBondSpawnAmt = 6;

    int distanceBetweenBonds = 6;

    static public int currentStreamLength;
    static bool allBondsCompleted;   

    static float randomizedNewLocationY;

    [SerializeField] Vector3 playerPos;
    static bool gameStart;
 
    // Start is called before the first frame update
    void Start()
    {
        if (gameStart)
        {
            SetupStream();
        }
    }

    void Update()
    {
        playerPos = FindAnyObjectByType<Player>().transform.position;

        if (allBondsCompleted == true)
        {
            SetupStream();
        }
    }

    static public void SetGameStart(bool b)
    {
        gameStart = b;
    }

    static public float GetRandomY()
    {
        return randomizedNewLocationY;
    }

    public int GetRandomSpawnAmt()
    {
        int corountineRepeats = 
            UnityEngine.Random.Range(minBondSpawnAmt, maxBondSpawnAmt);
        return corountineRepeats;
    }

    

    public void SetupStream()
    {
        allBondsCompleted= false;
        
        randomizedNewLocationY = UnityEngine.Random.Range(-6, 6);

        currentStreamLength = GetRandomSpawnAmt();
        FindObjectOfType<Player>().SetCoroutineRuns(currentStreamLength, currentStreamLength);

        float x = playerPos.x;
        CreateStream(currentStreamLength);
    }

    public void CreateStream(int runs)
    {
        float x = playerPos.x; 

        for (int i = 1; i < runs+1; i++)
        {
            GameObject bond = Instantiate(bondPref) as GameObject;
            if (i == 1)
            {
                bond.transform.position = new Vector3(x + distanceBetweenBonds, randomizedNewLocationY, 0);
            }
            else
            {
                bond.transform.position = new Vector3(x + (distanceBetweenBonds * i) , randomizedNewLocationY, 0);
            }
        }
    }

    static public void SetBondsCompleted()
    {
        allBondsCompleted = true;
    }

    // Update is called once per frame

}
