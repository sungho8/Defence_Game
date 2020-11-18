using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Product : MonoBehaviour
{
    AudioSource audioSource;
    GameObject Effect;
    Controller_Stage controllerStage;
    TowerStatus status;

    public string goods;

    private void Start()
    {
        Effect = Resources.Load<GameObject>("TextEffect");
        audioSource = GetComponent<AudioSource>();
        controllerStage = GameObject.Find("BG_Field").GetComponent<Controller_Stage>();
        status = GetComponent<TowerStatus>();
    }

    public void Production()
    {
        if(goods == "Money")
        {
            controllerStage.Money += status.attack;
            GameObject temp = Instantiate(Effect, transform.position,Quaternion.identity);
            temp.transform.GetChild(0).GetComponent<TextMesh>().text = "+"+ status.attack + "$";
            temp.transform.GetChild(0).GetComponent<TextMesh>().color = new Color(244 / 255f, 194 / 255f, 104 / 255f);

            audioSource.Play();
        }
    }
}
