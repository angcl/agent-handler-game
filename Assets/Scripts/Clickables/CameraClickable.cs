using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraClickable : MonoBehaviour, IClickable
{
    public bool isActive { get; private set; } = true;
    private float timePassedSinceDeactivation = 0.0f;
    
    [SerializeField]
    private float timeForReactivation = 10.0f;

    void Update()
    {
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
            gameObject.GetComponent<Renderer>().material.color = Color.green;
            return;
        }

        timePassedSinceDeactivation = 0.0f;
        gameObject.GetComponent<Renderer>().material.color = Color.red;
    }

    public void Run(EContextButton contextButton)
    {
        if (contextButton == EContextButton.DEACTIVATE) {
            SetCameraState(false);
        }

        if (contextButton == EContextButton.ACTIVATE) {
            SetCameraState(true);
        }

        if(contextButton == EContextButton.DOWNLOAD)
        {

        }

        if(contextButton == EContextButton.UPLOAD){

        }
    }

    public EContextButton[] GetContextButtons()
    {
        if(isActive)
        {
            return new EContextButton[] {
                EContextButton.DEACTIVATE,
                EContextButton.MOVE
            };
        }
        else
        {
            return new EContextButton[] {
                EContextButton.ACTIVATE,
                EContextButton.MOVE
            };
        }
    }
}