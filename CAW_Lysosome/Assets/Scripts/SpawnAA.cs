using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class SpawnAA : MonoBehaviour
{
    [SerializeField] GameObject[] aa;
    [SerializeField] GameObject aaLocation;
    [SerializeField] GameObject Connector;

    public GameObject g;

    // Start is called before the first frame update
    void Start()
    {
        g = aa[GetRandAA()];
        //g = aa[15];

        GameObject AminoAcid = Instantiate(g) as GameObject;

        AminoAcid.transform.SetParent(gameObject.transform, false);
        AminoAcid.transform.position = aaLocation.transform.position;

        float offset = AminoAcid.GetComponent<AALocation>().GetOffset();

        if (g == aa[0])
        {
            AminoAcid.transform.position = new Vector3(AminoAcid.transform.position.x,
                                                AminoAcid.transform.position.y - offset, 0);
        }
        if (g == aa[1])
        {
            AminoAcid.transform.position = new Vector3(AminoAcid.transform.position.x - 0.8f,
                                                        AminoAcid.transform.position.y - offset, 0);
        }
        if (g == aa[2])
        {
            AminoAcid.transform.position = new Vector3(AminoAcid.transform.position.x + 0.95f,
                                                        AminoAcid.transform.position.y - offset, 0);
        }
        if (g == aa[3])
        {
            AminoAcid.transform.position = new Vector3(AminoAcid.transform.position.x,
                                                AminoAcid.transform.position.y - offset, 0);
        }
        if (g == aa[4])
        {
            AminoAcid.transform.position = new Vector3(AminoAcid.transform.position.x,
                                                AminoAcid.transform.position.y - offset, 0);
        }
        if (g == aa[5])
        {
            AminoAcid.transform.position = new Vector3(AminoAcid.transform.position.x,
                                                AminoAcid.transform.position.y - offset, 0);
        }
        if (g == aa[6])
        {
            AminoAcid.transform.position = new Vector3(AminoAcid.transform.position.x + 0.25f,
                                                   AminoAcid.transform.position.y - offset, 0);
        }
        if (g == aa[7])
        {
            AminoAcid.transform.position = new Vector3(AminoAcid.transform.position.x + 0.35f,
                                                        AminoAcid.transform.position.y - offset, 0);
        }
        if (g == aa[8])
        {
            AminoAcid.transform.position = new Vector3(AminoAcid.transform.position.x,
                                                        AminoAcid.transform.position.y - offset, 0);
        }
        if (g == aa[9])
        {
            AminoAcid.transform.position = new Vector3(AminoAcid.transform.position.x - 0.25f,
                                                        AminoAcid.transform.position.y - offset, 0);
        }
        if (g == aa[10])
        {
            AminoAcid.transform.position = new Vector3(AminoAcid.transform.position.x,
                                                        AminoAcid.transform.position.y - offset, 0);
        }
        if (g == aa[11])
        {
            AminoAcid.transform.position = new Vector3(AminoAcid.transform.position.x - 0.45f,
                                                        AminoAcid.transform.position.y - offset, 0);
        }
        if (g == aa[12])
        {
            AminoAcid.transform.position = new Vector3(AminoAcid.transform.position.x + 0.30f,
                                                        AminoAcid.transform.position.y - offset, 0);
        }
        if (g == aa[13])
        {
            AminoAcid.transform.position = new Vector3(AminoAcid.transform.position.x - 0.10f,
                                                        AminoAcid.transform.position.y - offset, 0);
        }
        if (g == aa[14])
        {
            AminoAcid.transform.position = new Vector3(AminoAcid.transform.position.x,
                                                        AminoAcid.transform.position.y - offset, 0);
        }
        if (g == aa[15])
        {
            AminoAcid.transform.position = new Vector3(AminoAcid.transform.position.x + 0.10f,
                                                        AminoAcid.transform.position.y - offset, 0);
        }
        if (g == aa[16])
        {
            AminoAcid.transform.position = new Vector3(AminoAcid.transform.position.x + 0.30f,
                                                        AminoAcid.transform.position.y - offset, 0);
        }
        if (g == aa[17])
        {
            AminoAcid.transform.position = new Vector3(AminoAcid.transform.position.x,
                                                        AminoAcid.transform.position.y - offset, 0);
        }
        if (g == aa[18])
        {
            AminoAcid.transform.position = new Vector3(AminoAcid.transform.position.x,
                                                        AminoAcid.transform.position.y - offset, 0);
        }
        if (g == aa[19])
        {
            AminoAcid.transform.position = new Vector3(AminoAcid.transform.position.x,
                                                        AminoAcid.transform.position.y - offset, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int GetRandAA()
    {
        return Random.Range(1, aa.Length);
    }
}
