using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CameraDeactivated : ICondition
{
    CameraClickable cameraClickable;

    public bool Randomize()
    {
        CameraClickable[] availableCameras = GameObject.FindObjectsOfType<CameraClickable>();
        availableCameras = availableCameras.AsQueryable().Where(c => c.isActive).ToArray();
        if(availableCameras.Length == 0){
            return false;
        }
        cameraClickable = availableCameras[Random.Range(0, availableCameras.Length)];
        return true;
    }

    public bool Check()
    {
        return !cameraClickable.isActive;
    }

    public float TimeToSolve() {
        return 10.0f;
    }

    public ICondition Clone() {
        return (ICondition) this.MemberwiseClone();
    }
    
    public GameObject GetObjectToFocus() {
        return cameraClickable.gameObject;
    }

    public ETaskIcon GetTaskIcon(){
        return ETaskIcon.CAMERA;
    }
}
