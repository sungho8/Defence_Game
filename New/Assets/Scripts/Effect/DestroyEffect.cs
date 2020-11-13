using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyEffect : MonoBehaviour
{
    void EndDestroy(float delay)
    {
        Destroy(this.gameObject, delay);
    }
}