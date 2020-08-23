using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller_Hand : MonoBehaviour
{
    public GameObject[] handSlots;

    public GameObject[] towers;

    void Start()
    {
        towers = new GameObject[10];
    }

    public int checkEmpty()
    {
        for (int i = 0; i < towers.Length; i++) 
        {
            if(towers[i] == null)
            {
                return i;
            }
        }
        // 10 = 꽉찼다
        return towers.Length;
    }

    public void setPurchasedTower(GameObject tower)
    {
        int empty = checkEmpty();

        Debug.Log(empty);

        GameObject purchasedTower = Instantiate(tower, handSlots[empty].transform.position, Quaternion.identity);
        purchasedTower.transform.SetParent(transform);
        purchasedTower.transform.localScale = new Vector2(1.7f, 1.7f);

        towers[empty] = purchasedTower;

        handSlots[empty].GetComponent<SpriteRenderer>().color = new Color(255,255,255,0);
    }
}
