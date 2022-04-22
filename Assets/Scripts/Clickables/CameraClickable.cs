using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraClickable : MonoBehaviour, IClickable
{
    public bool isActive { get; private set; } = true;

    public void Run(EContextButton contextButton)
    {
        if (contextButton == EContextButton.DEACTIVATE) {
            isActive = false;
            gameObject.GetComponent<Renderer>().material.color = Color.red;
        }

        if (contextButton == EContextButton.ACTIVATE) {
            isActive = true;
            gameObject.GetComponent<Renderer>().material.color = Color.green;
        }
    }

    public EContextButton[] GetContextButtons()
    {
        return new EContextButton[] {
            EContextButton.DEACTIVATE,
            EContextButton.ACTIVATE,
            EContextButton.CALL
        };
    }
}