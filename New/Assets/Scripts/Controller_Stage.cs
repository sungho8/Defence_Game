using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller_Stage : MonoBehaviour
{
    public GameObject StageTextUI;
    public GameObject MoneyTextUI;
    public GameObject HpTextUI;
    public GameObject ManaTextUI;
    //public GameObject ExpTextUI;
    public GameObject LevelTextUI;

    Controller_Shop controllerShop;

    public bool isStart = false;

    public int Stage = 1;
    int money = 3;
    public int Money { 
        get { return money; } 
        set { money = value; SetTextMesh(); } 
    }

    int hp = 5;
    public int HP
    {
        get { return hp; }
        set { hp = value; SetTextMesh(); }
    }

    public int manaMax = 30;
    int mana = 30;
    public int Mana
    {
        get { return mana; }
        set { mana = value; SetTextMesh(); }
    }

    private void Start()
    {
        SetTextMesh();
        controllerShop = GameObject.Find("BG_Shop").GetComponent<Controller_Shop>();
    }

    private void SetTextMesh()
    {
        StageTextUI.GetComponent<TextMesh>().text = "Stage " + Stage;
        MoneyTextUI.GetComponent<TextMesh>().text = Money + "$";
        HpTextUI.GetComponent<TextMesh>().text = "" + hp;
        //LevelTextUI.GetComponent<TextMesh>().text = "Lv " + level;
        ManaTextUI.GetComponent<TextMesh>().text = "" + mana;
    }

    /*
     스테이지 클리어시
     1. Stage += 1
     2. Money += 5
     3. isStart = false
     4. 상점 초기화
     5. exp + 2;
         */
    
    public void CurrenStageClear()
    {
        Stage += 1;
        Money += 5;
        isStart = false;
        
        SetTextMesh();
        controllerShop.setNewItem();
    }
}