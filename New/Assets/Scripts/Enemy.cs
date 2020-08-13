using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 0.1f;
    public int hp = 10;

    private int target = 0;
    Controller_Tile controllerTile;

    void Awake()
    {
        controllerTile = GameObject.Find("BG_Field").GetComponent<Controller_Tile>();
    }

    void Update()
    {
        // Move
        if (target < controllerTile.InvasionRoute.Count - 1) 
            CheckNextTile();
        Vector2 targetVec = controllerTile.InvasionRoute[target].transform.position;
        transform.position = Vector3.MoveTowards(transform.position, targetVec, speed);
    }

    int CheckNextTile()
    {
        float closed = 0.001f;
        Vector2 offset;
        
        offset = controllerTile.InvasionRoute[target].transform.position - transform.position;

        if (closed > offset.sqrMagnitude)
        {
            return ++target;
        }
        return target;
    }

    void Damaged(int _attack)
    {
        hp -= _attack;
    }
}
