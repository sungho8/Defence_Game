using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerStatus : MonoBehaviour
{
    public GameObject towerPrefab;

    public string currentState; // Shop : 상점 , Hand : 핸드, Field : 전투중 타워
    public string towerName;
    public string element;
    public string type;

    public int attack;
    public float attack_speed;
    public int direction;

    private GameObject target;

    // 상점
    public int cost = 1;
}
