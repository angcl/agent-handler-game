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

    void Update()
    {
        Vector3 cameraPos = Camera.main.WorldToScreenPoint(Vector3.zero);
        Vector3 iconPosition = Camera.main.WorldToScreenPoint(transform.position);
        bool detached = false;

        Vector3 min = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, cameraPos.z));
        Vector3 max = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, cameraPos.z));

        var currentPosition = currentTaskBar.transform.position;
        if (iconPosition.x < 0.0f)
        {
            detached = true;
            currentTaskBar.transform.SetParent(transform.parent);
            currentPosition.x = min.x + 1.5f;
        }

        if (iconPosition.x > Screen.width)
        {
            detached = true;
            currentTaskBar.transform.SetParent(transform.parent);
            currentPosition.x = max.x - 1.5f;
        }        

        if (iconPosition.y < 0.0f)
        {
            detached = true;
            currentTaskBar.transform.SetParent(transform.parent);
            currentPosition.y = min.y + 1.1f;
            currentPosition.z = min.z + 1.1f;
        }

        if (iconPosition.y > Screen.height)
        {
            detached = true;
            currentTaskBar.transform.SetParent(transform.parent);
            currentPosition.y = max.y - 1.1f;
            currentPosition.z = max.z - 1.1f;
        }
        
        currentTaskBar.transform.position = currentPosition;

        if(detached)
        {
            return;
        }

        if(!detached && currentTaskBar.transform.parent != transform)
        {
            currentTaskBar.transform.position = Vector3.zero;
            currentTaskBar.transform.SetParent(transform);
        }

        /*
        Vector3 cameraPos = Camera.main.WorldToScreenPoint(Vector3.zero);
        Vector3 min = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, cameraPos.z));
        Vector3 max = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, cameraPos.z));

        Vector3 iconPosition = Camera.main.WorldToScreenPoint(transform.position);

        bool detached = false;
        if (min.x > transform.position.x)
        {
            detached = true;
            currentTaskBar.transform.SetParent(transform.parent);
            var currentPosition = currentTaskBar.transform.position;
            currentPosition.x = min.x + 1.5f;
            currentTaskBar.transform.position = currentPosition;
        }

        if (transform.position.x > max.x) 
        {
            detached = true;
            currentTaskBar.transform.SetParent(transform.parent);
            var currentPosition = currentTaskBar.transform.position;
            currentPosition.x = max.x - 1.5f;
            currentTaskBar.transform.position = currentPosition;            
        }

        if (min.y > transform.position.y)
        {
            detached = true;
            currentTaskBar.transform.SetParent(transform.parent);
            var currentPosition = currentTaskBar.transform.position;
            currentPosition.y = min.y + 50.0f;
            currentTaskBar.transform.position = currentPosition;
        }

        /*
        if (transform.position.y > (max.y - transform.position.y / 2)) 
        {
            detached = true;
            currentTaskBar.transform.parent = transform.parent;
            var currentPosition = currentTaskBar.transform.position;
            currentPosition.y = max.y;
            currentTaskBar.transform.position = currentPosition;            
        }
        */

        /*
        if(!detached && currentTaskBar.transform.parent != transform)
        {
            currentTaskBar.transform.SetParent(transform);
        }
        // Debug.Log(max);

        // Bottom edge:
        // Debug.Log(min.y - 2.0f);
        // Debug.Log(min.y - transform.position.y/2);

        // Debug.Log(min.z > 0 ? min.z - 10 : min.z + 10);
        // Debug.Log(Camera.main.orthographicSize * Screen.height);
        */
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
    }

}
