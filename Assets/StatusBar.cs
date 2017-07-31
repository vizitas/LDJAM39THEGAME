using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusBar : MonoBehaviour
{
    public float OriginalXScale;
    public float OriginalYScale;

    float originalXPosition;
    float originalYPosition;
    [SerializeField]
    private bool IsXAxis = true;

    bool initialized = false;
    // Use this for initialization
    void Start()
    {
        initialized = true;
        OriginalXScale = transform.localScale.x;
        OriginalYScale = transform.localScale.y;
        originalYPosition = transform.localPosition.y;
        originalXPosition = transform.localPosition.x;
    }

    public void SetStatus(float percent)
    {
        if (!initialized)
            return;
        if (IsXAxis)
        {
            //MMMMM smells like makaroni
            transform.localScale = new Vector3(Mathf.Lerp(0, OriginalXScale, percent), transform.localScale.y, transform.localScale.z);
            float ammountMoved = (OriginalXScale - Mathf.Lerp(0, OriginalXScale, percent)) / 2;
            transform.localPosition = new Vector3(originalXPosition - ammountMoved, transform.localPosition.y, transform.localPosition.z);
        }
        else
        {
            transform.localScale = new Vector3(transform.localScale.x, Mathf.Lerp(0, OriginalYScale, percent), transform.localScale.z);
            float ammountMoved = (OriginalYScale - Mathf.Lerp(0, OriginalYScale, percent)) / 2;
            transform.localPosition = new Vector3(transform.localPosition.x, originalYPosition - ammountMoved, transform.localPosition.z);
        }
    }
}
//transform.localScale = new Vector3(transform.localScale.x, Mathf.Lerp(0, originalYScale, percent), transform.localScale.z);
//            transform.localPosition = new Vector3(transform.localPosition.x, originalYPosition* percent, transform.localPosition.z);