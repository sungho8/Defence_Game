using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageStart : MonoBehaviour
{
    private Controller_Stage controllerStage;
    Controller_Tile controllerTile;

    private void Start()
    {
        controllerStage = GameObject.Find("BG_Field").GetComponent<Controller_Stage>();
        controllerTile = GameObject.Find("BG_Field").GetComponent<Controller_Tile>();
    }

    // 스테이지 시작
    void OnMouseUp()
    {
        
        if (controllerStage.isStart == false && controllerTile.CheckInvasionRoute())
        {
            controllerStage.isStart = true;
        }
        else
        {
            Debug.Log("스테이지를 시작할수 없습니다.");
        }
            
    }
}
