using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public static Score Instance {get; private set;}

    private Text _scoreText;
    private int _score = 0;
    private string baseText = "Score: ";

    private void Awake(){
        Instance = this;
        _scoreText = GetComponent<Text>();
        UpdateScore();
    }

    private void UpdateScore()
    {
        _scoreText.text = baseText + _score.ToString();
    }

    public void AddScore(int value)
    {
        _score += value;
        UpdateScore();
    }

    public void ResetScore()
    {
        _score = 0;
        UpdateScore();
    }
}
