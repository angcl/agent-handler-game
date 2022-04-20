using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskManager : MonoBehaviour
{
    public List<Task> tasks = new List<Task>();
    private List<Task> tasksToRemove = new List<Task>();
    private GameController gameController;

    public delegate void TasksChanged(List<Task> tasks);
    public event TasksChanged OnTasksChanged;

    private void Awake()
    {
        gameController = GetComponent<GameController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        GenerateTask();
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
    }

    void GenerateTask()
    {
        var task = new Task();
        task.OnTaskSuccess += HandleSuccess;
        task.OnTaskFail += HandleFail;
        tasks.Add(task);
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
