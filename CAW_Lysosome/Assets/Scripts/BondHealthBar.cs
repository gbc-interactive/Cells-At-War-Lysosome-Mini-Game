using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BondHealthBar : MonoBehaviour
{
    [SerializeField] private Player player;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
        this.GetComponent<Image>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        HealthBarPosition();
        HealthBarVisibility();
    }

    private void HealthBarVisibility()
    {
        if (player.isAtPressStation)
        {
            this.GetComponent<Image>().enabled = true;
        }
        else
        {
            this.GetComponent<Image>().enabled = false;
        }
    }

    private void HealthBarPosition()
    {
        Vector3 offset = new Vector3(3.0f, 1.5f, 0.0f);
        transform.position = player.transform.position + offset;
    }
}
