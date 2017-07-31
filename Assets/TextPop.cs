using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextPop : MonoBehaviour
{
    public float duration = 5;
    public string text;
    public int size = 16;
    public float speed = 0.1f;
    public Color color;
    private float aliveSince;
    private TextMesh textMesh;
    // Use this for initialization
    void Start()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 0.1f);
        textMesh = GetComponent<TextMesh>();
        textMesh.text = text;
        textMesh.fontSize = size;
        textMesh.characterSize = size / 100f;
        textMesh.color = color;
        aliveSince = Time.time;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y + speed, transform.position.z);
        textMesh.color = new Color(textMesh.color.r, textMesh.color.g, textMesh.color.b, Mathf.Lerp(1f, 0.5f, Time.time / (aliveSince + duration)));
        if (Time.time > aliveSince + duration)
        {
            Destroy(gameObject);
        }
    }
}
