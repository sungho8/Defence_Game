﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopBuyBtn : MonoBehaviour
{
    public int index;

    Controller_Shop controllerShop;

    private void Start()
    {
        controllerShop = GameObject.Find("BG_Shop").GetComponent<Controller_Shop>();
    }
    void OnMouseUp()
    {
        controllerShop.SendMessage("BuyTower",index);
    }
}