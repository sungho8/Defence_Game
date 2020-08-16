using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Time_Pause : MonoBehaviour
{
    public Sprite pause;
    public Sprite unpause;

    Controller_Time ct;
    SpriteRenderer spriteRenderer;
    void Start()
    {
        ct = GameObject.Find("TimeController").GetComponent<Controller_Time>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void OnMouseUp()
    {
        // pause
        if(ct.isPause == false)
        {
            ct.isPause = true;
            Time.timeScale = 0f;
            spriteRenderer.sprite = unpause;
        }
        // un-pause
        else
        {
            ct.isPause = false;
            Time.timeScale = 1f;
            spriteRenderer.sprite = pause;
        }
    }
}
