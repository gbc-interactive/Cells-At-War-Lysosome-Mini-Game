using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BondManager : MonoBehaviour
{
    [SerializeField] Sprite[] aa;

    // Start is called before the first frame update
    void Start()
    {
    }

    public int GetRandAA()
    {
        return Random.Range(1, aa.Length);
    }

    

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            Debug.Log(aa[GetRandAA()].name);
        }
    }
}
