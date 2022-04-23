using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task
{
    public float reputationGain = 0.2f;
    public float reputationLoss = 0.1f;

    public string taskName = "Task";
    public string taskDescription = "Description";
    public float timeLimit = 0f;

    public float deathChanceOnFail = 0.5f;

    private List<ICondition> conditions = new List<ICondition>();
    private float timePassed = 0f;

    public delegate void TaskFail(Task task);
    public event TaskFail OnTaskFail;

    public delegate void TaskSuccess(Task task);
    public event TaskSuccess OnTaskSuccess;

    private GameController gameController;

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

    public float GetRemainingPercentage()
    {
        return 1f - (timePassed / timeLimit);
    }

    bool CheckConditions()
    {
        foreach(var condition in conditions)
            if(!condition.Check())
                return false;

        return true;
    }

    public void AddCondition(ICondition condition) {
        timeLimit += condition.TimeToSolve();
        conditions.Add(condition);
    }

    public ICondition GetCurrentCondition() {
        return conditions[0];
    }

}
