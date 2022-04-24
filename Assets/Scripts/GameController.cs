using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class GameController : MonoBehaviour
{
    public EGameState gameState { get; private set; } = EGameState.MAIN_MENU;

    private Highscore highscore;
    private float timeSurvived = 0;
    private int numTasksFailed = 0;
    private int numTasksCompleted = 0;

    private float reputation = 1.0f;
    private UIController uiController;
    private TaskManager taskManager;

    public float generateTaskAfterTime = 20.0f;
    private float timePassedForTaskGeneration = 0.0f;
    
    [SerializeField]
    private float minGenerationTime = 5.0f;
    
    [SerializeField]
    private float maxGenerationTime = 30.0f;

    private AudioManager audioManager;

    private void Awake()
    {
        uiController = GetComponent<UIController>();
        taskManager = GetComponent<TaskManager>();
        highscore = LoadHighscoreFromJson();
        uiController.SetHighscore(highscore);
        audioManager = GetComponent<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameState == EGameState.LOST) {
            if (Input.GetKeyDown(KeyCode.Q)) {
                Application.Quit();
                return;
            }

            if (Input.GetKeyDown(KeyCode.R)) {
                Restart();
                return;
            }

            return;
        }

        if (gameState == EGameState.MAIN_MENU && Input.anyKeyDown) {
            if (Input.GetKeyDown(KeyCode.Q)) {
                Application.Quit();
                return;
            }
            uiController.SetMainMenuMenuActive(false);
            uiController.SetInGameMenuActive(true);
            gameState = EGameState.PLAYING;
        }
        
        if (gameState == EGameState.PAUSED) {
            if (Input.GetKeyDown(KeyCode.R)) {
                Restart();
                return;
            }

            if (Input.GetKeyDown(KeyCode.Q)) {
                Application.Quit();
                return;
            }   

            if (Input.anyKeyDown) {
                uiController.SetPauseMenuActive(false);
                uiController.SetInGameMenuActive(true);

                Time.timeScale = 1.0f;

                gameState = EGameState.PLAYING;
                return;                
            }
        }

        if (reputation <= 0.0f) {
            gameState = EGameState.LOST;

            Time.timeScale = 0.0f;
            uiController.SetLostScreenStats(highscore, new Highscore {
                timeSurvived = timeSurvived,
                numTasksCompleted = numTasksCompleted,
                numTasksFailed = numTasksFailed
            });

            if (highscore.timeSurvived < timeSurvived) {
                highscore.timeSurvived = timeSurvived;
                highscore.numTasksCompleted = numTasksCompleted;
                highscore.numTasksFailed = numTasksFailed;

                SaveHighscoreToJson();
            }

            uiController.HideContextMenu();
            uiController.SetInGameMenuActive(false);
            uiController.SetLostMenuActive(true);

            Resources.UnloadUnusedAssets();
            GC.Collect();
        }

        if (gameState == EGameState.PLAYING)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                gameState = EGameState.PAUSED;

                Time.timeScale = 0.0f;

                uiController.HideContextMenu();
                uiController.SetInGameMenuActive(false);
                uiController.SetPauseMenuActive(true);
                
                return;
            }

            timeSurvived += Time.deltaTime;
            timePassedForTaskGeneration += Time.deltaTime;
            if (timePassedForTaskGeneration > generateTaskAfterTime) {
                timePassedForTaskGeneration = 0.0f;
                taskManager.GenerateTask();
                audioManager.PlayAudioClip(EAudioClip.UI_NEW_TASK);
            }

            GenerateTaskIfEmpty();
        }
    }

    public void TaskFailed(Task task)
    {
        reputation = Mathf.Max(0.0f, reputation - task.reputationLoss);
        numTasksFailed++;
        generateTaskAfterTime = Mathf.Min(maxGenerationTime, generateTaskAfterTime + 0.2f);
    }

    public void TaskSucceeded(Task task)
    {
        reputation = Mathf.Min(1.0f, reputation + task.reputationGain);
        numTasksCompleted++;
        generateTaskAfterTime = Mathf.Max(minGenerationTime, generateTaskAfterTime - 0.1f);
    }
    
    public float GetReputation()
    {
        return reputation;
    }

    private void GenerateTaskIfEmpty() {
        if(taskManager.tasks.Count == 0)
        {
            taskManager.GenerateTask();
            audioManager.PlayAudioClip(EAudioClip.UI_NEW_TASK);
            timePassedForTaskGeneration = 0.0f;
        }
    }

    private Highscore LoadHighscoreFromJson()
    {
        var path = Application.persistentDataPath + "\\highscore.json";
        if (!File.Exists(path)) {
            return new Highscore() {
                timeSurvived = 0.0f,
                numTasksCompleted = 0,
                numTasksFailed = 0
            };
        }

        var json = File.ReadAllText(path, System.Text.Encoding.UTF8);
        return JsonUtility.FromJson<Highscore>(json);
    }

    private void SaveHighscoreToJson()
    {
        var path = Application.persistentDataPath + "\\highscore.json";
        File.WriteAllText(path, JsonUtility.ToJson(highscore));
    }

    private void Restart() {
        reputation = 1.0f;
        timeSurvived = 0.0f;
        numTasksCompleted = 0;
        numTasksFailed = 0;

        taskManager.ResetTasks();

        gameState = EGameState.PLAYING;
        uiController.SetLostMenuActive(false);
        uiController.SetPauseMenuActive(false);
        uiController.HideContextMenu();
        uiController.SetInGameMenuActive(true);
        Time.timeScale = 1.0f;        
    }
}
