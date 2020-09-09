using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenceFail : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            Debug.Log("hp - 1");
        }
    }
}
