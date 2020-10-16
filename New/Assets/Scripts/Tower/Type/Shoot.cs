using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Magic type Script

// 총알 생성 (일반 투사체, 부메랑 투사체)
public class Shoot : MonoBehaviour
{
    public GameObject bullet;
    
    private TowerStatus towerStatus;
    private Controller_Enemy controllerEnemy;
    private float time = 0;

    private void Awake()
    {
        towerStatus = GetComponent<TowerStatus>();
        controllerEnemy = GameObject.Find("BG_Field").GetComponent<Controller_Enemy>();
    }

    void Update()
    {
        if(towerStatus.currentState == "Field")
        {
            time += Time.deltaTime;
            if (time > towerStatus.attack_speed)
            {
                if (CheckTarget() != null)
                {
                    NormalBullet(CheckTarget());
                }
                time = 0;
            }
        }
    }

    GameObject CheckTarget()
    {
        Vector2 offset;

        float min = 10f;
        int minIndex = 0;

        if (controllerEnemy.Enemys.Count != 0)
        {
            for (int i = 0; i < controllerEnemy.Enemys.Count; i++)
            {
                offset = controllerEnemy.Enemys[i].transform.position - transform.position;

                if (min > offset.sqrMagnitude)
                {
                    min = offset.sqrMagnitude;
                    minIndex = i;
                }
            }
            return controllerEnemy.Enemys[minIndex];
        }
        else
        {
            return null;
        }
    }

    // 총알 생성(부메랑 추가)
    void NormalBullet(GameObject _target)
    {
        GameObject temp = Instantiate(bullet, transform.position, Quaternion.identity);
        temp.transform.localScale = new Vector2(0.5f, 0.5f);

        Bullet bul = temp.GetComponent<Bullet>();
        bul.target = _target;
        bul.tower = this.gameObject;
        bul.attack = towerStatus.attack;
    }

}