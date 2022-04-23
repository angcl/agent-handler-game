using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorldSpaceCanvasManager : MonoBehaviour
{

    [SerializeField]
    private GameObject infoPrefab;
    
    public GameObject CreateElement(Vector3 position)
    {   
        GameObject gameObject = Instantiate(infoPrefab, transform);

        gameObject.transform.position = position;

        return gameObject;
    }
}
