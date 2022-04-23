using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public EGameState gameState { get; private set; } = EGameState.MAIN_MENU;
    private float timeSurvived = 0;
    private int numAgentsDied = 0;
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

    private void Awake()
    {
        uiController = GetComponent<UIController>();
        taskManager = GetComponent<TaskManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameState == EGameState.MAIN_MENU && Input.anyKeyDown) {
            uiController.HideMainMenu();
            uiController.ShowIngameMenu();
            gameState = EGameState.PLAYING;
        }
        
        if (gameState == EGameState.PAUSED) {
            if (Input.anyKeyDown) {
                uiController.HidePauseMenu();
                uiController.ShowIngameMenu();

                Time.timeScale = 1.0f;

                gameState = EGameState.PLAYING;
                return;                
            }
        }

        if (gameState == EGameState.PLAYING)
        {
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                gameState = EGameState.PAUSED;

                Time.timeScale = 0.0f;

                uiController.HideContextMenu();
                uiController.HideIngameMenu();
                uiController.ShowPauseMenu();
                
                return;
            }

            timeSurvived += Time.deltaTime;
            timePassedForTaskGeneration += Time.deltaTime;
            if (timePassedForTaskGeneration > generateTaskAfterTime) {
                timePassedForTaskGeneration = 0.0f;
                taskManager.GenerateTask();
            }

            GenerateTaskIfEmpty();
        }
    }

    public void TaskFailed(Task task)
    {
        reputation = Mathf.Max(0.0f, reputation - task.reputationLoss);
        numTasksFailed++;
        if (task.deathChanceOnFail > 0f 
            && Random.Range(0f, 1f) <= task.deathChanceOnFail)
        {
            // TODO Agent Death animation
            numAgentsDied++;
        }

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
            timePassedForTaskGeneration = 0.0f;
        }
    }
}
