using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public GameObject ShopUI;

    bool isShopOpen = false;

    private void Start()
    {
        
    }

    // 스테이지 시작
    void OnMouseUp()
    {
        if(isShopOpen == false)
        {
            isShopOpen = true;
            ShopUI.transform.localPosition = new Vector3(0, 475, 0);
        }
        else
        {
            isShopOpen = false;
            ShopUI.transform.localPosition = new Vector3(0, 1000, 0);
        }
    }
}