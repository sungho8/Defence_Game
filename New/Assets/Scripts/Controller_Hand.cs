using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller_Hand : MonoBehaviour
{
    Storage_Tower storageTower;

    public GameObject[] handSlots;  // 핸드 자리 10개

    void Start()
    {
        storageTower = GameObject.Find("Storage").GetComponent<Storage_Tower>();
    }

    public int checkEmpty()
    {
        for (int i = 0; i < storageTower.Tower_Hand.Count; i++) 
        {
            if(storageTower.Tower_Hand[i] == null)
            {
                return i;
            }
        }
        // 10 = 꽉찼다
        return storageTower.Tower_Hand.Count;
    }

    public void setPurchasedTower(GameObject tower)
    {
        int empty = checkEmpty();

        GameObject purchasedTower = Instantiate(tower, handSlots[empty].transform.position, Quaternion.identity);
        purchasedTower.transform.SetParent(transform);
        purchasedTower.transform.localScale = new Vector2(1.7f, 1.7f);
        purchasedTower.GetComponent<TowerDrag>().index = -2 - empty;

        storageTower.Tower_Hand[empty] = purchasedTower;

        //handSlots[empty].GetComponent<SpriteRenderer>().color = new Color(255,255,255,0);
    }
}
