using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenceFail : MonoBehaviour
{
    Controller_Stage controllerStage;
    Controller_Enemy controllerEnemy;
    private void Start()
    {
        controllerStage = GameObject.Find("BG_Field").GetComponent<Controller_Stage>();
        controllerEnemy = GameObject.Find("BG_Field").GetComponent<Controller_Enemy>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            //방어실패
            if (controllerStage.HP > 0)
            {
                controllerEnemy.RemoveEnemy(other.gameObject);
                controllerStage.HP--;
            }
            else
            {
                Debug.Log("GameOver");
            }
            Destroy(other.gameObject);
        }
    }
}
