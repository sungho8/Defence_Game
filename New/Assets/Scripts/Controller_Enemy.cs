using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller_Enemy : MonoBehaviour
{
    public GameObject Sponer;
    public List<GameObject> Enemys;
    public GameObject EnemyObject;

    public int RemainingEnemyCount = 30;

    private GameObject temp;
    private float time = 0;
    private float delay = 1f;


    
    private void Update()
    {
        time += Time.deltaTime;

        Debug.Log(RemainingEnemyCount);
        if(time > 1f && RemainingEnemyCount > 0)
        {
            time = 0;
            temp = Instantiate(EnemyObject,Sponer.transform.position,Quaternion.identity);
            Enemys.Add(temp);

            RemainingEnemyCount--;
        }
    }
}
