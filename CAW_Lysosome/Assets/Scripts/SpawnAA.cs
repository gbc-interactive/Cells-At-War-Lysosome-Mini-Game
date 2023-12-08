using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class SpawnAA : MonoBehaviour
{
    [SerializeField] GameObject[] aa;
    [SerializeField] GameObject aaLocation;

    public GameObject g;

    // Start is called before the first frame update
    void Start()
    {
        g = aa[GetRandAA()];

        GameObject AminoAcid = Instantiate(g) as GameObject;

        AminoAcid.transform.SetParent(gameObject.transform, false);
        AminoAcid.transform.position = aaLocation.transform.position;

        float offset = AminoAcid.GetComponent<AALocation>().GetOffset();

        AminoAcid.transform.position = new Vector3(AminoAcid.transform.position.x,
                                                    AminoAcid.transform.position.y - offset, 0);
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
