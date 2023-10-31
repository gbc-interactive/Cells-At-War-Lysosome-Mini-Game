using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BondManager : MonoBehaviour
{
    [SerializeField] GameObject[] aa;
    [SerializeField] GameObject bond;
    [SerializeField] GameObject bondBridge;

    [SerializeField] Vector3 playerPos;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnBondBridgeAA());
    }

    public int GetRandAA()
    {
        return Random.Range(1, aa.Length);
    }

    IEnumerator SpawnBondBridgeAA()
    {
        Instantiate(bond, new Vector3(playerPos.x + 21, -1.8f, 0f), Quaternion.identity);
        Instantiate(bondBridge, new Vector3(playerPos.x + 24, -2f, 0f), Quaternion.identity);
        Instantiate(aa[GetRandAA()], new Vector3(playerPos.x + 21, -1.8f, 0f), Quaternion.identity);
        yield return new WaitForSeconds(2);
        StartCoroutine(SpawnBondBridgeAA());
    }

    // Update is called once per frame
    void Update()
    {
        playerPos = FindAnyObjectByType<Player>().transform.position;
    }
}
