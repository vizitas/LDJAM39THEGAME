using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public static float spawnSpeed = 1f;
    private float lastSpawn;
    [SerializeField]
    private GameObject console;
    [SerializeField]
    private GameObject letter;

    private GameObject[] spawnPoints;
    public bool isSpawnEmpty;
    private void Start()
    {
        spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint");
    }
    // Update is called once per frame
    void Update()
    {

        if (isSpawnEmpty && (Time.time > lastSpawn + spawnSpeed))
        {
            lastSpawn = Time.time;
            SpawnNewLetter();
        }
    }
    private void FixedUpdate()
    {
        isSpawnEmpty = true;
    }
    private void SpawnNewLetter()
    {
        GameObject newLetter = Instantiate(letter);
        int spawnPoint = Random.Range(0, spawnPoints.Length);
        newLetter.transform.position = spawnPoints[spawnPoint].transform.position;
        newLetter.transform.parent = console.transform;
        if (RemainingTimeController.IsCrunchTime())
        {
            Rigidbody2D body = newLetter.GetComponent<Rigidbody2D>();
            body.AddForce(Random.insideUnitCircle, ForceMode2D.Impulse);
            body.AddTorque(Random.Range(-1f,1f),ForceMode2D.Impulse);
        }
        newLetter.GetComponent<LetterController>().letter = (char)Random.Range(65, 91);
    }
    void OnTriggerStay2D(Collider2D other)
    {
        isSpawnEmpty = false;
    }

}
