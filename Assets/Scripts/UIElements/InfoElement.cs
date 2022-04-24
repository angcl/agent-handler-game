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
            Color color;
            ColorUtility.TryParseHtmlString("#00be53ff", out color);
            currentTaskBar.gameObject.transform.Find("Fill Area").Find("Fill").GetComponent<Image>().color = color;
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
                ColorUtility.TryParseHtmlString("#ff4c53ff", out color);    
            }
            else
            {
                ColorUtility.TryParseHtmlString("#ffbe53ff", out color);
            }
            currentTaskBar.gameObject.transform.Find("Fill Area").Find("Fill").GetComponent<Image>().color = color;
        }
        // green - 00be53ff
        // yellow - ffbe53ff
        // red - ff4c53ff
    }

}
