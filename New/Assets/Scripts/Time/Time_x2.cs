using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Time_x2 : MonoBehaviour
{
    public Sprite x1;
    public Sprite x2;

    Controller_Time ct;
    SpriteRenderer spriteRenderer;
    void Start()
    {
        ct = GameObject.Find("TimeController").GetComponent<Controller_Time>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void OnMouseUp()
    {
        if(ct.isPause == false)
        {
            // 2배속
            if(ct.isFastSpeed == false)
            {
                ct.isFastSpeed = true;
                Time.timeScale = 2f;
                spriteRenderer.sprite = x2;
            }
            // 1배속
            else
            {
                ct.isFastSpeed = false;
                Time.timeScale = 1f;
                spriteRenderer.sprite = x1;
            }
        }
    }
}
