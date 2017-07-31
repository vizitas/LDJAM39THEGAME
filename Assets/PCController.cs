using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCController : MonoBehaviour
{
    BoxCollider2D collider;
    [SerializeField]
    Collider2D player;
    [SerializeField]
    GameObject console;
    [SerializeField]
    private GameObject TextPop;
    // Use this for initialization
    void Start()
    {
        collider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameStateController.Instance.CurrentState == GameStateController.GameStates.Tutorial && Input.GetKeyDown(KeyCode.E) && collider.IsTouching(player))
        {
            GameObject textObject = Instantiate(TextPop);
            textObject.transform.position = transform.position;
            TextPop pop = textObject.GetComponent<TextPop>();
            pop.color = Color.white;
            pop.duration = 3f;
            pop.text = "JAM NOT YET STARTED\n MAKE COFFE TO START JAM";
            pop.speed = 0.01f;
            return;
        }
        if (GameStateController.Instance.CurrentState != GameStateController.GameStates.InGame)
        {
            return;
        }
        if (PlayerController.State == PlayerController.PlayerState.Walking && Input.GetKey(KeyCode.E) && collider.IsTouching(player))
        {
            PlayerController.State = PlayerController.PlayerState.Programming;
            console.SetActive(true);
        }
        if (PlayerController.State == PlayerController.PlayerState.Programming && (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.Tab)))
        {
            console.SetActive(false);
            foreach (Transform child in console.transform)
            {
                if (child.tag == "Letter")
                {
                    Destroy(child.gameObject);
                }
            }
            ScoreController.Instance.DecreseMultiplier();
            PlayerController.State = PlayerController.PlayerState.Walking;
        }
    }
}
