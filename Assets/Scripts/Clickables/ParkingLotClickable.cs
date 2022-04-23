using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParkingLotClickable : MonoBehaviour, IClickable
{
    public bool isArrived { get; private set; } = false;

    [SerializeField]
    public float timeForVehicleToArrive { get; private set; } = 10.0f;
    private float timePassedSinceVehicleCall = 0.0f;

    public bool isWaitingForArrival { get; private set; } = false;
    
    [SerializeField]
    private float timeForReactivation = 10.0f;
    private float timePassedSinceArrival = 0.0f;

    private WorldSpaceCanvasManager worldSpaceCanvasManager;

    private InfoElement infoElement;
    [SerializeField]
    private float offsetY = 4.0f;

    void Awake()
    {
        worldSpaceCanvasManager = GameObject.FindObjectOfType<WorldSpaceCanvasManager>();
        var collider = gameObject.GetComponent<BoxCollider>();
        var position = new Vector3(transform.position.x, collider.bounds.max.y + offsetY, transform.position.z);
        infoElement = worldSpaceCanvasManager.CreateElement(position).GetComponent<InfoElement>();
    }

    void Update()
    {
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
    
    public void Run(EContextButton contextButton)
    {
        if (isWaitingForArrival)
            return;

        if (contextButton == EContextButton.CALL) {
            isWaitingForArrival = true;
            infoElement.SetBarState(true, Color.cyan);
        }   
    }

    public EContextButton[] GetContextButtons()
    {
        return new EContextButton[] {
            EContextButton.CALL
        };
    }
}
