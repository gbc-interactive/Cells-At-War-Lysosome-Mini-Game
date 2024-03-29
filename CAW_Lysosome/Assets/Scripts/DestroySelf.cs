using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class DestroySelf : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(EndSelf());
    }

    IEnumerator EndSelf()
    {
        yield return new WaitForSeconds(20);
        Destroy(this.gameObject);
    }
}
