using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunController : MonoBehaviour
{

    BoxCollider2D collider;
    [SerializeField]
    Collider2D player;

    float progress = 0f;
    float speed = 0.2f;
    [SerializeField]
    StatusBar SunProgress;

    [SerializeField]
    float power = 0.02f;

    [SerializeField]
    private GameObject TextPop;
    
    float realTimeDuration = 172800;
    void Start()
    {
        collider = GetComponent<BoxCollider2D>();
        InvokeRepeating("SunLogic", 0, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && collider.IsTouching(player))
        {
            float timeDialiation = realTimeDuration / GameStateController.Instance.GameDuration;
            DateTime realCurrentTime = new DateTime().AddSeconds((Time.time - GameStateController.Instance.GameStartTime) * timeDialiation);
            if (realCurrentTime.Hour<12)
            {
                PlayerController.State = PlayerController.PlayerState.Sun;

            }
            else
            {
                GameObject textObject = Instantiate(TextPop);
                textObject.transform.position = transform.position;
                TextPop pop = textObject.GetComponent<TextPop>();
                pop.color = Color.cyan;
                pop.duration = 3f;
                pop.text = "No Sun";
                pop.size = 17;
                pop.speed = 0.01f;
            }
        }
        SunProgress.SetStatus(progress);
    }
    void SunLogic()
    {
        if (PlayerController.State == PlayerController.PlayerState.Sun)
        {
            if (progress < 1)
            {
                progress += speed;
                if (progress > 0.1f)
                {
                    PlayerPower.Instance.ChangePower(power);
                    GameObject textObject = Instantiate(TextPop);
                    textObject.transform.position = transform.position;
                    TextPop pop = textObject.GetComponent<TextPop>();
                    pop.color = Color.yellow;
                    pop.duration = 3f;
                    pop.text = "PRAISE THE SUN";
                    pop.speed = 0.01f;
                }
            }
            else
            {
                progress = 0;
                PlayerController.State = PlayerController.PlayerState.Walking;
            }
        }
    }
}

