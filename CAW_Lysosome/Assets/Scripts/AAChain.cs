using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.XR;
using Random = UnityEngine.Random;


// testing, maybe complete it and use in future
public class AAChain : MonoBehaviour
{
    [SerializeField] private int minChainSize = 3;
    [SerializeField] private int maxChainSize = 6;
    [SerializeField] private GameObject stationPrefab;
    [SerializeField] private GameObject aminoAcidPrefab;
    [SerializeField] private GameObject[] aminoAcidPrefabList;
    [SerializeField] public List<GameObject> stationQueue;
    [SerializeField] public List<GameObject> aminoAcidQueue;

    // track head and tail of the chain
    [SerializeField] public GameObject stationHead;
    [SerializeField] public GameObject stationTail;
    [SerializeField] public GameObject aminoAcidHead;
    [SerializeField] public GameObject aminoAcidTail;

    // Start is called before the first frame update
    void Start()
    {
        stationQueue = new List<GameObject>();
        aminoAcidQueue = new List<GameObject>();
        SpawnChain();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateQueue();
        MoveChain();
    }

    private GameObject RandomAminoAcid()
    {
        return aminoAcidPrefabList[Random.Range(0, aminoAcidPrefabList.Length)];
    }

    public void SpawnChain()
    {
        int chainSize = Random.Range(minChainSize, maxChainSize);
        float segmentSpacing = 4.8f;

        for (int i = 0; i < chainSize; i++)
        {
            GameObject tempStation = Instantiate(stationPrefab);
            tempStation.transform.localScale *= 0.80f;
            tempStation.transform.position = new Vector3(tempStation.transform.position.x + segmentSpacing, 1.0f);
            Vector2 aaLocation = tempStation.transform.GetChild(1).gameObject.transform.position;

            // for the last segment in the protein, remove the bond and disable the bond collider
            if (i == chainSize - 1)
            {
                tempStation.GetComponent<BoxCollider2D>().enabled = false;
                Destroy(tempStation.transform.GetChild(0).gameObject);
                tempStation.tag = "last";
            }

            GameObject tempAminoAcid = Instantiate(RandomAminoAcid());
            tempAminoAcid.transform.SetParent(tempStation.transform);
            tempAminoAcid.transform.localScale *= 0.37f;
            // set the location of each amino acid by using the difference between the location of the station's connector and the amino acid's offset point
            tempAminoAcid.transform.position = aaLocation;
            Vector2 aaOffset = tempStation.transform.GetChild(1).gameObject.transform.position - 
                                tempAminoAcid.transform.GetChild(1).gameObject.transform.position;
            tempAminoAcid.transform.position = aaLocation + aaOffset;

            stationQueue.Add(tempStation);
            aminoAcidQueue.Add(tempAminoAcid);
            segmentSpacing += 4.6f;
        }
    }

    private void MoveChain()
    {
        foreach (var obj in stationQueue)
        {
            obj.transform.position -= new Vector3(1.0f, 0.0f, 0.0f) * Time.deltaTime;
        }
    }

    private void UpdateQueue()
    {
        // keep track of the first and last segment in the chain
        stationTail = stationQueue.Last();
        aminoAcidTail = aminoAcidQueue.Last();
        stationHead = stationQueue[0];
        aminoAcidHead = aminoAcidQueue[0];
    }
}
