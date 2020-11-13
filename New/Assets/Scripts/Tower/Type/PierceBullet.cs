using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PierceBullet : MonoBehaviour
{
    public GameObject target;
    public GameObject tower;
    public GameObject effect;
    public int attack;
    public float speed = 5f;

    Vector2 dirVector;
    Rigidbody2D rigidbody;


    private void Start()
    {
        dirVector = target.transform.position - tower.transform.position + new Vector3(0,0.4f,0);
        dirVector = dirVector.normalized;
        rigidbody = GetComponent<Rigidbody2D>();
        float digree = Mathf.Atan2(dirVector.y, dirVector.x) * 180f / Mathf.PI;
        transform.Rotate(0, 0, digree);


        Destroy(this.gameObject, 5f);
    }

    private void Update()
    {
        rigidbody.AddForce(dirVector * speed * Time.deltaTime, ForceMode2D.Impulse);

        // 게임 오브젝트 기준으로 이동
        //transform.Translate(speed * dirVector * Time.deltaTime);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            GameObject temp = Instantiate(effect, other.transform.position + new Vector3(0, 0.4f, 0), Quaternion.identity);
            temp.transform.localScale = new Vector2(0.5f, 0.5f);

            Enemy enemy = other.GetComponent<Enemy>();
            enemy.Damaged(attack);
        }
    }
}
