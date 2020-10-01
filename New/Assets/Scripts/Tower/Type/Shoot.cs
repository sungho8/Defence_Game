using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Magic type Script

public class Shoot : MonoBehaviour
{
    public GameObject bullet;
    
    private TowerStatus towerStatus;
    private Controller_Enemy c;
    private float time = 0;

    private void Awake()
    {
        towerStatus = GetComponent<TowerStatus>();
        c = GameObject.Find("BG_Field").GetComponent<Controller_Enemy>();
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
                    ShootBullet(CheckTarget());
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

        if (c.Enemys.Count != 0)
        {
            for (int i = 0; i < c.Enemys.Count; i++)
            {
                offset = c.Enemys[i].transform.position - transform.position;

                if (min > offset.sqrMagnitude)
                {
                    min = offset.sqrMagnitude;
                    minIndex = i;
                }
            }
            return c.Enemys[minIndex];
        }
        else
        {
            return null;
        }
    }

    // 총알 생성
    void ShootBullet(GameObject _target)
    {
        GameObject temp = Instantiate(bullet, transform.position, Quaternion.identity);
        temp.transform.localScale = new Vector2(0.5f, 0.5f);

        Bullet bul = temp.GetComponent<Bullet>();
        bul.target = _target;
        bul.attack = towerStatus.attack;
    }
}