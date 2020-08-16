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
    private Controller_Stage controllerStage;
    private float time = 0;
    private float delay = 1f;

    private void Start()
    {
        controllerStage = GetComponent<Controller_Stage>();
    }

    private void Update()
    {
        time += Time.deltaTime;

        if(time > 1f)
            SummonEnemy();
    }

    private void SummonEnemy()
    {
        // 스테이지 진행중 몬스터 소환
        if (RemainingEnemyCount > 0 && controllerStage.isStart)
        {
            time = 0;
            temp = Instantiate(EnemyObject, Sponer.transform.position, Quaternion.identity);
            Enemys.Add(temp);

            RemainingEnemyCount--;
        }
        // 해당 스테이지 클리어!
        else if(controllerStage.isStart == true && RemainingEnemyCount == 0)
        {
            controllerStage.CurrenStageClear();
            RemainingEnemyCount = 30;
        }
    }
}
