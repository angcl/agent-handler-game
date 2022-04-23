using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FadeAnimation : MonoBehaviour
{
    private TMP_Text text;

    private bool shouldFadeIn = false;
    private float fadeLevel = 1.0f;

    void Awake() {
        text = this.GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        var currentColor = text.color;
        
        if (shouldFadeIn) {
            currentColor.a += 0.5f * Time.deltaTime;
            if (currentColor.a >= 1.0f) {
                shouldFadeIn = false;
            }
        } else {
            currentColor.a -= 0.5f * Time.deltaTime;
            if (currentColor.a <= 0.2f) {
                shouldFadeIn = true;
            }            
        }
        
        text.color = currentColor;
    }
}
