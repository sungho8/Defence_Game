using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyEffect : MonoBehaviour
{
    void EndDestroy()
    {
        Destroy(this.gameObject, 0.3f);
    }
}