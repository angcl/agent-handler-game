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

    // Update is called once per frame
    void Update()
    {
        if(task != null)
        {
            nameText.text = task.taskName;
            timeBar.value = (1f - task.GetRemainingPercentage());
        }
    }

}
