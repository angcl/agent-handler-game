using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraClickable : GeneralClickable
{
    public bool isActive { get; private set; } = true;
    private float timePassedSinceDeactivation = 0.0f;

    private float timeForReactivation = 7.5f;
    
    void Update()
    {
        if(HasTask())
        {
            infoElement.UpdateTaskState(task.GetRemainingPercentage());
        }
        if(!isActive)
        {
            timePassedSinceDeactivation += Time.deltaTime;
            if(timePassedSinceDeactivation >= timeForReactivation)
            {
                SetCameraState(true);
                timePassedSinceDeactivation = 0.0f;
            }    
        }
    }

    private void SetCameraState(bool active) 
    {
        isActive = active;
        if (isActive) {
            return;
        }

        timePassedSinceDeactivation = 0.0f;
    }

    public override void Run(EContextButton contextButton)
    {
        if (contextButton == EContextButton.DEACTIVATE)
        {
            SetCameraState(false);
        }

        if (contextButton == EContextButton.ACTIVATE)
        {
            SetCameraState(true);
        }
    }

    public override void Reset()
    {
        SetCameraState(true);

        timePassedSinceDeactivation = 0.0f;
    }

    public override EContextButton[] GetContextButtons()
    {
        if(isActive)
        {
            return new EContextButton[] {
                EContextButton.DEACTIVATE
            };
        }
        else
        {
            return new EContextButton[] {
                EContextButton.ACTIVATE
            };
        }
    }
}