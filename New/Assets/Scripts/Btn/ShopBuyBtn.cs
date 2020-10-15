using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopBuyBtn : MonoBehaviour
{
    public int index;

    Controller_Shop controllerShop;
    AudioSource audioSource;

    private void Start()
    {
        controllerShop = GameObject.Find("BG_Shop").GetComponent<Controller_Shop>();
        audioSource = GetComponent<AudioSource>();
    }
    void OnMouseUp()
    {
        audioSource.Play();
        controllerShop.SendMessage("BuyTower",index);
    }
}
