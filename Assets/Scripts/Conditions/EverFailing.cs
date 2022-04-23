using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EverFailing : ICondition
{

    public bool Randomize()
    {
        return true;
    }

    public bool Check()
    {
        return false;
    }

    public float TimeToSolve()
    {
        return 1.0f;
    }

    public ICondition Clone()
    {
        return (ICondition) this.MemberwiseClone();
    }

    public GameObject GetObjectToFocus() {
        return null;
    }

    public ETaskIcon GetTaskIcon() {
        return ETaskIcon.CAMERA;
    }    
}
