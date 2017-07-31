using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoffeController : MonoBehaviour
{

    BoxCollider2D collider;
    [SerializeField]
    Collider2D player;

    float progress = 0f;
    float speed = 0.1f;
    [SerializeField]
    StatusBar coffeProgress;

    [SerializeField]
    float coffePower = 0.1f;

    [SerializeField]
    private GameObject TextPop;
    AudioSource audioSource;
    [SerializeField]
    List<AudioClip> makingSound;
    [SerializeField]
    List<AudioClip> gulp;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        collider = GetComponent<BoxCollider2D>();
        InvokeRepeating("CoffeLogic", 0, 1.5f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && collider.IsTouching(player))
        {
            if (OvenController.Instance.Heat > 0.5f)
            {
                PlayerController.State = PlayerController.PlayerState.Coffee;
                audioSource.PlayOneShot(makingSound[Random.Range(0, makingSound.Count)], 1f);
            }
            else
            {
                GameObject textObject = Instantiate(TextPop);
                textObject.transform.position = transform.position;
                TextPop pop = textObject.GetComponent<TextPop>();
                pop.color = Color.cyan;
                pop.duration = 3f;
                pop.text = "Kettle too cold\n start the kettle";
                pop.size = 17;
                pop.speed = 0.01f;
            }
        }
        coffeProgress.SetStatus(progress);
    }
    void CoffeLogic()
    {
        if (PlayerController.State == PlayerController.PlayerState.Coffee)
        {
            if (progress < 1)
            {
                progress += speed;
                if (progress > 0.5f)
                {
                    PlayerPower.Instance.ChangePower(coffePower);
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
                if (GameStateController.Instance.CurrentState == GameStateController.GameStates.Tutorial)
                    GameStateController.Instance.StartGame();
            }
        }
    }
}
