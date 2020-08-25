using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerDrag : MonoBehaviour
{
    Vector3 mousePosition;
    Vector3 originPosition;
    Vector3 initMousePos;

    Controller_Tile controllerTile;
    GameObject arrangeEffect;

    int index = -1;

    private void Start()
    {
        controllerTile = GameObject.Find("BG_Field").GetComponent<Controller_Tile>();
        arrangeEffect = GameObject.Find("arrangeEffect");
    }

    void OnMouseDown()
    {
        mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10);
        originPosition = transform.position;

        initMousePos = Input.mousePosition;
        initMousePos.z = -10;
        initMousePos = Camera.main.ScreenToWorldPoint(initMousePos);

        // 이미 배치되어있는 타워일경우
        if(index != -1)
        {
            controllerTile.ArrangedTower[index] = null;
        }
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

        index = controllerTile.CheckClosedTile(transform);
        // 배치위치 표시이펙트
        arrangeEffect.transform.position = controllerTile.Tile[index].transform.position;
    }

    public void OnMouseUp()
    {
        arrangeEffect.transform.position = new Vector3(0, 3000, 0);         // 이펙트 멀리멀리
        transform.position = controllerTile.Tile[index].transform.position; // 타워 배치

        controllerTile.ArrangedTower[index] = this.gameObject;
    }
}
