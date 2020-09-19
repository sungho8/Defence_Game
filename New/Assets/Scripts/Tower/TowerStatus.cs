using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerStatus : MonoBehaviour
{
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

    public void DestroyTower()
    {
        Destroy(this.gameObject);
    }

    public void UpgradeTower()
    {
        grade++;
        this.gameObject.name = (grade+1) + name;
        transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = rateSprite[grade];
        attack *= 2;
    }
}
