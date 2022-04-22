using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IClickable
{
    void Run(EContextButton contextButton);
    EContextButton[] GetContextButtons();
}