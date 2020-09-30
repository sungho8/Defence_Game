using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenceFail : MonoBehaviour
{
    Controller_Stage controllerStage;
    private void Start()
    {
        controllerStage = GameObject.Find("BG_Field").GetComponent<Controller_Stage>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            //방어실패
            if (controllerStage.HP > 0)
            {
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
