using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 타워 -> 타겟 -> 타워 -> 타겟... 을 반복하는 총알

public class Bumerang : MonoBehaviour
{
    public GameObject effect;
    public float speed;

    public GameObject target;
    public GameObject tower;

    bool goTarget = true;

    void Update()
    {
        MoveToTarget();
    }

    void MoveToTarget()
    {
        // 타워 -> 타겟
        if (goTarget)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
        }
        // 타겟 -> 타워
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, tower.transform.position, speed * Time.deltaTime);
        }
    }

    void ChangeTarget(bool b)
    {
        goTarget = b;

        float r1 = Random.Range(1f, -1f);
        float r2 = Random.Range(1f, -1f);
        GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        GetComponent<Rigidbody2D>().AddForce(new Vector3(r1, r2, 0) * 100f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == target)
        {
            ChangeTarget(false);
        }
        else if(other.gameObject == tower)
        {
            ChangeTarget(true);
        }
    }

}