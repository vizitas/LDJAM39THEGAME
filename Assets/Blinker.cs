using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blinker : MonoBehaviour
{
    SpriteRenderer renderer;
    [SerializeField]
    float blinkingSpeed = 0.2f;
    float lastTogle = 0f;
    public bool deffault = true;
    // Use this for initialization
    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > lastTogle + blinkingSpeed)
        {
            lastTogle = Time.time;
            renderer.enabled = !renderer.enabled;
        }
    }
    public void OnDisable()
    {
        renderer.enabled = deffault;
    }
}
