using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EverFailing : ICondition
{

    public void Randomize()
    {

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
}
