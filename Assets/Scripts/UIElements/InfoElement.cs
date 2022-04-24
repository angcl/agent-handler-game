using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoElement : MonoBehaviour
{

    [SerializeField]
    private GameObject[] icons;
    [SerializeField]
    private Sprite[] availableIcons;

    [SerializeField]
    private Slider progressBar;
    

    [SerializeField]
    private Slider currentTaskBar;

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

    public void SetTaskIcon(ETaskIcon icon)
    {
        currentTaskBar.gameObject.transform.Find("Icon").GetComponent<Image>().sprite = availableIcons[(int) icon];
    }

    public void SetSelected(bool state)
    {
        if(!state)
        {
            currentTaskBar.value = 1.0f;
            currentTaskBar.gameObject.transform.Find("Fill Area").Find("Fill").GetComponent<Image>().color = ColorHelper.GetColor(ColorHelper.GREEN);
        }

        currentTaskBar.gameObject.SetActive(state);
    }

    public void UpdateTaskState(float value)
    {
        currentTaskBar.value = value;

        if(value <= 0.5f)
        {
            Color color;
            if(value <= 0.25f)
            {
                color = ColorHelper.GetColor(ColorHelper.RED);  
            }
            else
            {
                color = ColorHelper.GetColor(ColorHelper.YELLOW);                
            }
            currentTaskBar.gameObject.transform.Find("Fill Area").Find("Fill").GetComponent<Image>().color = color;
        }
        // green - 00be53ff
        // yellow - ffbe53ff
        // red - ff4c53ff
        // blue - 297bffff
    }

}
