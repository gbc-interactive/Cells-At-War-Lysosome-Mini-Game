using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class SpawnAA : MonoBehaviour
{
    [SerializeField] GameObject[] aa;

    // Start is called before the first frame update
    void Start()
    {
        GameObject AminoAcid = Instantiate(aa[GetRandAA()]) as GameObject;
        
        AminoAcid.transform.SetParent(gameObject.transform, false);
        AminoAcid.transform.position = gameObject.transform.position + new Vector3(1.5f, -1.8f);
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
