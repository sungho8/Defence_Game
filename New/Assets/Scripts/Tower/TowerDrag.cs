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
    GameObject message;

    public int index = -1;
    int preIndex = -1;
    int x = 0;
    int y = 0;

    private void Start()
    {
        controllerTile = GameObject.Find("BG_Field").GetComponent<Controller_Tile>();
        controllerStage = GameObject.Find("BG_Field").GetComponent<Controller_Stage>();
        controllerHand = GameObject.Find("Hand").GetComponent<Controller_Hand>();
        storageTower = GameObject.Find("Storage").GetComponent<Storage_Tower>();
        message = Resources.Load<GameObject>("Message");

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

            
            // 이미 배치되어있는 타워일경우 (- 일경우 핸드)
            if (index >= 0)
            {
                preIndex = index;
                index = -1;
            }
            else if(index < -1)
            {
                preIndex = index;
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
            transform.position = new Vector3(transform.position.x + diffPos.x, transform.position.y + diffPos.y, transform.position.z);

            index = controllerTile.CheckClosedTile(transform);
            // 필드
            if (index >= 0) 
            {
                // 배치위치 표시이펙트
                // index = 0
                y = index / controllerTile.col;
                x = index % controllerTile.col;

                arrangeEffect.transform.position = controllerTile.Tile[y][x].transform.position;
            }else if (index < -1)
            {
                arrangeEffect.transform.position = controllerHand.handSlots[-1 * index - 2].transform.position;
            }

            //Debug.Log(name + " : (pre : " + preIndex + ", index : " + index + ")");
        }
    }
    
    public void OnMouseUp()
    {
        // 스테이지 비 진행중에만 드래그 가능
        if (!controllerStage.isStart)
        {
            // 필드 배치
            if (index >= 0)
            {
                // 핸드에서 필드로 배치하는경우 마나가 부족하면 배치하지못한다.
                if(preIndex < -1 && !storageTower.CheckMana())
                {
                    originArrange();
                    Instantiate(message).GetComponent<Message>().MessageMana();
                    return;
                }

                // 이미 타워가 존재하는곳에 배치하면 서로 위치가 바뀐다.
                if(storageTower.Tower_Field[index] != null)
                {
                    originArrange();
                    //swapArrange();
                    return;
                }
                else
                {
                    preClear();
                }

                transform.position = controllerTile.Tile[y][x].transform.position; // 타워 배치
                storageTower.Tower_Field[index] = this.gameObject;
                towerStatus.currentState = "Field";
                preClear();
            }
            // 핸드 배치
            else if (index < -1)
            {
                // 이미 타워가 존재하는곳에 배치하면 서로 위치가 바뀐다.
                if (storageTower.Tower_Hand[-1 * index - 2] != null)
                {
                    originArrange();
                    return;
                    //swapArrange();
                }
                else
                {
                    preClear();
                }

                transform.position = controllerHand.handSlots[-1 * index - 2].transform.position;
                storageTower.Tower_Hand[-1 * index - 2] = this.gameObject;
                towerStatus.currentState = "Hand";
                
            }
            // 엉뚱한위치로 배치시 원래위치로
            else
            {
                originArrange();
            }
        }
        PrintHand();
        controllerTile.CheckInvasionRoute();
        arrangeEffect.transform.position = new Vector3(0, 3000, 0);
    }

    // 원래위치로
    void originArrange()
    {
        if(preIndex >= 0)
        {
            storageTower.Tower_Field[preIndex] = this.gameObject;
        }
        else
        {
            storageTower.Tower_Hand[-1 * preIndex - 2] = this.gameObject;
        }

        transform.position = originPos;
        arrangeEffect.transform.position = new Vector3(0, 3000, 0);
        index = preIndex;
    }

    // 서로 교환
    void swapArrange()
    {
        string prestate = "";
        GameObject preObj;

        if (preIndex >= 0)
        {
            prestate = "Field";
            preObj = storageTower.Tower_Field[preIndex];
        }
        else
        {
            prestate = "Hand";
            preObj = storageTower.Tower_Hand[-1 * preIndex - 2];
        }

        // 교환 대상이 필드
        if (index >= 0)
        {
            storageTower.Tower_Field[index].transform.position = originPos;
            storageTower.Tower_Field[index].GetComponent<TowerStatus>().currentState = prestate;
            storageTower.Tower_Field[index].GetComponent<TowerDrag>().index = preIndex;

            if (preIndex >= 0)
            {
                storageTower.Tower_Field[preIndex] = storageTower.Tower_Field[index];
            }
            else
            {
                storageTower.Tower_Hand[-1 * preIndex - 2] = storageTower.Tower_Field[index];
            }
        }
        // 교환 대상이 핸드
        else
        {
            storageTower.Tower_Hand[-1 * index - 2].transform.position = originPos;
            storageTower.Tower_Hand[-1 * index - 2].GetComponent<TowerStatus>().currentState = prestate;
            storageTower.Tower_Hand[-1 * index - 2].GetComponent<TowerDrag>().index = preIndex;

            if (preIndex >= 0)
            {
                storageTower.Tower_Field[preIndex] = storageTower.Tower_Hand[-1 * index - 2];
            }
            else
            {
                storageTower.Tower_Hand[-1 * preIndex - 2] = storageTower.Tower_Hand[-1 * index - 2];
            }
        }
    }

    void preClear()
    {
        if (preIndex == index)
            return;

        if(preIndex >= 0)
        {
            storageTower.Tower_Field[preIndex] = null;
        }
        else
        {
            storageTower.Tower_Hand[-1 * preIndex - 2] = null;
        }
    }

    void PrintHand()
    {
        string prt = "";
        for(int i = 0; i < storageTower.Tower_Hand.Count; i++)
        {
            if (storageTower.Tower_Hand[i] != null)
                prt += "(" + i + "," + storageTower.Tower_Hand[i].name + ") , ";
        }
        Debug.Log(prt);

        string prt2 = "";
        for (int i = 0; i < storageTower.Tower_Field.Count; i++)
        {
            if (storageTower.Tower_Field[i] != null)
                prt2 += "(" + i + "," + storageTower.Tower_Field[i].name + ") , ";
        }
        Debug.Log(prt2);
    }
}