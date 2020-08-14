using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller_Enemy : MonoBehaviour
{
    public GameObject Sponer;
    public List<GameObject> Enemys;
    public GameObject EnemyObject;

    private float time = 0;
    private float delay = 1f;

    private void Update()
    {
        time += Time.deltaTime;

        if(time < 1000f)
        {
            time = 0;
            Instantiate(EnemyObject,Sponer.transform.position,Quaternion.identity);
        }
    }
}
