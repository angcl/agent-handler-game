using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CameraDeactivated : ICondition
{
    private ResourceManager resourceManager;
    CameraClickable cameraClickable;

    public bool Randomize()
    {
        if (resourceManager == null)
            resourceManager = GameObject.FindObjectOfType<ResourceManager>();
        
        List<CameraClickable> availableCameras = new List<CameraClickable>();
        foreach (var c in resourceManager.cameras)
        {
            if (c.isActive && !c.HasTask())
                availableCameras.Add(c);
        }

        if(availableCameras.Count == 0){
            return false;
        }

        cameraClickable = availableCameras[Random.Range(0, availableCameras.Count)];
        return true;
    }

    public bool Check()
    {
        return !cameraClickable.isActive;
    }

    public float TimeToSolve() 
    {
        return 8.5f;
    }

    public float ReputationLoss()
    {
        return 0.15f;
    }

    public ICondition Clone() 
    {
        return (ICondition) this.MemberwiseClone();
    }
    
    public GameObject GetObjectToFocus() 
    {
        return cameraClickable.gameObject;
    }

    public ETaskIcon GetTaskIcon()
    {
        return ETaskIcon.CAMERA;
    }
}
