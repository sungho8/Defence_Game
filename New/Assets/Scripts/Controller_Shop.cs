using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller_Shop : MonoBehaviour
{
    Storage_Tower storageTower;

    public GameObject hand;
    public GameObject soldOut;
    public GameObject[] shopSlots;
    public GameObject[] slotName;
    public GameObject[] slotCost;

    void Start()
    {
        storageTower = GameObject.Find("Storage").GetComponent<Storage_Tower>();

        setNewItem();
    }

    // 상점에 타워 생성
    void setNewItem()
    {
        List<GameObject> towers = storageTower.TowerList;

        int r;

        for(int i =0; i < 5; i++)
        {
            r = Random.Range(0,towers.Count);

            GameObject temp = Instantiate(towers[r], shopSlots[i].transform.position, Quaternion.identity);
            temp.transform.SetParent(shopSlots[i].transform);

            // name
            slotName[i].GetComponent<TextMesh>().text = temp.GetComponent<TowerStatus>().towerName;

            // cost
            slotCost[i].GetComponent<TextMesh>().text = temp.GetComponent<TowerStatus>().cost + "$";
        }
    }

    void BuyTower(int slotIndex)
    {
        Controller_Hand controllerHand = hand.GetComponent<Controller_Hand>();

        if (controllerHand.checkEmpty() != controllerHand.towers.Length)
        {
            GameObject shopTower = shopSlots[slotIndex].transform.GetChild(3).gameObject;
            TowerStatus status = shopTower.GetComponent<TowerStatus>();
            GameObject fieldTower = status.towerPrefab;

            controllerHand.setPurchasedTower(fieldTower);

            // 구매한 상점은 SoldOut 처리
            Destroy(shopTower);
            GameObject sold = Instantiate(soldOut, shopSlots[slotIndex].transform.position, Quaternion.identity);
            sold.transform.SetParent(shopSlots[slotIndex].transform);
        }
    }
}
