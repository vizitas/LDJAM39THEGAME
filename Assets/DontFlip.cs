using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontFlip : MonoBehaviour
{
    private StatusBar bar;
    bool side;
    bool flip = false;
    // Use this for initialization
    void Start()
    {
        bar = GetComponent<StatusBar>();
        side = transform.parent.localScale.x > 0;
    }

    // Update is called once per frame
    void Update()
    {
        flip = (side && transform.parent.localScale.x < 0) || (!side && transform.parent.localScale.x > 0);
        if (flip)
        {
            if (bar != null)
            {
                bar.OriginalXScale *= -1;
                bar.OriginalYScale *= -1;
            }

            side = transform.parent.localScale.x > 0;
            flip = false;
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }
    }
}
