using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CameraDeactivated : ICondition
{
    CameraClickable cameraClickable;

    public bool Randomize()
    {
        CameraClickable[] allCameras = GameObject.FindObjectsOfType<CameraClickable>();

        List<CameraClickable> availableCameras = new List<CameraClickable>();

        foreach (var c in allCameras)
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
        return 10.0f;
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
