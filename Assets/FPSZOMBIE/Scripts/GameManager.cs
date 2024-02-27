using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.SceneManagement;
using UnityEngine.XR.ARFoundation;

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

    public UnityEvent OnGameStart, OnRoundStart, OnRoundEnd, OnGameOver;

    //Game States (essentially gamemodes)
    GameState_Base currentState;
    public GameState_Start state_Start = new GameState_Start();
    public GameState_Timed state_Timed = new GameState_Timed();
    public GameState_End state_End = new GameState_End();

    //References
    public NewZombieManager zombieManager;
    public Player_FPS player;

    /// UI
    public GameObject startButton;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI waveText;

    public ARPlaneManager arPlaneManager;
    public XROrigin xrOrigin;

    public GameObject shopPrefab;

    //Variables
    public int wave = 1;
    public float waveTime = 30f;
    public float score;
    public float roundDelay = 15f;

    Coroutine currentCoroutine;

    #region States
    private void Start()
    {
        currentState = state_Start;
        currentState.EnterState(this);
        waveText.text = "Wave " + wave;
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
            case "END":
                currentState = state_End;
                break;
            default:
                Debug.LogError("INVALID STATE: " + newState);
                break;
        }
        currentState.EnterState(this);
    }

    #endregion states

    public void StartGame()
    {
        if(xrOrigin.transform.GetChild(1).childCount > 0)
            ChangeState("TIMED");
    }

    public void PauseGame()
    {

    }

    public void ResetGame()
    {
        SceneManager.LoadScene(0);
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
        zombieManager.StopSpawningZombies(true);
        OnRoundEnd.Invoke();
        player.SwitchState("HAND");
        GameObject siopa = Instantiate(shopPrefab, xrOrigin.transform.GetChild(0).position, Quaternion.identity);
        yield return new WaitForSecondsRealtime(roundDelay);
        Destroy(siopa);
        player.ReturnToCurrentWeaponState();
        wave++;
        waveText.text = "Wave " + wave;
        currentState.NextWave(this);
        zombieManager.StartSpawningZombies();
        OnRoundStart.Invoke();
    }

    public void CallCoroutine(IEnumerator routine)
    {
        currentCoroutine = StartCoroutine(routine);
    }
    public void StopCurrentCoroutine()
    {
        StopCoroutine(currentCoroutine);
    }

}
