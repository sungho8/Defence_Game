using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpUp : MonoBehaviour
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
        controllerStage.HP += 2;
        controllerStage.Level += 1;
        levelBtn.showRewardUI();
    }
}
