using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageStart : MonoBehaviour
{
    private Controller_Stage controllerStage;

    private void Start()
    {
        controllerStage = GameObject.Find("BG_Field").GetComponent<Controller_Stage>();
    }

    // 스테이지 시작
    void OnMouseUp()
    {
        if(controllerStage.isStart == false)
            controllerStage.isStart = true;
    }
}
