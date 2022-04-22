using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskManager : MonoBehaviour
{
    public List<Task> tasks = new List<Task>();
    public string[] names = new string[] { };

    private List<Task> tasksToRemove = new List<Task>();
    private GameController gameController;

    public delegate void TasksChanged(List<Task> tasks);
    public event TasksChanged OnTasksChanged;

    private readonly List<ICondition> availableConditions = new List<ICondition> {
        new EverFailing(),
        new EverSucceeding()
    };

    private void Awake()
    {
        gameController = GetComponent<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        CleanupTasks();

        foreach(var task in tasks)
            task.Update();
    }

    void CleanupTasks()
    {
        if (tasksToRemove.Count == 0)
            return;

        foreach(var task in tasksToRemove)
        {
            tasks.Remove(task);
        }

        tasksToRemove.Clear();

        if (OnTasksChanged != null)
            OnTasksChanged(tasks);
    }

    public void GenerateTask()
    {
        var task = new Task();

        // Test random name generation
        task.taskName = "Agent " + names[Random.Range(0, names.Length)];
        task.OnTaskSuccess += HandleSuccess;
        task.OnTaskFail += HandleFail;

        task.AddCondition(GetRandomConditon());
        tasks.Add(task);

        if (OnTasksChanged != null)
            OnTasksChanged(tasks);
    }
    
    private ICondition GetRandomConditon() {
        return availableConditions[Random.Range(0, availableConditions.Count)].Clone();
    }

    void HandleTaskEvent(Task task)
    {
        task.OnTaskSuccess -= HandleSuccess;
        task.OnTaskFail -= HandleFail;
        tasksToRemove.Add(task);
    }

    void HandleFail(Task task)
    {
        Debug.Log("Task failed: " + task.taskName);
        HandleTaskEvent(task);
        gameController.TaskFailed(task);
    }

    void HandleSuccess(Task task)
    {
        Debug.Log("Task succeeded: " + task.taskName);
        HandleTaskEvent(task);
        gameController.TaskSucceeded(task);
    }
}
