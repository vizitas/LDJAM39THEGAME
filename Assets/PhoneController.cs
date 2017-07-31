using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//I get paid by lines of code
public class PhoneController : Singleton<PhoneController>
{
    BoxCollider2D collider;
    [SerializeField]
    Collider2D player;
    public bool isOn;
    public float intervals = 20;
    public float probability = 0.5f;
    public Blinker lightBlinker;
    [SerializeField]
    private GameObject TextPop;
    [SerializeField]
    private AudioClip ring;
    private AudioSource audioSource;
    [SerializeField]
    private List<AudioClip> slams;
    // Use this for initialization
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        collider = GetComponent<BoxCollider2D>();
        InvokeRepeating("Roll", intervals, intervals);
        InvokeRepeating("Ring", 0, 3);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && collider.IsTouching(player))
        {
            audioSource.PlayOneShot(slams[Random.Range(0, slams.Count)], 1f);
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
        }
    }
    void Ring()
    {
        if (isOn)
        {
            audioSource.PlayOneShot(ring, 0.5f);
            GameObject textObject = Instantiate(TextPop);
            textObject.transform.position = transform.position;
            TextPop pop = textObject.GetComponent<TextPop>();
            pop.color = Color.yellow;
            pop.duration = 0.5f;
            pop.text = "Ring";
            pop.speed = 0.1f;
        }
    }

}
