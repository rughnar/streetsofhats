using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public float tiempoDeJuegoReal = 0;
    public int score = 0;
    public float multiplier = 1;
    [SerializeField] private GameObject loseScreen;
    [SerializeField] private GameObject winScreen;
    [SerializeField] private GameObject pauseScreen;

    [SerializeField] private AudioClip winSound;
    [SerializeField] private AudioClip loseSound;
    [SerializeField] Transform spawnTransform;
    public KeyCode resetKey = KeyCode.R;
    public KeyCode pauseKey = KeyCode.P;
    public KeyCode alternativeResumeKey = KeyCode.Escape;
    public int firstLevelBuildIndex = 4;
    private bool gameEnded = false;
    private bool gamePaused = false;
    private int sceneIndexToLoadIfReset;
    private AudioManager audioManager;

    private EnemyManager enemyManager;
    private PlayerMovement playerMovement;
    private PlayerController playerController;
    private UIController uIController;


    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        sceneIndexToLoadIfReset = 1;
    }

    void Awake()
    {
        enemyManager = FindObjectOfType<EnemyManager>();
        playerMovement = FindObjectOfType<PlayerMovement>();
        playerController = FindObjectOfType<PlayerController>();
        uIController = FindObjectOfType<UIController>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(alternativeResumeKey)) if (gamePaused) Resume();

        if (Input.GetKeyDown(pauseKey))
        {
            if (!gamePaused) Pause();
            else Resume();
        }

        if (Input.GetKeyDown(resetKey))
        {
            if (gameEnded) SceneManager.LoadScene(sceneIndexToLoadIfReset, LoadSceneMode.Single);
        }
        tiempoDeJuegoReal += Time.deltaTime;

    }

    public void LoseGame()
    {
        Time.timeScale = 0;
        sceneIndexToLoadIfReset = SceneManager.GetActiveScene().buildIndex;
        Score();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

        //StartCoroutine(ShowLoseScreen(1.5f));
        //soundManager.PlaySFX(loseSound);

    }

    public IEnumerator ShowLoseScreen(float secondsToWait)
    {
        yield return new WaitForSecondsRealtime(secondsToWait);
        loseScreen.SetActive(true);
        gameEnded = true;
    }

    public void EndLevel()
    {
        if (SceneManager.sceneCountInBuildSettings - 1 != SceneManager.GetActiveScene().buildIndex)
        {

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else
        {
            WinGame();
        }
    }

    public void WinGame()
    {
        //   playerBehaviourController.Celebrate();

        winScreen.SetActive(true);
        gameEnded = true;
        sceneIndexToLoadIfReset = 0;
        Time.timeScale = 0;
        //soundManager.PlaySFX(winSound);
    }

    public void Pause()
    {
        Time.timeScale = 0;
        pauseScreen.SetActive(true);
        gamePaused = true;
        playerMovement.enabled = false;
        playerController.enabled = false;
    }

    public void Resume()
    {
        Time.timeScale = 1;
        pauseScreen.SetActive(false);
        gamePaused = false;
        playerMovement.enabled = true;
        playerController.enabled = true;
    }

    public void BackToMainMenu() { SceneManager.LoadScene(0); }


    public Transform GetSpawnPoint() { return spawnTransform; }

    private void Score()
    {
        //Formula: (6000 * cantidad de segundos ) - 70 * cantidad de obst�culos chocados
        string score = ((60 * (int)tiempoDeJuegoReal) - 26 * enemyManager.quantityEnemiesDestroyed).ToString();
        PlayerPrefs.SetString("score", score);
        PlayerPrefs.Save();
    }

    public void AddToScore(int scorePoints)
    {
        score += (int)(scorePoints * multiplier);
        uIController.SetScoreSilently(score);
    }

    public void AddToMultiplier(float multiplierBonus)
    {
        multiplier += multiplierBonus;
        multiplier = Mathf.Round(multiplier * 10f) / 10f;
        uIController.IncreaseMultiplier(multiplier);
    }

    public void ReduceMultiplierTo1()
    {
        multiplier = 1;
        uIController.IncreaseMultiplier(1);
    }
}

