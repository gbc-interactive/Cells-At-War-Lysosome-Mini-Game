using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AALocation : MonoBehaviour
{
    GameObject p1;
    GameObject p2;

    public float GetOffset()
    {
        foreach (Transform child in this.transform)
        {
            if (child.gameObject.tag == "p1")
            {
                p1= child.gameObject;
            }

            if (child.gameObject.tag == "p2")
            {
                p2= child.gameObject;
            }
        }

        float offset = Vector3.Distance(p1.transform.position, p2.transform.position);

        return offset;
    }
}
