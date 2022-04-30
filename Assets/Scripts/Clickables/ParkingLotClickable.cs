using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParkingLotClickable : GeneralClickable
{
    public bool isArrived { get; private set; } = false;
    public float timeForVehicleToArrive { get; private set; } = 7.5f;
    private float timePassedSinceVehicleCall = 0.0f;

    public bool isWaitingForArrival { get; private set; } = false;
    
    private float timeForReactivation = 6.5f;
    private float timePassedSinceArrival = 0.0f;

    void Update()
    {
        if(HasTask())
        {
            infoElement.UpdateTaskState(task.GetRemainingPercentage());
        }
        if (isArrived)
        {
            timePassedSinceArrival += Time.deltaTime;
            if (timePassedSinceArrival >= timeForReactivation)
            {
                SetArrivalState(false);
                timePassedSinceArrival = 0.0f;
            }
            return;
        }

        if(isWaitingForArrival)
        {
            timePassedSinceVehicleCall += Time.deltaTime;
            infoElement.UpdateProgress(timePassedSinceVehicleCall / timeForVehicleToArrive);            
            if(timePassedSinceVehicleCall >= timeForVehicleToArrive)
            {
                SetArrivalState(true);
                timePassedSinceVehicleCall = 0.0f;
            }
        }
    }

    private void SetArrivalState(bool state) 
    {
        isArrived = state;
        if (isArrived) {
            infoElement.ShowIcon(EInfoIcon.ARRIVED);
            infoElement.SetBarState(false, Color.cyan);

            timePassedSinceArrival = 0.0f;
            isWaitingForArrival = false;
            return;
        }

        infoElement.HideIcon(EInfoIcon.ARRIVED);
        timePassedSinceArrival = 0.0f;
    }
    
    public override void Reset()
    {
        SetArrivalState(false);

        isWaitingForArrival = false;
        timePassedSinceArrival = 0.0f;

        infoElement.SetBarState(false, Color.cyan);
    }

    public override void Run(EContextButton contextButton)
    {
        if (isWaitingForArrival)
            return;

        if (contextButton == EContextButton.CALL) {
            isWaitingForArrival = true;
            infoElement.SetBarState(true, ColorHelper.GetColor(ColorHelper.BLUE));
        }   
    }

    public override EContextButton[] GetContextButtons()
    {
        return new EContextButton[] {
            EContextButton.CALL
        };
    }
}
