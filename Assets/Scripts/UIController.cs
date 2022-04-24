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
    
    // Todo: More beautiful and efficient solution to this
    public GameObject mainMenu;
    public TMPro.TMP_Text highScoreLabel;
    public TMPro.TMP_Text scoreLabel;
    public GameObject ingameMenu;
    public GameObject pauseMenu;
    public GameObject lostMenu;
    public GameObject taskArea;
    public GameObject taskList;
    public GameObject taskPrefab;
    public GameObject toolBar;

    public GameObject contextMenu;

    private AudioSource audioSource;

    private void Awake()
    {

        taskManager = GetComponent<TaskManager>();
        gameController = GetComponent<GameController>();
        taskManager.OnTasksChanged += HandleTasksChanged;
        var cameraController = Camera.main.GetComponent<CameraController>();
        cameraController.OnCameraMoved += HideContextMenu;
        audioSource = GetComponent<AudioSource>();
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

    public void HideContextMenu() 
    {
        contextMenu.SetActive(false);
    }

    public bool IsContextMenuActive() 
    {
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

    public void SetMainMenuMenuActive(bool state)
    {
        mainMenu.SetActive(state);
    }

    public void SetInGameMenuActive(bool state)
    {
        ingameMenu.SetActive(state);
    }

    public void SetPauseMenuActive(bool state)
    {
        pauseMenu.SetActive(state);
    }

    public void SetLostMenuActive(bool state)
    {
        lostMenu.SetActive(state);
    }

    public void SetHighscore(Highscore highscore)
    {
        if(highscore.timeSurvived <= 0.0f)
        {
            highScoreLabel.text = "No highscore available";
            return;
        }
        highScoreLabel.text = string.Format("{0:f2} seconds survived with {1} tasks completed", highscore.timeSurvived, highscore.numTasksCompleted);
    }

    public void SetLostScreenStats(Highscore highscore, Highscore current)
    {
        scoreLabel.text = string.Format("{0:f2} seconds and completed {1} tasks", current.timeSurvived, current.numTasksCompleted);
    }    

}
