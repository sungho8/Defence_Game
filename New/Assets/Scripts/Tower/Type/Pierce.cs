using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pierce : MonoBehaviour
{
    public GameObject bullet;

    private TowerStatus status;
    private Controller_Enemy controllerEnemy;
    private float time = 0;

    private void Awake()
    {
        status = GetComponent<TowerStatus>();
        controllerEnemy = GameObject.Find("BG_Field").GetComponent<Controller_Enemy>();
    }

    void Update()
    {
        if (status.currentState == "Field")
        {
            time += Time.deltaTime;
            if (time > status.attack_speed)
            {
                if (CheckTarget() != null)
                {
                    PierceBullet(CheckTarget());
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

        if (controllerEnemy.currentEnemys.Count != 0)
        {
            for (int i = 0; i < controllerEnemy.currentEnemys.Count; i++)
            {
                offset = controllerEnemy.currentEnemys[i].transform.position - transform.position;

                if (min > offset.sqrMagnitude)
                {
                    min = offset.sqrMagnitude;
                    minIndex = i;
                }
            }
            return controllerEnemy.currentEnemys[minIndex];
        }
        else
        {
            return null;
        }
    }

    void PierceBullet(GameObject _target)
    {
        GameObject temp = Instantiate(bullet, transform.position, Quaternion.identity);
        temp.transform.localScale = new Vector2(0.2f, 0.2f);

        PierceBullet bul = temp.GetComponent<PierceBullet>();
        bul.target = _target;
        bul.tower = this.gameObject;
        bul.attack = status.attack;
    }
}
