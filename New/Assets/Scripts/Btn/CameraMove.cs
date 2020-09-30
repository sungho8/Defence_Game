using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public GameObject Camera;
    public Sprite on;
    public Sprite off;

    PinchZoom pinchZoom;
    SpriteRenderer spriteRenderer;
    void Start()
    {
        pinchZoom = Camera.GetComponent<PinchZoom>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void OnMouseUp()
    {
        if (pinchZoom.isCameraMoveOn == false)
        {
            pinchZoom.isCameraMoveOn = true;
            spriteRenderer.sprite = on;
        }
        else
        {
            pinchZoom.isCameraMoveOn = false;
            spriteRenderer.sprite = off;
        }
    }
}
