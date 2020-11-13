using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller_Stage : MonoBehaviour
{
    public GameObject StageTextUI;
    public GameObject MoneyTextUI;
    public GameObject HpTextUI;
    public GameObject ManaTextUI;
    public GameObject LevelTextUI;

    Controller_Shop controllerShop;
    Storage_Tower storage;

    public bool isStart = false;
    public bool IsStart
    {
        get {return isStart; 
        }
        set {
            Debug.Log("스테이지 시작");
            if (value) StartEvent();
            isStart = value; }
    }

    public int Stage = 1;
    int money = 30;
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

    public int manaMax = 3;
    int mana = 3;
    public int Mana
    {
        get { return mana; }
        set { mana = value; SetTextMesh(); }
    }

    int level = 1;
    public int Level
    {
        get { return level; }
        set { level = value; SetTextMesh(); }
    }

    private void Start()
    {
        SetTextMesh();
        controllerShop = GameObject.Find("BG_Shop").GetComponent<Controller_Shop>();
        storage = GameObject.Find("Storage").GetComponent<Storage_Tower>();
    }

    private void SetTextMesh()
    {
        StageTextUI.GetComponent<TextMesh>().text = "Stage " + Stage;
        MoneyTextUI.GetComponent<TextMesh>().text = Money + "$";
        HpTextUI.GetComponent<TextMesh>().text = "" + hp;
        LevelTextUI.GetComponent<TextMesh>().text = ""+level;
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
        IsStart = false;
        
        SetTextMesh();
        controllerShop.setNewItem();
    }

    public void StartEvent()
    {
        for (int i = 0; i < storage.Tower_Field.Count; i++) 
        {
            if(storage.Tower_Field[i] != null && storage.Tower_Field[i].GetComponent<Floor>() != null)
            {
                storage.Tower_Field[i].GetComponent<Floor>().EffectOn();
            }
        }
    }
}