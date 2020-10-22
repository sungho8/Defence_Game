using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUp : MonoBehaviour
{
    public GameObject reward;
    Controller_Stage controllerStage;

    private void Awake()
    {
        controllerStage = GameObject.Find("BG_Field").GetComponent<Controller_Stage>();
    }

    void OnMouseUp()
    {
        
    }
}
