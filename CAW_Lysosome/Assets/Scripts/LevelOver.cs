using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelOver : MonoBehaviour
{
    [SerializeField] private Score score;
    [SerializeField] private int scoreFor1Stars;
    [SerializeField] private int scoreFor2Stars;
    [SerializeField] private int scoreFor3Stars;

    [SerializeField] private GameObject star1;
    [SerializeField] private GameObject star2;
    [SerializeField] private GameObject star3;
    [SerializeField] private GameObject quoteText;
    [SerializeField] private Sprite quoteText1Star;
    [SerializeField] private Sprite quoteText2Star;
    [SerializeField] private Sprite quoteText3Star;

    // Start is called before the first frame update
    void Start()
    {
        score = FindObjectOfType<Score>();
    }

    // Update is called once per frame
    void Update()
    {
        if (score.GetScore() <= scoreFor1Stars && score.GetScore() < scoreFor2Stars)
        {
            StarLevel1();
        }
        else if (score.GetScore() >= scoreFor2Stars && score.GetScore() < scoreFor3Stars)
        {
            StarLevel2();
        }
        else if (score.GetScore() >= scoreFor3Stars)
        {
            StarLevel3();
        }
    }

    private void StarLevel1()
    {
        star1.GetComponent<Image>().enabled = true;
        star2.GetComponent<Image>().enabled = false;
        star3.GetComponent<Image>().enabled = false;
        quoteText.GetComponent<Image>().overrideSprite = quoteText1Star;
    }

    private void StarLevel2()
    {
        star1.GetComponent<Image>().enabled = true;
        star2.GetComponent<Image>().enabled = true;
        star3.GetComponent<Image>().enabled = false;
        quoteText.GetComponent<Image>().overrideSprite = quoteText2Star;
    }

    private void StarLevel3()
    {
        star1.GetComponent<Image>().enabled = true;
        star2.GetComponent<Image>().enabled = true;
        star3.GetComponent<Image>().enabled = true;
        quoteText.GetComponent<Image>().overrideSprite = quoteText3Star;
    }
}
