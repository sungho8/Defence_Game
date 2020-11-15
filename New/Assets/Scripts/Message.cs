using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Message : MonoBehaviour
{
    public GameObject text;

    private void Start()
    {
        transform.localScale = new Vector3(0.5f, 0.5f, 0);
    }

    public void SetMessage(string str, Vector3 pos, Color col)
    {
        text.GetComponent<TextMesh>().text = str;
        text.GetComponent<TextMesh>().color = col;
        transform.position = pos;
    }

    public void MessageMana()
    {
        text.GetComponent<TextMesh>().text = "마나가 부족합니다";
        text.GetComponent<TextMesh>().color = new Color(81 / 255f, 205 / 255f, 227 / 255f);
        transform.position = new Vector3(-4, 2, 0);
    }

    public void MessageHand()
    {
        text.GetComponent<TextMesh>().text = "핸드가 꽉찼습니다";
        text.GetComponent<TextMesh>().color = new Color(0.7f, 0.7f, 0.7f);
        transform.position = new Vector3(7, -2.8f, 0);
    }

    public void MessageCost()
    {
        text.GetComponent<TextMesh>().text = "골드가 부족합니다";
        text.GetComponent<TextMesh>().color = new Color(244 / 255f, 194 / 255f, 104 / 255f);
        transform.position = new Vector3(7, -2.8f, 0);
    }
}
