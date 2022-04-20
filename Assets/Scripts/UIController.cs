using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    private TaskManager taskManager;
    public GameObject reputationBar;
    public GameObject taskList;
    public GameObject taskPrefab;
    public GameObject toolBar;

    private void Awake()
    {
        taskManager = GetComponent<TaskManager>();
        taskManager.OnTasksChanged += HandleTasksChanged;
    }

    private void HandleTasksChanged(List<Task> tasks)
    {

    }


}
