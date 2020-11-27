using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller_Shop : MonoBehaviour
{
    Storage_Tower storageTower;
    Controller_Stage controllerStage;
    Controller_Hand controllerHand;
    Controller_Tower controllerTower;
    GameObject message;
    
    public GameObject hand;
    public GameObject soldOut;  // sold out sprite

    // shop object
    public GameObject[] shopSlots;
    public GameObject[] slotName;
    public GameObject[] slotCost;

    void Start()
    {
        storageTower = GameObject.Find("Storage").GetComponent<Storage_Tower>();
        controllerStage = GameObject.Find("BG_Field").GetComponent<Controller_Stage>();
        controllerTower = GameObject.Find("BG_Field").GetComponent<Controller_Tower>();
        controllerHand = hand.GetComponent<Controller_Hand>();
        message = Resources.Load<GameObject>("Message");

        setNewItem();
    }

    void DestroyPreShop()
    {
        // 이전상점 스프라이트 삭제
        GameObject[] temp = GameObject.FindGameObjectsWithTag("ShopSprite");
        for(int i =0; i < temp.Length; i++)
        {
            Destroy(temp[i]);
        }
    }

    // 상점에 타워 생성
    public void setNewItem()
    {
        DestroyPreShop();

        List<GameObject> towers = new List<GameObject>();
        int r;

        for (int i = 0; i < storageTower.TowerList.Count; i++)
        {
            if(controllerStage.Level >= storageTower.TowerList[i].GetComponent<TowerStatus>().cost)
            {
                towers.Add(storageTower.TowerList[i]);
            }
        }

        for(int i =0; i < 5; i++)
        {
            r = Random.Range(0, towers.Count);

            GameObject temp = Instantiate(towers[r], shopSlots[i].transform.position, Quaternion.identity);
            temp.transform.SetParent(shopSlots[i].transform);
            storageTower.Tower_Shop[i] = temp;

            // name
            slotName[i].GetComponent<TextMesh>().text = temp.GetComponent<TowerStatus>().towerName;
            // cost
            slotCost[i].GetComponent<TextMesh>().text = temp.GetComponent<TowerStatus>().cost + "$";
        }
    }

    void BuyTower(int slotIndex)
    {
        // 이미 구입한 상품은 구입 불가
        if(storageTower.Tower_Shop[slotIndex] != null)
        {
            int towerCost = storageTower.Tower_Shop[slotIndex].GetComponent<TowerStatus>().cost;

            if(controllerHand.checkEmpty() == storageTower.Tower_Hand.Count)
            {
                Instantiate(message).GetComponent<Message>().MessageHand();
                return;
            }

            if (controllerStage.Money < towerCost)
            {
                Instantiate(message).GetComponent<Message>().MessageCost();
                return;
            }

            // 핸드 자리 있는지 체크 & 돈 확인
            if (controllerStage.Money >= towerCost)
            {
                GameObject shopTower = shopSlots[slotIndex].transform.GetChild(3).gameObject;
                TowerStatus status = shopTower.GetComponent<TowerStatus>();
                GameObject fieldTower = status.towerPrefab;

                // 구입한 타워 핸드로 
                controllerHand.setPurchasedTower(fieldTower);
                controllerStage.Money -= towerCost;

                // 구매한 상점은 SoldOut 처리
                Destroy(shopTower);
                GameObject sold = Instantiate(soldOut, shopSlots[slotIndex].transform.position, Quaternion.identity);
                sold.transform.SetParent(shopSlots[slotIndex].transform);
                storageTower.Tower_Shop[slotIndex] = null;
            }
        }
    }
}