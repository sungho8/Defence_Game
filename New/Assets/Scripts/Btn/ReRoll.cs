using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReRoll : MonoBehaviour
{
    Controller_Shop controllerShop;
    Controller_Stage controllerStage;
    AudioSource audioSource;
    private void Awake()
    {
        controllerShop = GameObject.Find("BG_Shop").GetComponent<Controller_Shop>();
        controllerStage = GameObject.Find("BG_Field").GetComponent<Controller_Stage>();
        audioSource = GetComponent<AudioSource>();
    }
    void OnMouseUp()
    {
        if(controllerStage.Money >= 2)
        {
            controllerStage.Money -= 2;
            audioSource.Play();
            controllerShop.setNewItem();
        }
        
    }
}
