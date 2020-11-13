using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaUp : MonoBehaviour
{
    private Controller_Stage controllerStage;
    private LevelUp levelBtn;
    // Start is called before the first frame update
    void Start()
    {
        controllerStage = GameObject.Find("BG_Field").GetComponent<Controller_Stage>();
        levelBtn = GameObject.Find("Btn_LevelUp").GetComponent<LevelUp>();
    }

    void OnMouseUp()
    {
        Debug.Log("마나업 클릭");
        controllerStage.Level += 1;
        controllerStage.manaMax += 2;
        controllerStage.Mana += 2;
        levelBtn.showRewardUI();
    }
}
