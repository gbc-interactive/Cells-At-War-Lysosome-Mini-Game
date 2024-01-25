using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class BondManager : MonoBehaviour
{
    [SerializeField] GameObject bondPref;
    [SerializeField] GameObject endStream;
    int minBondSpawnAmt;
    int maxBondSpawnAmt;

    float distanceBetweenBonds = 5.5f;

    static public int currentStreamLength;
    static bool allBondsCompleted;   

    static float randomizedNewLocationY;

    [SerializeField] Vector3 playerPos;
    static bool gameStart;
 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        if (gameStart)
        {
            playerPos = FindAnyObjectByType<Player>().transform.position;

            if (allBondsCompleted == true)
            {
                SetupStream();
            }
        }
        else
            return;
    }

    public void SetGameStart(bool b)
    {
        gameStart = b;

        Physics2D.gravity = new Vector2(-9.81f, 0);

        if (gameStart)
        {
            minBondSpawnAmt = 3;
            maxBondSpawnAmt = 6;

            SetupStream();
        }
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
        FindObjectOfType<Player>().SetCoroutineRuns(currentStreamLength-1, currentStreamLength-1);

        float x = playerPos.x;
        CreateStream(currentStreamLength);
    }

    public void CreateStream(int runs)
    {
        float x = playerPos.x; 

        for (int i = 1; i < runs+1; i++)
        {
            GameObject bond = Instantiate(bondPref) as GameObject;
            bond.GetComponent<Rigidbody2D>().gravityScale = 0f;
            if (i == 1)
            {
                bond.transform.position = new Vector3(x + distanceBetweenBonds, randomizedNewLocationY, 0);
            }
            else
            {
                bond.transform.position = new Vector3(x + (distanceBetweenBonds * i) , randomizedNewLocationY, 0);

                if (i == runs)
                {
                    bond.gameObject.tag = "last";

                    for (int c = 0; c < bond.transform.childCount; c++) 
                    {
                        if (bond.transform.GetChild(c).gameObject.tag != "Connector")
                        {
                            Destroy(bond.transform.GetChild(c).gameObject); // For the final bond per stream
                        }
                        bond.GetComponent<BoxCollider2D>().enabled = false;
                    }
                }
            }
        }
    }

    public IEnumerator WaitForSecond(GameObject Obj)
    {
        yield return new WaitForSeconds(1);
        if (Obj != null)
        {
            CreateExplosion(Obj);
        }
        Obj.GetComponent<Rigidbody2D>().gravityScale = 1;
    }

    public IEnumerator SendBondToLeft(GameObject obj)
    {
        foreach (Transform child in obj.transform)
        {
            if (child.tag == "Bond")
            {
                obj.GetComponent<BoxCollider2D>().enabled = false;
                yield return new WaitForSeconds(0.1f);
                Destroy(child.gameObject);
                obj.GetComponent<Rigidbody2D>().gravityScale = 1;
                CreateExplosion(obj);
            }
        } 
    }

    public void CreateExplosion(GameObject obj)
    {
        int randExplosion = UnityEngine.Random.Range(1, 4);

        switch (randExplosion)
        {
            case 1:
                obj.GetComponent<Rigidbody2D>().AddForceAtPosition(Vector2.down * UnityEngine.Random.Range(1, 10), Vector2.right);
                break;
            case 2:
                obj.GetComponent<Rigidbody2D>().AddForceAtPosition(Vector2.up * UnityEngine.Random.Range(1, 10), Vector2.right);
                break;
            case 3:
                obj.GetComponent<Rigidbody2D>().AddForceAtPosition(Vector2.left * UnityEngine.Random.Range(1, 10), Vector2.right);
                break;
            case 4:
                obj.GetComponent<Rigidbody2D>().AddForceAtPosition(Vector2.right * UnityEngine.Random.Range(1, 10), Vector2.right);
                break;
            default:
                break;
        }
    }

    static public void SetBondsCompleted()
    {
        allBondsCompleted = true;
    }

    // Update is called once per frame

}
