using Spine.Unity;
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

    SkeletonAnimation monsterAnimator;
    Controller_Tile controllerTile;
    Controller_Enemy controllerEnemy;
    GameObject nextTile;
    GameObject hb = null;
    int tileIndex = 0;

    public GameObject StunEffect;
    public GameObject DamageEffect;
    public GameObject HpBar;
    public float speed;
    public float hp;
    public float maxhp;

    float next = 0.0f;
    float delay = 1.0f;
    float burnDamage = 0;
    float slowspeed = 1;

    int row = 0, col = 0;

    void Awake()
    {
        StateArr = new bool[STATE_COUNT];
        StateTime = new float[STATE_COUNT];
        monsterAnimator = GetComponent<SkeletonAnimation>();
        controllerTile = GameObject.Find("BG_Field").GetComponent<Controller_Tile>();
        controllerEnemy = GameObject.Find("BG_Field").GetComponent<Controller_Enemy>();

        nextTile = controllerTile.route[0];
        SetHpBar();
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
        if (!StateArr[STUN] && nextTile != null && monsterAnimator.AnimationName != "Dead")
        {
            if(monsterAnimator.AnimationName != "Walk")
                ChangeAnimation("Walk");

            Vector2 targetVec = nextTile.transform.position - new Vector3(0, 0.4f, 0);

            // 좌우회전
            if (targetVec.x - transform.position.x < 0)
                transform.rotation = Quaternion.Euler(0, 180, 0);
            else
                transform.rotation = Quaternion.Euler(0, 0, 0);

            transform.position = Vector3.MoveTowards(transform.position, targetVec, Time.deltaTime * slowspeed * speed);
        }
    }

    void CheckNextTile()
    {
        float closed = 0.4f;
        Vector2 offset = nextTile.transform.position - transform.position;
        if (closed > offset.sqrMagnitude)
        {
            if (tileIndex >= controllerTile.route.Count)
                return;
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
        GameObject de = Instantiate(DamageEffect, transform.position + new Vector3(0,0.3f,0), Quaternion.identity);
        de.transform.GetChild(0).GetComponent<TextMesh>().text = "-"+_attack;
        // Die
        if (hp <= 0)
        {
            ChangeAnimation("Dead");
            controllerEnemy.currentEnemys.Remove(this.gameObject);
            Destroy(hb);
            Destroy(this.gameObject,1.5f);
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

    public void ChangeAnimation(string AnimationName)  //Names are: Idle, Walk, Dead and Attack
    {
        if (monsterAnimator == null)
            return;

        bool IsLoop = true;
        if (AnimationName == "Dead")
            IsLoop = false;

        //set the animation state to the selected one
        monsterAnimator.AnimationState.SetAnimation(0, AnimationName, IsLoop);
    }

    void SetHpBar()
    {
        hb = Instantiate(HpBar);
        //hb.transform.SetParent(transform);
        hb.transform.localScale = new Vector3(0.03f, 0.03f, 0);
        hb.transform.position = transform.position;
        hb.GetComponent<HpBar>().TargetEnemy = this.gameObject;
    }
}