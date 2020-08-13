using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Shoot type Script

public class Shoot : MonoBehaviour
{
    public GameObject bullet;
    
    private TowerStatus t;
    private Controller_Enemy c;
    private GameObject canvas;
    private float time = 0;

    private void Awake()
    {
        t = GetComponent<TowerStatus>();
        c = GameObject.Find("BG_Field").GetComponent<Controller_Enemy>();
        canvas = GameObject.Find("Canvas");
    }

    void Update()
    {
        time += Time.deltaTime;
        if (time > t.attack_speed)
        {
            if (CheckTarget() != null)
            {
                ShootBullet(CheckTarget());
            }
            time = 0;
        }
    }

    GameObject CheckTarget()
    {
        Vector2 offset;

        float min = 10f;
        int minIndex = 0;

        for (int i = 0; i < c.Enemys.Count; i++)
        {
            offset = c.Enemys[i].transform.position - transform.position;

            if(min > offset.sqrMagnitude)
            {
                min = offset.sqrMagnitude;
                minIndex = i;
            }
        }
        return c.Enemys[minIndex];
    }

    // 총알 생성
    void ShootBullet(GameObject _target)
    {
        GameObject temp = Instantiate(bullet, transform.position, Quaternion.identity);
        temp.transform.SetParent(canvas.transform);
        temp.transform.localScale = new Vector2(60f, 60f);
        temp.GetComponent<Bullet>().target = _target;
        temp.GetComponent<Bullet>().attack = t.attack;
    }
}
