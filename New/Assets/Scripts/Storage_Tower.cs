using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Storage_Tower : MonoBehaviour
{
    public Controller_Tower controllerTower;
    public List<GameObject> TowerList;  // 상점에 사용되는 전체 타워 리스트


    // 상점, 핸드, 필드에 있는 타워
    List<GameObject> tower_Shop;
    List<GameObject> tower_Hand;
    List<GameObject> tower_Field;    //controllerTile 에 arrangedTowers;

    public List<GameObject> Tower_Shop
    {
        get
        {
            return tower_Shop;
        }
    }

    public List<GameObject> Tower_Hand 
    { 
        get
        {
            controllerTower.CheckTowerUpgrade();
            return tower_Hand;
        }
    }

    public List<GameObject> Tower_Field
    {
        get
        {
            return tower_Field;
        }
    }

    private void Start()
    {
        tower_Shop = new List<GameObject>(new GameObject[5]);
        tower_Hand = new List<GameObject>(new GameObject[10]);
        tower_Field = new List<GameObject>(new GameObject[45]);

        controllerTower = GameObject.Find("BG_Field").GetComponent<Controller_Tower>();
    }
}
