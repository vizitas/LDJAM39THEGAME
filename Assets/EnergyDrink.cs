using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyDrink : MonoBehaviour
{

    BoxCollider2D collider;
    [SerializeField]
    Collider2D player;

    float progress = 0f;
    float speed = 0.2f;
    [SerializeField]
    StatusBar EnergyProgress;

    [SerializeField]
    float power = 0.1f;

    [SerializeField]
    private GameObject TextPop;
    AudioSource audioSource;
    [SerializeField]
    List<AudioClip> makingSound;
    [SerializeField]
    List<AudioClip> gulp;
    private int ammount = 2;
    void Start()
    {
        collider = GetComponent<BoxCollider2D>();
        audioSource = GetComponent<AudioSource>();
        InvokeRepeating("EnergyLogic", 0, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerController.State == PlayerController.PlayerState.Walking && Input.GetKeyDown(KeyCode.E) && collider.IsTouching(player))
        {
            if (ammount > 0)
            {
                PlayerController.State = PlayerController.PlayerState.Energy;
                audioSource.PlayOneShot(makingSound[Random.Range(0, makingSound.Count)], 1f);
                ammount--;
            }
            else
            {
                GameObject textObject = Instantiate(TextPop);
                textObject.transform.position = transform.position;
                TextPop pop = textObject.GetComponent<TextPop>();
                pop.color = Color.cyan;
                pop.duration = 3f;
                pop.text = "Out of energy drinks";
                pop.size = 17;
                pop.speed = 0.01f;
            }
        }
        EnergyProgress.SetStatus(progress);
    }
    void EnergyLogic()
    {
        if (PlayerController.State == PlayerController.PlayerState.Energy)
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
                    pop.duration = 0.5f;
                    pop.text = "Gulp";
                    pop.speed = 0.1f;
                    audioSource.PlayOneShot(gulp[Random.Range(0, gulp.Count)], 1f);
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
