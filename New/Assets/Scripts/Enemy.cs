using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Controller_Tile controllerTile;
    Controller_Enemy controllerEnemy;
    Animation anim;

    public float speed = 0.1f;
    public int hp = 10;

    public int target = 0;

    void Awake()
    {
        controllerTile = GameObject.Find("BG_Field").GetComponent<Controller_Tile>();
        controllerEnemy = GameObject.Find("BG_Field").GetComponent<Controller_Enemy>();
        anim = gameObject.GetComponent<Animation>();
    }

    void Update()
    {
        // Move
        if (target < controllerTile.InvasionRoute.Count - 1) 
            CheckNextTile();
        Vector2 targetVec = controllerTile.InvasionRoute[target].transform.position;
        transform.position = Vector3.MoveTowards(transform.position, targetVec, Time.deltaTime * speed);
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

    public void Damaged(int _attack)
    {
        hp -= _attack;
        anim.Play("Damaged");

        // Die
        if (hp < 0)
        {
            controllerEnemy.Enemys.Remove(this.gameObject);
            Destroy(this.gameObject);
        }
    }

}
