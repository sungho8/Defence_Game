using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public GameObject ShopUI;

    bool isShopOpen = false;

    // 상점 열기
    void OnMouseUp()
    {
        if(isShopOpen == false)
        {
            isShopOpen = true;
            ShopUI.transform.localPosition = new Vector3(0, 200, 0);
        }
        else
        {
            isShopOpen = false;
            ShopUI.transform.localPosition = new Vector3(-3200, 1000, 0);
        }
    }
}