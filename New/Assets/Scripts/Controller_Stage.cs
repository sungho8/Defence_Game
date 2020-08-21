using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller_Stage : MonoBehaviour
{
    public GameObject StageTextUI;
    public GameObject MoneyTextUI;

    public bool isStart = false;

    int Stage = 1;
    int Money = 0;

    private void Start()
    {
        SetTextMesh();
    }

    private void SetTextMesh()
    {
        StageTextUI.GetComponent<TextMesh>().text = "Stage " + Stage;
        MoneyTextUI.GetComponent<TextMesh>().text = Money + "$";
    }

    /*
     스테이지 클리어시
     1. Stage += 1
     2. Money += 5
     3. isStart = false
         */
    
    public void CurrenStageClear()
    {
        Stage += 1;
        Money += 5;
        isStart = false;

        SetTextMesh();
    }
}