using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LetterController : MonoBehaviour
{
    public char letter = 'A';
    public static float maxSpeed = 1f;

    bool isInInput = false;
    float inputTime = 0f;
    [SerializeField]
    float maxInputTime = 3f;
    Rigidbody2D body;
    Blinker blinker;
    // Use this for initialization
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        TextMesh text = GetComponentInChildren<TextMesh>();
        text.text = letter.ToString().ToUpper();
        blinker = GetComponent<Blinker>();
    }

    void Update()
    {
        if (body.velocity.y < maxSpeed)
        {
            body.velocity = new Vector2(body.velocity.x, -maxSpeed);
        }
        FailLogic();
    }

    private void FailLogic()
    {
        if (isInInput)
        {
            if (Time.time > inputTime + (maxInputTime / 2))
            {
                blinker.enabled = true;
            }
            if (Time.time > inputTime + maxInputTime)
            {
                ScoreController.Instance.DecreseMultiplier();
                Destroy(gameObject);
                PlayerPower.Instance.InflictPowerDamage(0.1f);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Input")
        {
            isInInput = true;
            inputTime = Time.time;
        }
    }
}
