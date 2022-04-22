using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private float timeSurvived = 0;
    private int numAgentsDied = 0;
    private int numTasksFailed = 0;
    private int numTasksCompleted = 0;

    private float reputation = 1.0f;
    private TaskManager taskManager;

    public float generateTaskAfterTime = 20.0f;
    private float timePassedForTaskGeneration = 0.0f;
    
    [SerializeField]
    private float minGenerationTime = 5.0f;
    
    [SerializeField]
    private float maxGenerationTime = 30.0f;

    private void Awake()
    {
        taskManager = GetComponent<TaskManager>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        timeSurvived += Time.deltaTime;
        timePassedForTaskGeneration += Time.deltaTime;
        if (timePassedForTaskGeneration > generateTaskAfterTime) {
            timePassedForTaskGeneration = 0.0f;
            taskManager.GenerateTask();
        }

        GenerateTaskIfEmpty();
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
        if (task.deathChanceOnSuccess > 0f
            && Random.Range(0f, 1f) <= task.deathChanceOnSuccess)
        {
            // TODO Agent Death animation
            numAgentsDied++;
        }

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
