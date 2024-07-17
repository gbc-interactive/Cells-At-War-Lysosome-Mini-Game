using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Reflection;
using UnityEngine;
using UnityEngine.XR;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class SpawnAA : MonoBehaviour
{
    [SerializeField] GameObject[] aa;
    [SerializeField] GameObject aaLocation;
    [SerializeField] GameObject Connector;

    public GameObject g;
    private GameObject AminoAcid;

    private float moveSpeed = 0.01f;

    [SerializeField] private Material stationMaterial;

    // Start is called before the first frame update
    void Start()
    {
        stationMaterial = GetComponent<SpriteRenderer>().material;

        g = aa[GetRandAA()];
        //g = aa[15];

        AminoAcid = Instantiate(g) as GameObject;

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
        FadeAminoAcid();
    }

    void FixedUpdate()
    {
        // move each station in the chain 
        transform.position += Vector3.left * 0.05f;

        //StartCoroutine("LerpPosition");
    }

    // move amino acids to random position 
    private IEnumerator LerpPosition()
    {
        Camera mainCamera = Camera.main;
        Vector3 randomScreenPosition = new Vector3(Random.Range(0f, mainCamera.pixelWidth), Random.Range(0f, mainCamera.pixelHeight), 0.0f);
        Vector3 randomWorldPosition = mainCamera.ScreenToWorldPoint(randomScreenPosition);

        Vector3 movementDirection = (randomWorldPosition - transform.position).normalized;

        while (Vector2.Distance(transform.position, randomWorldPosition) > 0.5f)
        {
            transform.position += movementDirection * moveSpeed * Time.deltaTime;
            yield return new WaitForFixedUpdate();
        }
    }

    public int GetRandAA()
    {
        return Random.Range(1, aa.Length);
    }

    private void FadeAminoAcid()
    {
        // station alpha channel
        Color stationColour = stationMaterial.color;
        stationColour.a -= 0.01f * Time.fixedDeltaTime;
        stationMaterial.color = stationColour;

        // fade all child component alpha channels if they have a sprite renderer 
        foreach (Transform child in gameObject.transform)
        {
            if (child.GetComponent<SpriteRenderer>() != null)
            {
                Material childMaterial = child.GetComponent<SpriteRenderer>().material;
                Color childColour = childMaterial.color;
                childColour.a -= 0.01f * Time.fixedDeltaTime;
                childMaterial.color = childColour;
            }
        }

        // destroy entire chain if player fails to complete it before it fades away
        if (stationMaterial.color.a <= 0.0f)
        {
            Destroy(gameObject);

            // TODO: spawn a new chain
            
        }
    }
}
