using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerDrag : MonoBehaviour
{
    Vector3 initMousePos;
    Vector3 originPos;

    Storage_Tower storageTower;
    Controller_Tile controllerTile;
    Controller_Stage controllerStage;
    Controller_Hand controllerHand;
    TowerStatus towerStatus;
    GameObject arrangeEffect;

    public int index = -1;
    int x = 0;
    int y = 0;

    private void Start()
    {
        controllerTile = GameObject.Find("BG_Field").GetComponent<Controller_Tile>();
        controllerStage = GameObject.Find("BG_Field").GetComponent<Controller_Stage>();
        controllerHand = GameObject.Find("Hand").GetComponent<Controller_Hand>();
        storageTower = GameObject.Find("Storage").GetComponent<Storage_Tower>();

        arrangeEffect = GameObject.Find("arrangeEffect");
        towerStatus = GetComponent<TowerStatus>();
    }

    void OnMouseDown()
    {
        // 스테이지 비 진행중에만 드래그 가능
        if(!controllerStage.isStart)
        {
            initMousePos = Input.mousePosition;
            initMousePos.z = -10;
            initMousePos = Camera.main.ScreenToWorldPoint(initMousePos);

            originPos = transform.position; // 원래위치 기억

            // 이미 배치되어있는 타워일경우 (- 일경우 아직 배치되지 않은 타워)
            if (index >= 0)
            {
                storageTower.Tower_Field[index] = null;
                index = -1;
            }
            else if(index < -1)
            {
                storageTower.Tower_Hand[-1 * index - 2] = null;
            }
        }
    }

    void OnMouseDrag()
    {
        // 스테이지 비 진행중에만 드래그 가능
        if (!controllerStage.isStart)
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
            if (index >= 0) 
            {
                // 배치위치 표시이펙트
                // index = 0

                y = index / controllerTile.col;
                x = index % controllerTile.col;
                Debug.Log(index +" = "+ x + "," +y);

                arrangeEffect.transform.position = controllerTile.Tile[y][x].transform.position;
            }else if (index < -1)
            {
                arrangeEffect.transform.position = controllerHand.handSlots[-1 * index - 2].transform.position;
            }
        }
    }

    public void OnMouseUp()
    {
        // 스테이지 비 진행중에만 드래그 가능
        if (!controllerStage.isStart)
        {
            // 필드 배치 && 마나체크
            if (index >= 0)
            {
                if (!storageTower.CheckMana())
                {
                    transform.position = originPos;
                    arrangeEffect.transform.position = new Vector3(0, 3000, 0);
                    return;
                }

                transform.position = controllerTile.Tile[y][x].transform.position; // 타워 배치
                towerStatus.currentState = "Field";
                storageTower.Tower_Field[index] = this.gameObject;
            }
            // 핸드 배치
            else if (index < -1)
            {
                transform.position = controllerHand.handSlots[-1 * index - 2].transform.position;
                towerStatus.currentState = "Hand";
                storageTower.Tower_Hand[-1 * index - 2] = this.gameObject;
            }
            // 엉뚱한위치로 배치시 원래위치로
            else
            {
                transform.position = originPos;
            }
        }
        arrangeEffect.transform.position = new Vector3(0, 3000, 0);         // 이펙트 멀리멀리
        storageTower.CheckMana();
    }
}