using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemainingTimeController : MonoBehaviour
{
    TextMesh textMesh;
    MeshRenderer renderer;
    float realTimeDuration = 172800;
    float timeDialiation;
    public static TimeSpan remainingTime;
    void Start()
    {
        textMesh = GetComponent<TextMesh>();
        renderer = GetComponent<MeshRenderer>();
        timeDialiation = realTimeDuration / GameStateController.Instance.GameDuration;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameStateController.Instance.CurrentState == GameStateController.GameStates.Tutorial || PlayerController.State == PlayerController.PlayerState.Programming)
        {
            renderer.enabled = false;
            return;
        }
        else
        {
            renderer.enabled = true;
        }
        DateTime realCurrentTime = new DateTime().AddSeconds((Time.time - GameStateController.Instance.GameStartTime) * timeDialiation);
        DateTime durationInrealTimeDate = new DateTime().AddSeconds(realTimeDuration);
        remainingTime = durationInrealTimeDate - realCurrentTime;
        var h = (int)(remainingTime.TotalMinutes / 60);
        var m = remainingTime.TotalMinutes % 60;
        textMesh.text = "ENDS IN\n" + h + ":" + m.ToString("00");
    }
    public static bool IsCrunchTime()
    {
        if (GameStateController.Instance.CurrentState == GameStateController.GameStates.Tutorial)
            return false;
        return RemainingTimeController.remainingTime.TotalHours < 6;
    }
}
