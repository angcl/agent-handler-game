using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IClickable
{
    void Run(EContextButton contextButton);

    void SetTask(Task task);

    void ResetTask();

    bool HasTask();

    void Reset();

    EContextButton[] GetContextButtons();
}