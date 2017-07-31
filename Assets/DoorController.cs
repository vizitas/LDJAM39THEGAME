using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//I get paid by lines of code
public class DoorController : Singleton<DoorController>
{
    BoxCollider2D collider;
    [SerializeField]
    Collider2D player;
    public bool isOn;
    public float intervals = 40;
    public float probability = 0.5f;
    public Blinker lightBlinker;
    [SerializeField]
    private GameObject TextPop;
    [SerializeField]
    private AudioClip doorBell;
    private AudioSource audioSource;
    [SerializeField]
    private List<AudioClip> shouting;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        collider = GetComponent<BoxCollider2D>();
        InvokeRepeating("Roll", intervals, intervals);
        InvokeRepeating("Ring", 0, 5);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && collider.IsTouching(player))
        {
            audioSource.PlayOneShot(shouting[Random.Range(0, shouting.Count)], 1f);
            isOn = false;
            lightBlinker.enabled = false;
        }

    }
    void Roll()
    {
        if (GameStateController.Instance.CurrentState != GameStateController.GameStates.InGame)
            return;
        if (Random.Range(0f, 1f) < probability)
        {
            lightBlinker.enabled = true;
            isOn = true;
            Ring();
        }
    }
    void Ring()
    {
        if (isOn)
        {
            audioSource.PlayOneShot(doorBell, 0.2f);
            GameObject textObject = Instantiate(TextPop);
            textObject.transform.position = transform.position;
            TextPop pop = textObject.GetComponent<TextPop>();
            pop.color = Color.yellow;
            pop.duration = 0.2f;
            pop.text = "Ding\nDing";
            pop.speed = 0.1f;
        }
    }
}
