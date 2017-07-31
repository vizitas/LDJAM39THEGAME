using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreController : Singleton<ScoreController>
{
    public float Score { get { return score; } }
    float score = 0;
    float multiplier = 1;
    float combo = 0;
    [SerializeField]
    private TextMesh text;
    [SerializeField]
    private StatusBar comboBar;
    float lastDecay;
    float decaySpeed = 3;
    void Start()
    {
        InputController.letterDestroyed.AddListener(AddScore);
    }
    private void Update()
    {
        CalculateMultiplier();
        string distractionText = "";
        if (IsDistractions() > 0)
            distractionText = IsDistractions() + " DISTRACTION(S) ";

        if (RemainingTimeController.IsCrunchTime())
        {
            distractionText = "CRUNCH TIME!!! ";
            if (multiplier < 2)
                multiplier = 2f;
        }

        text.text = "SCORE: " + score + "\n" + distractionText + "MULTI: " + multiplier + "X";

        LetterController.maxSpeed = multiplier * 0.9f;
        Spawner.spawnSpeed = (1 / multiplier) * 1.1f;

        comboBar.SetStatus(combo / 90);

        ComboDecay();
    }

    void ComboDecay()
    {
        if (Time.time > lastDecay + decaySpeed)
        {
            lastDecay = Time.time;
            if (combo > 0)
                combo--;
        }
    }
    void AddScore(LetterController letter)
    {
        score += 10 * multiplier;
        IncreaseCombo();
    }
    private void IncreaseCombo()
    {
        if (IsDistractions() == 0)
            combo++;
    }

    private void CalculateMultiplier()
    {
        if (combo > 5)
            multiplier = 1.5f;
        if (combo > 25)
            multiplier = 2f;
        if (combo > 60)
            multiplier = 2.5f;
        if (combo > 90)
            multiplier = 3f;
        if (IsDistractions() == 1)
            multiplier = 1f;
        if (IsDistractions() >= 2)
            multiplier = 0.5f;
    }

    private int IsDistractions()
    {
        int result = 0;
        if (PhoneController.Instance.isOn)
            result++;
        if (DoorController.Instance.isOn)
            result++;
        return result;
    }

    public void DecreseMultiplier()
    {
        combo *= 0.3f;

    }
}
