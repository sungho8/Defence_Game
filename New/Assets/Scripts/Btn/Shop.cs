using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public GameObject ShopUI;
    public GameObject ReRollBtn;

    bool isShopOpen = false;
    Vector3 Point;

    private void Start()
    {
        Point = GameObject.Find("ShopPoint").transform.localPosition;
    }

    // 상점 열기
    void OnMouseUp()
    {
        if(isShopOpen == false)
        {
            isShopOpen = true;
            ShopUI.transform.localPosition = Point;
            ReRollBtn.transform.localPosition = new Vector3(-2.3f, 0, 0);
        }
        else
        {
            isShopOpen = false;
            ShopUI.transform.localPosition = new Vector3(-3200, 1000, 0);
            ReRollBtn.transform.localPosition = new Vector3(-3200, 1000, 0);
        }
    }
}