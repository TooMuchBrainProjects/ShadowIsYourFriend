using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    private double nextUpdate = 0.05;
    private bool playerIsDead = false;

    public Rigidbody2D rb;
    public double ScoreCounter;
    public int MaxScoreCounter;
    public double ScoreCounterSpeed;
    public int ScoreMinusPerSecond;

    public TextMeshProUGUI CurrentScoreText;
    public TextMeshProUGUI HighscoreText;

    [HideInInspector] private StealthMaster stealthMaster;
    [HideInInspector] public float PlayerDistance;
    [HideInInspector] public int CurrentScore;

    public int Highscore
    {
        get { return PlayerPrefs.GetInt("Highscore"); }
        set { PlayerPrefs.SetInt("Highscore", value); }
    }

    void Start()
    {
        stealthMaster = StealthMaster.Instance;

        // Zahl zwischen 0 und 1 machen (normalize)
        if(ScoreCounterSpeed >= 1)
            ScoreCounterSpeed = ScoreCounterSpeed / Mathf.Pow(10f, (float)ScoreCounterSpeed.ToString().Length);
        Debug.Log(ScoreCounterSpeed);
    }

    void Update()
    {
        // If the next update is reached
        if (Time.time >= nextUpdate)
        {
            Debug.Log(Time.time + ">=" + nextUpdate);
            // Change the next update (current second+1)
            nextUpdate = Mathf.FloorToInt(Time.time) + 1;
            // Call your fonction
            UpdateEverySecond();
        }

        CurrentScoreText.text = CurrentScore.ToString();
        HighscoreText.text = Highscore.ToString();
    }

    void UpdateEverySecond()
    {
        if (CurrentScore > 0 && !playerIsDead)
            CurrentScore -= ScoreMinusPerSecond;
        if (rb.position.x > PlayerDistance)
        {
            PlayerDistance = rb.position.x;
            CurrentScore += (int)ScoreCounter;

            // logistische Wachstumsfunktion
            ScoreCounter = MaxScoreCounter / (1 + ((MaxScoreCounter - ScoreCounter) / ScoreCounter) * Mathf.Pow((float)ScoreCounterSpeed, (float)ScoreCounter));
            if (Highscore < CurrentScore)
                Highscore = CurrentScore;

            Debug.LogWarning("CurrentScore: " + CurrentScore);
            Debug.LogError("Highscore: " + Highscore);
        }

        if(stealthMaster.maxAttention > 50)
            stealthMaster.maxAttention -= 0.1f;
        Debug.Log(stealthMaster.maxAttention);
    }

    public void ResetHighscore()
    {
        Highscore = 0;
    }

    public void PlayerIsDead()
    {
        playerIsDead = true;
    }
}
