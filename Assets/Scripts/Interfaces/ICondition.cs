using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICondition
{
    void Randomize();
    bool Check();
    float TimeToSolve();
    ICondition Clone();
}
