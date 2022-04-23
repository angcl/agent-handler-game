using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TaskElement : MonoBehaviour
{
    public Task task;
    public TextMeshProUGUI nameText;
    public Slider timeBar;
    public Image icon;
    public Sprite[] availableIcons;

    // Update is called once per frame
    void Update()
    {
        if(task != null)
        {
            nameText.text = task.taskName;
            timeBar.value = task.GetRemainingPercentage();
            ICondition condition = task.GetCurrentCondition();
            icon.GetComponent<Image>().sprite = availableIcons[(int) condition.GetTaskIcon()];
        }
    }

    public void Focus()
    {
        var condition = task.GetCurrentCondition();

        var objectToFocus = condition.GetObjectToFocus();
        if (objectToFocus != null) {
            Debug.Log(task.GetCurrentCondition());
            Camera.main.GetComponent<CameraController>().objectToFocus = objectToFocus;
        }
    }
}
