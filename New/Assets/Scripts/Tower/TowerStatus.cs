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
    public int cost = 1;

    public string effect;

    private void Awake()
    {
        towerStatusUi = GameObject.Find("TowerStatusUiBg");
    }

    public void DestroyTower()
    {
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
