using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateController : Singleton<GameStateController>
{
    public enum GameStates { Tutorial, InGame, Menu }
    public float Score { get { return score; } }
    private float score;
    private GameStates currentState = GameStates.InGame;
    public GameStates CurrentState { get { return currentState; } }
    public float GameStartTime { get { return gameStartTime; } }
    private float gameStartTime;
    public float GameDuration { get { return gameDuration; } }
    private float gameDuration = 600;

    void Start()
    {
        score = 0;
        DontDestroyOnLoad(this);
    }
    void Update()
    {
        if (currentState == GameStates.InGame)
        {
            CheckForGameWin();
            CheckForGameOver();
        }
    }
    public void StartGame()
    {
        currentState = GameStates.InGame;
        gameStartTime = Time.time;
    }
    void CheckForGameOver()
    {
        if (PlayerPower.Instance.Power < 0)
        {
            currentState = GameStates.Menu;
            SceneManager.LoadScene("FellASleep");
        }
        if (OvenController.Instance.Heat > 1.1)
        {
            currentState = GameStates.Menu;
            SceneManager.LoadScene("Fire");
        }
    }
    public void LoadMain()
    {
        score = 0;
        currentState = GameStates.Tutorial;
        SceneManager.LoadScene("main");
    }
    void CheckForGameWin()
    {
        if (Time.time > GameStartTime + GameDuration)
        {
            score = ScoreController.Instance.Score;
            currentState = GameStates.Menu;
            SceneManager.LoadScene("Win");
        }
    }
}
