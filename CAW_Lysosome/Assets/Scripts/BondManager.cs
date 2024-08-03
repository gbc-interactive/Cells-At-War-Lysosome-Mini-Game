using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class BondManager : MonoBehaviour
{
    [SerializeField] private AAChain AAChain;
    [SerializeField] GameObject bondPref;
    [SerializeField] GameObject endStream;
    int minBondSpawnAmt;
    int maxBondSpawnAmt;

    float distanceBetweenBonds = 5.5f;

    public static int currentStreamLength;
    static bool allBondsCompleted;   

    static float randomizedNewLocationY;

    [SerializeField] Vector3 playerPos;
    static bool gameStart;

    private GameObject bond;


    // Start is called before the first frame update
    void Start()
    {
        AAChain = FindFirstObjectByType<AAChain>();
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

        Physics2D.gravity = new Vector2(0, 0);

        if (gameStart)
        {
            minBondSpawnAmt = 3;
            maxBondSpawnAmt = 6;

            SetupStream();
        }
    }

    public static float GetRandomY()
    {
        return randomizedNewLocationY;
    }

    public int GetRandomSpawnAmt()
    {
        int corountineRepeats = Random.Range(minBondSpawnAmt, maxBondSpawnAmt);
        return corountineRepeats;
    }

    // currently spawns a chain on the right side of screen, randomized y value
    public void SetupStream()
    {
        allBondsCompleted= false;
        
        randomizedNewLocationY = Random.Range(-2, 3);

        currentStreamLength = GetRandomSpawnAmt();
        FindObjectOfType<Player>().SetCoroutineRuns(currentStreamLength-1, currentStreamLength-1);

        float x = playerPos.x;
        //CreateStream(currentStreamLength);
    }

    public void CreateStream(int runs)
    {
        float x = playerPos.x; 

        for (int i = 1; i < runs+1; i++)
        {
            bond = Instantiate(bondPref) as GameObject;
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
                    // for the last segment in the protein, remove the bond and disable the bond collider
                    bond.gameObject.tag = "last";
                    bond.GetComponent<BoxCollider2D>().enabled = false;
                    Destroy(bond.transform.GetChild(0).gameObject);
                }
            }
        }
    }

    public IEnumerator WaitForSecond(GameObject obj)
    {
        if (obj != null)
        {
            CreateExplosion(obj);
            yield return new WaitForSeconds(2.0f);
            Destroy(obj);
        }
    }

    // destroys bond and then destroys protein segment after 2 seconds of explosion time
    public IEnumerator SendBondToLeft(GameObject obj)
    {
        foreach (Transform child in obj.transform)
        {
            if (child.tag == "Bond")
            {
                // remove destroyed objects from queue and set new heads
                AAChain.stationQueue.Remove(obj);
                AAChain.aminoAcidQueue.Remove(obj);

                obj.GetComponent<BoxCollider2D>().enabled = false;
                yield return new WaitForSeconds(0.1f);
                Destroy(child.gameObject);
                CreateExplosion(obj);
                yield return new WaitForSeconds(2.0f);
                Destroy(obj);
            }
        }
    }

    // adds random explosion force originating from players position to the destroyed segment
    public void CreateExplosion(GameObject obj)
    {
        int randExplosion = Random.Range(1, 4);

        switch (randExplosion)
        {
            case 1:
                obj.GetComponent<Rigidbody2D>().AddForceAtPosition(Vector2.down * UnityEngine.Random.Range(10, 100), playerPos);
                break;
            case 2:
                obj.GetComponent<Rigidbody2D>().AddForceAtPosition(Vector2.up * UnityEngine.Random.Range(10, 100), playerPos);
                break;
            case 3:
                obj.GetComponent<Rigidbody2D>().AddForceAtPosition(Vector2.left * UnityEngine.Random.Range(10, 100), playerPos);
                break;
            case 4:
                obj.GetComponent<Rigidbody2D>().AddForceAtPosition(Vector2.right * UnityEngine.Random.Range(10, 100), playerPos);
                break;
            default:
                break;
        }
    }

    public static void SetBondsCompleted()
    {
        allBondsCompleted = true;
    }
}
