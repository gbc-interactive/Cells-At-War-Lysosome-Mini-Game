using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BondManager : MonoBehaviour
{
    [SerializeField] GameObject bondPref;

    [SerializeField] Vector3 playerPos;
    static bool gameStart;

    // Start is called before the first frame update
    void Start()
    {
        if (gameStart)
        {
            StartCoroutine(SpawnBondBridgeAA());
        }
    }

    static public void SetGameStart(bool b)
    {
        gameStart = b;
    }


    IEnumerator SpawnBondBridgeAA()
    {
        GameObject bond = Instantiate(bondPref) as GameObject;
        //GameObject bondBr= Instantiate(bondBridge) as GameObject;
        //GameObject AminoAcid = Instantiate(aa[GetRandAA()]) as GameObject;

        bond.transform.position = new Vector3(playerPos.x + 21, -2f, 0f);

        //bondBr.transform.SetParent(bond.transform, false);
        //bondBr.transform.position = new Vector3(playerPos.x + 24, -1.5f, 0f);

        //AminoAcid.transform.SetParent(bondBr.transform, false);
        //AminoAcid.transform.position = new Vector3(playerPos.x + 21, -2f, 0f);
        yield return new WaitForSeconds(2);

        StartCoroutine(SpawnBondBridgeAA());
    }

    // Update is called once per frame
    void Update()
    {
        playerPos = FindAnyObjectByType<Player>().transform.position;
    }
}
