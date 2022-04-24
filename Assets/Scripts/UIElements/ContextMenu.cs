using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContextMenu : MonoBehaviour
{

    public IClickable clickable;
    public GameObject[] buttons;
    private AudioManager audioManager;
    
    void Awake()
    {
        audioManager = FindObjectOfType<AudioManager>();

        for(int i = 0; i < buttons.Length; i++)
        {
            var contextIndex = i;
            var button = buttons[contextIndex];
            
            button.SetActive(false);
            button.GetComponent<Button>().onClick.AddListener(() => {
                CallClickable((EContextButton) contextIndex);
            });
        }
    }

    private void CallClickable(EContextButton contextButton)
    {
        if(clickable == null)
            return;

        audioManager.PlayAudioClip(EAudioClip.UI_CLICK);
        clickable.Run(contextButton);
    }

    public void SetClickable(IClickable clickable) {
        this.clickable = clickable;

        foreach(var button in buttons)
        {
            button.SetActive(false);
        }
        
        foreach(var context in clickable.GetContextButtons())
        {
            buttons[(int) context].SetActive(true);
        }
    }
}
