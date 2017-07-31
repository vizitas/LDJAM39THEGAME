using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialController : MonoBehaviour
{
    void Update()
    {
        if (GameStateController.Instance.CurrentState != GameStateController.GameStates.Tutorial)
        {
            gameObject.SetActive(false);
        }
    }
}
