using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskManager : MonoBehaviour
{
    public List<Task> tasks = new List<Task>();
    public string[] names = new string[] {};

    private List<Task> tasksToRemove = new List<Task>();
    private GameController gameController;

    public delegate void TasksChanged(List<Task> tasks);
    public event TasksChanged OnTasksChanged;

    public delegate void TaskAdded(Task task);
    public event TaskAdded OnTaskAdded;

    public delegate void TaskRemoved(Task task);
    public event TaskRemoved OnTaskRemoved;    

    private readonly List<ICondition> availableConditions = new List<ICondition> {
        new CameraDeactivated(),
        new EnergyDeactivated(),
        new UploadedVirus(),
        new DownloadedFiles(),
        new VehicleArrived()
    };

    private void Awake()
    {
        gameController = GetComponent<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameController.gameState != EGameState.PLAYING)
            return;

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
            task.GetCurrentCondition().GetObjectToFocus().GetComponent<IClickable>().ResetTask();
            tasks.Remove(task);
            if (OnTaskRemoved != null) {
                OnTaskRemoved(task);
            }
        }

        tasksToRemove.Clear();

        if (OnTasksChanged != null)
            OnTasksChanged(tasks);
    }

    public void ResetTasks()
    {
        foreach(var task in tasks)
        {
            task.GetCurrentCondition().GetObjectToFocus().GetComponent<IClickable>().ResetTask();
            if (OnTaskRemoved != null)
                OnTaskRemoved(task);
        }
        tasks.Clear();

        GeneralClickable[] clickables = GameObject.FindObjectsOfType<GeneralClickable>();
        foreach(var clickable in clickables)
        {
            clickable.Reset();
        }
    }

    public bool GenerateTask()
    {
        var task = new Task();

        // Test random name generation
        task.taskName = "Agent " + names[Random.Range(0, names.Length)];
        task.OnTaskSuccess += HandleSuccess;
        task.OnTaskFail += HandleFail;

        // Todo: Better way of randomization
        ICondition randomCondition = GetRandomConditon();
        if(!randomCondition.Randomize())
        {
            return false;
        }
        
        task.AddCondition(randomCondition);
        tasks.Add(task);
        if (OnTaskAdded != null) {
            OnTaskAdded(task);
        }
        randomCondition.GetObjectToFocus().GetComponent<IClickable>().SetTask(task);

        if (OnTasksChanged != null)
            OnTasksChanged(tasks);

        return true;
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
        HandleTaskEvent(task);
        gameController.TaskFailed(task);
    }

    void HandleSuccess(Task task)
    {
        HandleTaskEvent(task);
        gameController.TaskSucceeded(task);
    }
}
