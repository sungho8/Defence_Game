using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUp : MonoBehaviour
{
    public GameObject reward;
    public int cost = 5;

    private Controller_Stage controllerStage;
    private Vector3 unviewPos;
    private Vector3 viewPos;
    private bool isOn = false;
    

    private void Awake()
    {
        controllerStage = GameObject.Find("BG_Field").GetComponent<Controller_Stage>();
        unviewPos = reward.transform.position;
        viewPos = new Vector3(0, 0, 0);
    }

    public void showRewardUI()
    {
        if (isOn)
        {
            isOn = false;
            reward.transform.position = unviewPos;
        }
        else
        {
            isOn = true;
            reward.transform.localPosition = viewPos;
        }
    }

    void OnMouseUp()
    {
        if (controllerStage.Money >= cost)
        {
            controllerStage.Money -= cost;
            showRewardUI();
        }
            
        else
            Debug.Log("돈이 부족합니다");
    }
}
