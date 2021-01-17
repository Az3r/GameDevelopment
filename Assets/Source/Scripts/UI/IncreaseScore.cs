using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class IncreaseScore : MonoBehaviour
{
    public TextMeshProUGUI score;
    public float maxTime;
    public float maxScore;
    public float currentScore;

    [Header("Observed Fields")]
    [SerializeField]
    public float speed;

    private void Start()
    {
        SetScore(maxScore);
    }

    void Update()
    {
        UpdateScore();
    }

    private void UpdateScore()
    {
        if (Mathf.Approximately(maxScore, currentScore)) return;

        // Update score
        currentScore += speed * Time.deltaTime;
        currentScore = Mathf.Min(currentScore, maxScore);

        // Update Score UI
        score.text = Mathf.RoundToInt(currentScore).ToString("0000000000");
    }

    public void SetScore(float value)
    {
        maxScore = value;

        // Calculate the speed currentScore needed to be equal to maxScore
        // in less than maxTime seconds.
        speed = Mathf.Ceil((maxScore - currentScore) / maxTime);
    }
}
