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
    }

    public void TaskFailed(Task task)
    {
        reputation = Mathf.Max(0.0f, reputation - task.reputationLoss);
        numTasksFailed++;
    }

    public void TaskSucceeded(Task task)
    {
        reputation = Mathf.Min(1.0f, reputation + task.reputationGain);
        numTasksCompleted++;
    }
}
