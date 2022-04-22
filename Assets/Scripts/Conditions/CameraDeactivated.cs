using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraDeactivated : ICondition
{
    CameraClickable cameraClickable;

    public void Randomize()
    {

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
}
