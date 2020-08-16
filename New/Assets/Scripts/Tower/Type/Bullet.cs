using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject target { get; set; }
    public GameObject effect;

    public int attack { get; set; }
    public float speed;

    private GameObject canvas;

    private void Awake()
    {
        canvas = GameObject.Find("Canvas");
    }

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
            temp.transform.localScale = new Vector2(0.5f, 0.5f);
            target.GetComponent<Enemy>().Damaged(attack);
            Destroy(this.gameObject);
        }
    }
}