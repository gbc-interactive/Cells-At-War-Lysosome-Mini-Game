using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BondManager : MonoBehaviour
{
    [SerializeField] Sprite[] aa;
    [SerializeField] GameObject bond;
    [SerializeField] GameObject bondBridge;

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(bond, new Vector3(0f, -2f, 0f), Quaternion.identity);
    }

    public int GetRandAA()
    {
        return Random.Range(1, aa.Length);
    }

    

    // Update is called once per frame
    void Update()
    {
        
    }
}
