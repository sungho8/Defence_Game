using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBar : MonoBehaviour
{
    public GameObject TargetEnemy;

    Slider hpBar;
    TextMesh txt;
    Enemy enemy;
    float hp;
    float maxhp;
    
    private void Start()
    {
        hpBar = transform.GetChild(0).GetComponent<Slider>();
        txt = transform.GetChild(1).GetComponent<TextMesh>();
        enemy = TargetEnemy.GetComponent<Enemy>();
    }

    // Update is called once per frame
    void Update()
    {
        if(TargetEnemy != null)
            transform.position = TargetEnemy.transform.position - new Vector3(0, 0.25f, 0);
        hpBar.value = enemy.hp / enemy.maxhp;
        txt.text = enemy.hp+"";
    }
}
