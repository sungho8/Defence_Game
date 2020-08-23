using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerDrag : MonoBehaviour
{
    Vector3 mousePosition;
    Vector3 objPosition;
    Vector3 originPosition;
    public Vector3 initMousePos;

    void OnMouseDown()
    {
        mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10);
        objPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        originPosition = transform.position;

        initMousePos = Input.mousePosition;
        initMousePos.z = -10;
        initMousePos = Camera.main.ScreenToWorldPoint(initMousePos);
    }

    void OnMouseDrag()
    {
        Vector3 worldPoint = Input.mousePosition;
        worldPoint.z = -10;
        worldPoint = Camera.main.ScreenToWorldPoint(worldPoint);
        Vector3 diffPos = worldPoint - initMousePos;
        diffPos.z = 0;
        initMousePos = Input.mousePosition;
        initMousePos.z = -10;
        initMousePos = Camera.main.ScreenToWorldPoint(initMousePos);
        transform.position = new Vector3(transform.position.x + diffPos.x,
                        transform.position.y + diffPos.y,
                        transform.position.z);
    }

    public void OnMouseUp()
    {
        
    }
}
