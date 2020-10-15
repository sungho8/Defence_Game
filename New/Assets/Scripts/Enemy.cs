using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // state
    bool[] StateArr;
    float[] StateTime;
    const int STATE_COUNT = 3;
    const int STUN = 0;
    const int BURN = 1;
    const int SLOW = 2;

    Controller_Tile controllerTile;
    Controller_Enemy controllerEnemy;
    Animation anim;

    public GameObject StunEffect;
    public float speed = 0.1f;
    public float hp = 10;
    public int target = 0;

    float next = 0.0f;
    float delay = 1.0f;
    float burnDamage = 0;
    float slowspeed = 1;

    void Awake()
    {
        StateArr = new bool[STATE_COUNT];
        StateTime = new float[STATE_COUNT];
        controllerTile = GameObject.Find("BG_Field").GetComponent<Controller_Tile>();
        controllerEnemy = GameObject.Find("BG_Field").GetComponent<Controller_Enemy>();
        anim = gameObject.GetComponent<Animation>();
    }

    void Update()
    {
        // 상태이상
        if (Time.time > next)
        {
            next = Time.time + delay;
            CheckState();
        }

        // Move
        if (target < controllerTile.InvasionRoute.Count - 1) 
            CheckNextTile();

        if (!StateArr[STUN])
        {
            Vector2 targetVec = controllerTile.InvasionRoute[target].transform.position;
            transform.position = Vector3.MoveTowards(transform.position, targetVec, Time.deltaTime * slowspeed * speed);
        }
    }

    int CheckNextTile()
    {
        float closed = 0.001f;
        Vector2 offset = controllerTile.InvasionRoute[target].transform.position - transform.position;

        if (closed > offset.sqrMagnitude)
        {
            return ++target;
        }
        return target;
    }

    void CheckState()
    {
        for(int i =0; i < StateArr.Length; i++)
        {
            if (StateTime[i] <= 0) StateArr[i] = false;
            if (StateArr[i])
            {
                StateTime[i]--;

                if (i == BURN)
                {
                    Damaged(burnDamage);
                }
                else if(i == STUN)
                {
                    GameObject e = Instantiate(StunEffect, transform.position + new Vector3(0, 0.5f, 0), Quaternion.identity);
                    Destroy(e, StateTime[i]);
                }
                else if(i == SLOW)
                {
                    slowspeed = 0.5f;
                }
            }

            if (!StateArr[SLOW]) slowspeed = 1.0f;
        }

        
    }

    public void Damaged(float _attack)
    {
        hp -= _attack;
        anim.Play("Damaged");

        // Die
        if (hp <= 0)
        {
            controllerEnemy.Enemys.Remove(this.gameObject);
            Destroy(this.gameObject);
        }
    }

    public void ChangeState(int state, float stime, float sdam)
    {
        StateArr[state] = true;
        StateTime[state] = stime;

        if(state == BURN)
        {
            burnDamage = sdam;
        }
    }
}