using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // State
    bool[] StateArr;
    float[] StateTime;
    const int STATE_COUNT = 3;
    const int STUN = 0;
    const int BURN = 1;
    const int SLOW = 2;

    Controller_Tile controllerTile;
    Controller_Enemy controllerEnemy;
    Animation anim;
    GameObject nextTile;
    int tileIndex = 0;

    public GameObject StunEffect;
    public float speed;
    public float hp;

    float next = 0.0f;
    float delay = 1.0f;
    float burnDamage = 0;
    float slowspeed = 1;

    int row = 0, col = 0;

    void Awake()
    {
        StateArr = new bool[STATE_COUNT];
        StateTime = new float[STATE_COUNT];
        controllerTile = GameObject.Find("BG_Field").GetComponent<Controller_Tile>();
        controllerEnemy = GameObject.Find("BG_Field").GetComponent<Controller_Enemy>();
        anim = gameObject.GetComponent<Animation>();

        nextTile = controllerTile.route[0];
    }

    void Update()
    {
        // 상태이상
        if (Time.time > next)
        {
            next = Time.time + delay;
            CheckState();
        }
        
        CheckNextTile();

        // 스턴상태 아니면 다음타일로 이동
        if (!StateArr[STUN] && nextTile != null)
        {
            Vector2 targetVec = nextTile.transform.position;
            transform.position = Vector3.MoveTowards(transform.position, targetVec, Time.deltaTime * slowspeed * speed);
        }
    }

    void CheckNextTile()
    {
        float closed = 0.01f;
        Vector2 offset = nextTile.transform.position - transform.position;
        if (closed > offset.sqrMagnitude)
        {
            nextTile = controllerTile.route[++tileIndex];
        }
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