using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    private TaskManager taskManager;
    private List<GameObject> currentState = new List<GameObject>();

    public GameObject reputationBar;
    public GameObject taskList;
    public GameObject taskPrefab;
    public GameObject toolBar;

    private void Awake()
    {
        taskManager = GetComponent<TaskManager>();
        taskManager.OnTasksChanged += HandleTasksChanged;
    }

    void ClearTaskList()
    {
        if(currentState.Count == 0)
            return;
            
        foreach(var gameObject in currentState)
        {
            Destroy(gameObject);
        }
        
        currentState.Clear();
    }

    private void HandleTasksChanged(List<Task> tasks)
    {
        ClearTaskList();
        
        foreach(var task in tasks)
        {
            GameObject taskObject = Instantiate(taskPrefab, taskList.GetComponent<Transform>());
            currentState.Add(taskObject);
            
            TaskElement taskElement = taskObject.GetComponent<TaskElement>();
            taskElement.task = task;
        }
    }


}
