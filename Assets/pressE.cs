using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pressE : MonoBehaviour
{
    [SerializeField]
    MeshRenderer text;

    private void OnTriggerStay2D(Collider2D collider)
    {
        if (PlayerController.State == PlayerController.PlayerState.Walking)
            text.enabled = true;
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (PlayerController.State == PlayerController.PlayerState.Walking)
            text.enabled = true;
    }
    private void OnTriggerExit2D(Collider2D collider)
    {
        text.enabled = false;
    }
}
