using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject target;
    public GameObject tower;
    public GameObject effect;
    public int BulletType;

    public float timeType;
    public float damageType;

    const int STUN = 0;
    const int BURN = 1;
    const int SLOW = 2;

    public int attack { get; set; }
    public float speed;

    public void setTarget(GameObject _target)
    {
        target = _target;
    }

    private void Update()
    {
        if(target != null)
        {
            transform.Rotate(0, 0, Time.deltaTime* 2f);

            Vector2 targetVec = target.transform.position;
            transform.position = Vector3.MoveTowards(transform.position, targetVec, speed * Time.deltaTime);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    // 
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (target != null && other.gameObject == target)
        {
            GameObject temp = Instantiate(effect, target.transform.position, Quaternion.identity);
            Enemy enemy = target.GetComponent<Enemy>();
            temp.transform.localScale = new Vector2(0.6f, 0.6f);
            enemy.Damaged(attack);

            // 상태이상
            if(BulletType != -1) enemy.ChangeState(BulletType, timeType, damageType);
                
            Destroy(this.gameObject);
        }
    }
}