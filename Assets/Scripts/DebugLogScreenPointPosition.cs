using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugLogScreenPointPosition : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 iconPosition = Camera.main.WorldToScreenPoint(transform.position);
        Debug.Log(iconPosition);
    }
}
