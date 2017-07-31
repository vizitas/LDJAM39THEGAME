using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OvenController : Singleton<OvenController>
{
    bool isOn;
    float heat = 0.75f;
    public float Heat { get { return heat; } }
    float heatSpeed = 0.01f;
    float coolDownSpeed = 0.01f;

    BoxCollider2D collider;
    [SerializeField]
    StatusBar heatBar;
    [SerializeField]
    Collider2D player;
    private AudioSource[] audio;
    [SerializeField]
    private AudioClip spark;
    [SerializeField]
    private AudioClip fire;
    [SerializeField]
    private AudioClip startfire;
    bool success = false;
    [SerializeField]
    GameObject fireFX;
    [SerializeField]
    GameObject smokeFx;
    void Start()
    {
        audio = GetComponents<AudioSource>();
        collider = GetComponent<BoxCollider2D>();
        InvokeRepeating("UpdateLogic", 0, 1);
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && collider.IsTouching(player) && PlayerController.State == PlayerController.PlayerState.Walking)
        {
            audio[0].Stop();
            audio[1].Stop();
            if (!isOn)
            {
                PlayerController.State = PlayerController.PlayerState.Oven;
            }
            if (isOn)

                isOn = false;
        }
        heatBar.SetStatus(heat);
        if (heat > 1)
        {
            smokeFx.SetActive(true);
        }
        else
        {
            smokeFx.SetActive(false);
        }
    }
    void UpdateLogic()
    {

        if (PlayerController.State == PlayerController.PlayerState.Oven && !success)
        {
            audio[0].clip = spark;
            audio[0].Play();
            success = Random.Range(0f, 1f) <= 0.4f;
            if (success)
            {
                Invoke("DelayedSuccess", 1.7f);
            }
        }
        if (isOn)
        {
            fireFX.SetActive(true);
            if (!audio[1].isPlaying)
            {
                audio[1].clip = fire;
                audio[1].loop = true;
                audio[1].Play();
            }
            heat += heatSpeed;
        }
        else
        {
            fireFX.SetActive(false);
            if (heat > 0)
                heat -= coolDownSpeed;
        }
    }
    void DelayedSuccess()
    {
        success = false;
        isOn = true;
        audio[1].clip = startfire;
        audio[1].loop = false;
        audio[1].Play();
        PlayerController.State = PlayerController.PlayerState.Walking;
    }
}
