using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndGameScoreText : MonoBehaviour
{
    float authorScore = 5400;
    void Start()
    {
        Text text = GetComponent<Text>();
        text.text = "YOUR SCORE: " + GameStateController.Instance.Score + "\n";
        text.text += "IGN RATING: " + Random.Range(8f, 10f).ToString("0.0") + "\n";
        text.text += "STEAM REVIEWS: " + ((GameStateController.Instance.Score / authorScore) * 100f).ToString("00.00") + "%\n";
    }

}
