using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisibleDuringDay : MonoBehaviour
{
    SpriteRenderer renderer;
    float realTimeDuration = 172800;

    // Use this for initialization
    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        float timeDialiation = realTimeDuration / GameStateController.Instance.GameDuration;
        DateTime realCurrentTime = new DateTime().AddSeconds((Time.time - GameStateController.Instance.GameStartTime) * timeDialiation);
        if (realCurrentTime.Hour < 12)
        {
            renderer.enabled = true;
        }
        else
        {
            renderer.enabled = false;
        }

    }
}
