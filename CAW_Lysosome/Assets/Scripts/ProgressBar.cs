using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    [SerializeField] private float minimum = 0;
    [SerializeField] private float maximum = 1;
    [SerializeField] private float currentFillAmount;

    [SerializeField] private Image starImage;
    private Image progressBar;
    private Vector3 starStartPosition;

    // Start is called before the first frame update
    void Start()
    {
        progressBar = GetComponent<Image>(); 
        starImage = transform.GetChild(0).gameObject.GetComponent<Image>();
        starStartPosition = starImage.gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        GetCurrentFillAmount();
        MoveStar();
    }

    private void GetCurrentFillAmount()
    {
        currentFillAmount = progressBar.fillAmount;
    }

    // moves star image along with the progress bar
    private void MoveStar()
    {
        starImage.gameObject.transform.position = new Vector3(
            starStartPosition.x + (8.954545454545455f * currentFillAmount),
            starStartPosition.y, 
            starStartPosition.z);
    }
}
