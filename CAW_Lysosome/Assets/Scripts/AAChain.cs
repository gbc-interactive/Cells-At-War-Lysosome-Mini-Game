using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR;
using Random = UnityEngine.Random;


// this replaces old SpawnAA.cs script
public class AAChain : MonoBehaviour
{
    [SerializeField] private int minChainSize = 3;
    [SerializeField] private int maxChainSize = 6;
    [SerializeField] private GameObject stationPrefab;
    [SerializeField] private GameObject aminoAcidPrefab;
    [SerializeField] private GameObject[] aminoAcidPrefabList;
    [SerializeField] public List<GameObject> stationList;
    [SerializeField] public List<GameObject> aminoAcidList;

    // track head and tail of the chain
    [SerializeField] public GameObject stationHead;
    [SerializeField] public GameObject stationTail;
    [SerializeField] public GameObject aminoAcidHead;
    [SerializeField] public GameObject aminoAcidTail;

    private GameObject parent;
    private GameObject anchor;
    [SerializeField] private Vector3 destination;
    [SerializeField] private float speed = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        stationList = new List<GameObject>();
        aminoAcidList = new List<GameObject>();
        SpawnChain();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateQueue();
        ChainMovement();
        

        Debug.DrawLine(stationHead.transform.position, destination, Color.green);
    }

    void FixedUpdate()
    {

    }

    private GameObject RandomAminoAcid()
    {
        return aminoAcidPrefabList[Random.Range(0, aminoAcidPrefabList.Length)];
    }

    public void SpawnChain()
    {
        int chainSize = Random.Range(minChainSize, maxChainSize);
        float segmentSpacing = 4.8f;

        // creating an empty parent game object for all objects in the chain
        parent = new GameObject();
        parent.name = "ChainParent";

        for (int i = 0; i < chainSize; i++)
        {
            
            GameObject tempStation = Instantiate(stationPrefab);
            tempStation.transform.localScale *= 0.80f;
            tempStation.transform.position = new Vector3(tempStation.transform.position.x + segmentSpacing, 1.0f);
            Vector2 aaLocation = tempStation.transform.GetChild(1).gameObject.transform.position;
            tempStation.transform.SetParent(parent.transform);

            if (i == 0)
            {
                var temp = tempStation.gameObject.GetComponent<HingeJoint2D>();
                Destroy(temp);
            }

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

            stationList.Add(tempStation);
            aminoAcidList.Add(tempAminoAcid);
            segmentSpacing += 4.6f;
        }

        // configure the joints
        for (int i = 1; i < stationList.Count; i++)
        {
            HingeJoint2D joint = stationList[i].GetComponent<HingeJoint2D>();
            joint.connectedBody = stationList[i - 1].GetComponent<Rigidbody2D>();
        }
    }

    private void MoveChain()
    {
        foreach (var obj in stationList)
        {
            obj.transform.position -= new Vector3(1.0f, 0.0f, 0.0f) * Time.deltaTime;
        }
    }

    private void UpdateQueue()
    {
        // keep track of the first and last segment in the chain
        stationTail = stationList.Last();
        aminoAcidTail = aminoAcidList.Last();
        stationHead = stationList[0];
        aminoAcidHead = aminoAcidList[0];
    }

    public List<GameObject> GetStationList()
    {
        return stationList;
    }

    private Vector2 GetNewDestinationPoint()
    {
        // Get the square min and max coordinates and pick a random point inside it for the head of chain to move towards
        SpriteRenderer areaGameObject = UnityEngine.GameObject.Find("ChainMovementPointArea").GetComponent<SpriteRenderer>();
        Vector2 minBoundArea = areaGameObject.bounds.min;
        Vector2 maxBoundArea = areaGameObject.bounds.max;
        destination = new Vector3(Random.Range(minBoundArea.x, maxBoundArea.x), Random.Range(minBoundArea.y, maxBoundArea.y), 0.0f);

        return destination;
    }

    private void ChainMovement()
    {
        // if the chain's head isn't close enough to the destination point, move towards the destination
        Vector3 chainHeadPosition = stationList[0].transform.position;
        Vector3 vectorToDestination = destination - chainHeadPosition; // heading
        float distanceToDestination = Vector2.Distance(destination, chainHeadPosition); // distance
        Vector3 directionToDestinationNormalized = vectorToDestination / distanceToDestination; // normalized direction
        
        Debug.Log("distance to target: " + distanceToDestination);

        if (stationHead != null)
        {
            Rigidbody2D stationHeadRB = stationHead.GetComponent<Rigidbody2D>();
            
            //stationHeadRB.velocity += new Vector2(directionToDestinationNormalized.x * Time.deltaTime * speed,
            //directionToDestinationNormalized.y * Time.deltaTime * speed);

             stationHeadRB.AddForceAtPosition(new Vector2(
                 directionToDestinationNormalized.x * Time.deltaTime * speed,
                 directionToDestinationNormalized.y * Time.deltaTime * speed), stationHead.gameObject.transform.position, ForceMode2D.Impulse);
        }

        // pick a new point to move to when the head gets close enough
        if (distanceToDestination < 2.0f)
        {
            // reducing velocity when reaching point
            foreach (var stationGameObject in stationList)
            {
                stationGameObject.GetComponent<Rigidbody2D>().velocity *= 0.2f;
                stationGameObject.GetComponent<Rigidbody2D>().angularVelocity *= 0.2f;
            }
            GetNewDestinationPoint();
        }
    }
}
