using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller_Hand : MonoBehaviour
{
    public GameObject[] handSlots;  // 핸드 자리 10개

    public GameObject[] towers;     // 각 핸드에있는 타워, 빈자리는 null

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

        GameObject purchasedTower = Instantiate(tower, handSlots[empty].transform.position, Quaternion.identity);
        purchasedTower.transform.SetParent(transform);
        purchasedTower.transform.localScale = new Vector2(1.7f, 1.7f);
        purchasedTower.GetComponent<TowerDrag>().index = -2 - empty;

        towers[empty] = purchasedTower;

        handSlots[empty].GetComponent<SpriteRenderer>().color = new Color(255,255,255,0);
    }
}
