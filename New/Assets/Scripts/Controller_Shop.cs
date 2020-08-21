using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller_Shop : MonoBehaviour
{
    Storage_Tower storageTower;

    public GameObject[] shopSlots;

    void Start()
    {
        storageTower = GameObject.Find("Storage").GetComponent<Storage_Tower>();

        setNewItem();
    }

    void setNewItem()
    {
        List<GameObject> towers = storageTower.TowerList;

        int r;

        for(int i =0; i < 5; i++)
        {
            r = Random.Range(0,towers.Count);

            GameObject temp = Instantiate(towers[r], shopSlots[i].transform.position, Quaternion.identity);

        }
    }
}
