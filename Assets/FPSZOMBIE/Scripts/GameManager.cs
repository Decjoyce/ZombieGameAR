using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one instance of GameManager Found");
            return;
        }
        instance = this;
    }

    //Game States (essentially gamemodes)
    GameState_Base currentState;
    public GameState_Start state_Start = new GameState_Start();
    public GameState_Timed state_Timed = new GameState_Timed();

    //References
    public ZombieManager waveManager;

    public TextMeshProUGUI timerText;

    //Variables
    public int wave = 1;
    public float score;
    public float roundDelay = 15f;

    #region States
    private void Start()
    {
        currentState = state_Start;
        currentState.EnterState(this);
    }

    private void Update()
    {
        currentState.FrameUpdate(this);
    }

    private void FixedUpdate()
    {
        currentState.PhysicsUpdate(this);
    }

    public void ChangeState(string newState)
    {
        currentState.ExitState(this);
        switch (newState)
        {
            case "START":
                currentState = state_Start;
                break;
            case "TIMED":
                currentState = state_Timed;
                break;
            default:
                Debug.LogError("INVALID STATE: " + newState);
                break;
        }
        currentState.EnterState(this);
    }

    #endregion states

    public void PauseGame()
    {

    }

    public void IncreaseScore(float amount)
    {
        score += amount;
    }

    public void NextWave()
    {
        StartCoroutine(RoundDelay());
    }

    IEnumerator RoundDelay()
    {
        waveManager.StopSpawningZombies();
        yield return new WaitForSecondsRealtime(roundDelay);
        wave++;
        currentState.NextWave(this);
        waveManager.StartSpawningZombies();
        waveManager.CalculateNumberZombies();
    }

    public void CallCoroutine(IEnumerator routine)
    {
        StartCoroutine(routine);
    }

}
