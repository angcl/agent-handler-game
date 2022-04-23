using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoElement : MonoBehaviour
{

    [SerializeField]
    private GameObject[] icons;

    [SerializeField]
    private Slider progressBar;
    
    void Awake()
    {
        progressBar.gameObject.SetActive(false);

        foreach(var icon in icons)
        {
            icon.SetActive(false);
        }
    }

    public void ShowIcon(EInfoIcon icon)
    {
        icons[(int) icon].SetActive(true);
    }

    public void HideIcon(EInfoIcon icon)
    {
        icons[(int) icon].SetActive(false);
    }

    public void SetBarState(bool state, Color color)
    {
        if(!state)
            progressBar.value = 0.0f;

        progressBar.gameObject.SetActive(state);
        progressBar.gameObject.transform.Find("Fill Area").Find("Fill").GetComponent<Image>().color = color;
    }

    public void UpdateProgress(float value)
    {
        if(value >= 0.0f && value <= 1.0f)
        {
            progressBar.value = value;
        }
    }

}
