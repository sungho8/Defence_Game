using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour
{
    public GameObject Effect;
    public float range;

    Controller_Enemy controllerEnemy;
    Controller_Stage controllerStage;
    TowerStatus status;
    GameObject ef;

    float time = 0;

    void Start()
    {
        controllerEnemy = GameObject.Find("BG_Field").GetComponent<Controller_Enemy>();
        controllerStage = GameObject.Find("BG_Field").GetComponent<Controller_Stage>();
        status = GetComponent<TowerStatus>();
    }

    void Update()
    {
        if (status.currentState == "Field")
        {
            time += Time.deltaTime;
            if (time > status.attack_speed)
            {
                Attack();
                time = 0;
            }
        }
    }

    void Attack()
    {
        Vector2 offset;
        
        for (int j = 0; j < controllerEnemy.currentEnemys.Count; j++) 
        {
            offset = transform.position - controllerEnemy.currentEnemys[j].transform.position;
            if (range > offset.sqrMagnitude)
            {
                controllerEnemy.currentEnemys[j].GetComponent<Enemy>().Damaged(status.attack);
            }
        }
    }

    public void EffectOn(bool on)
    {
        if (on)
        {
            ef = Instantiate(Effect, transform.position, Quaternion.identity);
            ef.transform.localScale = new Vector3(0.5f, 0.5f, 1);
        }
        else
        {
            Destroy(ef);
        }
        
    }


}
