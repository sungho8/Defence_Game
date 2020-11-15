using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller_Message : MonoBehaviour
{
    void makeMessage()
    {
        Instantiate(Resources.Load("Message"));
    }
}
