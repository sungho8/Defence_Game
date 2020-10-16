using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerStatusUi : MonoBehaviour
{
    Vector3 oripos;
    public GameObject towersprite;

    public GameObject nameTxt;
    public GameObject elementTxt;
    public GameObject typeTxt;
    public GameObject attackTxt;
    public GameObject attackSpeedTxt;
    public GameObject costTxt;
    public GameObject effectTxt;

    private void Awake()
    {
        oripos = this.transform.position;
        transform.position = new Vector3(-3200,1000,0);
    }

    public void SetUiTxt(Sprite spr,TowerStatus ts)
    {
        transform.position = oripos;
        towersprite.GetComponent<SpriteRenderer>().sprite = spr;

        nameTxt.GetComponent<TextMesh>().text = ts.towerName +" "+ (ts.grade+1) + "★";   // 타워 이름
        elementTxt.GetComponent<TextMesh>().text = ts.element;  // 타워 원소
        typeTxt.GetComponent<TextMesh>().text = ts.type;
        attackTxt.GetComponent<TextMesh>().text = ts.attack + "";
        attackSpeedTxt.GetComponent<TextMesh>().text = ts.attack_speed + "";
        costTxt.GetComponent<TextMesh>().text = ts.cost + "$";

        effectTxt.GetComponent<TextMesh>().text = ts.effect;   // 효과
    }

    private void OnMouseUp()
    {
        transform.position = new Vector3(-3200, 1000, 0);
    }
}
