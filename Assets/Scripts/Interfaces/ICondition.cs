using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICondition
{
    bool Randomize();
    bool Check();
    float TimeToSolve();
    ICondition Clone();
    GameObject GetObjectToFocus();
    ETaskIcon GetTaskIcon();
}
