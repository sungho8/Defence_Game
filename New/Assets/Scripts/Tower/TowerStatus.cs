using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerStatus : MonoBehaviour
{
    private GameObject towerStatusUi;
    public GameObject towerPrefab;
    public Sprite[] rateSprite;

    public string currentState; // Shop : 상점 , Hand : 핸드, Field : 전투중 타워
    public string towerName;
    public string element;
    public string type;

    public int attack;
    public float attack_speed;
    public int direction;

    public int grade = 0;
    // 상점
    public int cost;

    public string effect;

    private void Awake()
    {
        towerStatusUi = GameObject.Find("TowerStatusUiBg");
    }

    public void DestroyTower()
    {
        int index = GetComponent<TowerDrag>().index;
        if (currentState == "Field")
        {
            GameObject.Find("Storage").GetComponent<Storage_Tower>().Tower_Field[index] = null;
        }
        else
        {
            GameObject.Find("Storage").GetComponent<Storage_Tower>().Tower_Hand[-1 * index - 2] = null;
        }
        GameObject.Find("BG_Field").GetComponent<Controller_Tile>().CheckInvasionRoute();
        Destroy(this.gameObject);
    }

    public void UpgradeTower()
    {
        grade++;
        this.gameObject.tag = "Tower";
        transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = rateSprite[grade];
        attack *= 2;
    }

    void OnMouseUp()
    {
        Sprite spr = null;
        if(tag == "Tower")
        {
            spr = transform.GetChild(0).GetComponent<SpriteRenderer>().sprite;
        }
        else
        {
            spr = transform.GetComponent<SpriteRenderer>().sprite;
        }

        towerStatusUi.GetComponent<TowerStatusUi>().SetUiTxt(spr,this.GetComponent<TowerStatus>());
    }
}
