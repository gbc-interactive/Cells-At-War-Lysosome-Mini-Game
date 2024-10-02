using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    private TextMeshProUGUI scoreText;
    [SerializeField] private int score = 0;

    // Start is called before the first frame update
    void Start()
    {
        scoreText = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.SetText("Score: " + score);
    }

    public int GetScore()
    {
        return score;
    }

    public void AddScore(int scoreAmountToAdd)
    {
        score += scoreAmountToAdd;
    }
}
