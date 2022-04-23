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
    
    public GameObject mainMenu;
    public GameObject ingameMenu;
    public GameObject pauseMenu;
    public GameObject taskArea;
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

    void Update() {
        var reputation = gameController.GetReputation();
        if (reputationBar.value != reputation) {
            reputationBar.value = Mathf.Lerp(reputationBar.value, reputation, 2.5f * Time.deltaTime);
        }
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
        
        // reputationBar.value = gameController.GetReputation();
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

    public bool IsContextMenuActive() {
        return contextMenu.activeInHierarchy;
    }

    public bool IsPointInTaskArea(Vector2 point)
    {
        var rectTransform = taskArea.GetComponent<RectTransform>();
        var xMin = taskArea.transform.position.x - rectTransform.rect.width;
        var xMax = taskArea.transform.position.x;
        var yMin = taskArea.transform.position.y - rectTransform.rect.height / 2;
        var yMax = taskArea.transform.position.y + rectTransform.rect.height / 2;

        return ( point.x >= xMin && point.x <= xMax && point.y >= yMin && point.y <= yMax );
    }

    public void ShowMainMenu() {
        mainMenu.SetActive(true);
    }

    public void HideMainMenu() {
        mainMenu.SetActive(false);
    }

    public void ShowIngameMenu() {
        ingameMenu.SetActive(true);
    }

    public void HideIngameMenu() {
        ingameMenu.SetActive(false);
    }

    public void ShowPauseMenu() {
        pauseMenu.SetActive(true);
    }

    public void HidePauseMenu() {
        pauseMenu.SetActive(false);
    }

}
