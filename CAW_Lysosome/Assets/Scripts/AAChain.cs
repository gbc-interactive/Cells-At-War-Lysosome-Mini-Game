using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;


// testing, maybe complete it and use in future
public class AAChain : MonoBehaviour
{
    [SerializeField] private int minChainSize = 3;
    [SerializeField] private int maxChainSize = 6;
    [SerializeField] private GameObject stationPrefab;
    [SerializeField] private GameObject aminoAcidPrefab;
    [SerializeField] private GameObject[] aminoAcidPrefabList;
    [SerializeField] private Queue<GameObject> stationQueue;
    [SerializeField] private Queue<GameObject> aminoAcidQueue;

    [SerializeField] private GameObject stationTail;
    [SerializeField] private GameObject aminoAcidTail;

    // Start is called before the first frame update
    void Start()
    {
        stationQueue = new Queue<GameObject>();
        aminoAcidQueue = new Queue<GameObject>();
        SpawnChain();
    }

    // Update is called once per frame
    void Update()
    {
        stationTail = stationQueue.Last();
        aminoAcidTail = aminoAcidQueue.Last();
    }

    private void RandomAminoAcid()
    {
        aminoAcidPrefab = aminoAcidPrefabList[Random.Range(0, aminoAcidPrefabList.Length)];
    }

    private void SpawnChain()
    {
        int chainSize = Random.Range(minChainSize, maxChainSize);

        for (int i = 0; i < chainSize; i++)
        {
            GameObject tempStation = Instantiate(stationPrefab);
            tempStation.transform.localScale *= 0.2f;
            tempStation.transform.position = new Vector3(tempStation.transform.position.x + i, 1.0f);
            Vector2 aaLocation = tempStation.transform.GetChild(1).gameObject.transform.position;
            aaLocation.y -= 0.25f;

            RandomAminoAcid();
            GameObject tempAminoAcid = Instantiate(aminoAcidPrefab);
            tempAminoAcid.transform.localScale *= 0.1f;
            tempAminoAcid.transform.position = aaLocation;

            stationQueue.Enqueue(tempStation);
            aminoAcidQueue.Enqueue(tempAminoAcid);
        }
    }
}
