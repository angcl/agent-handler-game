using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task
{
    public float reputationGain = 0.2f;
    public float reputationLoss = 0.1f;

    public string taskName = "Task";
    public string taskDescription = "Description";
    public float timeLimit = 10f;

    private List<ICondition> conditions = new List<ICondition>() { new EverFailing(), new EverSucceeding() };
    private float timePassed = 0f;

    public delegate void TaskFail(Task task);
    public event TaskFail OnTaskFail;

    public delegate void TaskSuccess(Task task);
    public event TaskSuccess OnTaskSuccess;

    public void Update()
    {
        timePassed += Time.deltaTime;
        if (timePassed > timeLimit)
            if (OnTaskFail != null)
                OnTaskFail(this);

        if (CheckConditions())
            if (OnTaskSuccess != null)
                OnTaskSuccess(this);
    }

    bool CheckConditions()
    {
        foreach(var condition in conditions)
            if(!condition.Check())
                return false;

        return true;
    }
}
