using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InputController : MonoBehaviour
{
    List<GameObject> letters = new List<GameObject>();
    public class LetterDestroyedEvent : UnityEvent<LetterController> { }
    AudioSource audioSource;
    [SerializeField]
    List<AudioClip> selectClips;
    public static LetterDestroyedEvent letterDestroyed = new LetterDestroyedEvent();

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    void Update()
    {
        for (int i = 0; i < letters.Count; i++)
        {
            var letter = letters[i];
            if (letter == null)
            {
                letters.RemoveAt(i);
                continue;
            }
            var letterController = letter.GetComponent<LetterController>();
            if (Input.GetKey(letterController.letter.ToString().ToLower()))
            {
                letterDestroyed.Invoke(letterController);
                letters.Remove(letter);
                PlayRandomSelect();
                Destroy(letter);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        letters.Add(collider.gameObject);
    }
    private void PlayRandomSelect()
    {
        audioSource.PlayOneShot(selectClips[Random.Range(0,selectClips.Count)],0.2f);
    }
}
