using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    private TaskManager taskManager;
    private GameController gameController;
    
    private List<GameObject> currentState = new List<GameObject>();

    public Slider reputationBar;
    
    public GameObject taskList;
    public GameObject taskPrefab;
    public GameObject toolBar;

    public GameObject contextMenu;

    private void Awake()
    {
        taskManager = GetComponent<TaskManager>();
        gameController = GetComponent<GameController>();
        taskManager.OnTasksChanged += HandleTasksChanged;
        var cameraController = Camera.main.GetComponent<CameraController>();
        cameraController.OnCameraMoved += HideContextMenu;
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
        
        reputationBar.value = gameController.GetReputation();
    }

    public void ShowContextMenu(Vector3 coordinates, IClickable clickable)
    {
        coordinates.z = 0;

        contextMenu.SetActive(true);
        contextMenu.transform.position = coordinates;

        contextMenu.GetComponent<ContextMenu>().SetClickable(clickable);
    }

    public void HideContextMenu() {
        contextMenu.SetActive(false);
    }

}
