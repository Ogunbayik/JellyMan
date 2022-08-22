using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    [SerializeField] private Text scoreText;

    [HideInInspector]
    public float currentScore = 0;
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }
    private void Start()
    {
        scoreText.text = "Score: " + currentScore;
    }
    private void OnEnable()
    {
        PlayerTriggered.OnTriggeredGem += AddScore;
    }
    private void OnDisable()
    {
        PlayerTriggered.OnTriggeredGem -= AddScore;
    }
    public void AddScore(int score)
    {
        currentScore += score;
        UpdateScore();
    }
    public void UpdateScore()
    {
        if (currentScore <= 0)
            currentScore = 0;

        scoreText.text = "Score: " + currentScore;
    }

}
