﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller_Tower : MonoBehaviour
{
    public GameObject Effect;

    Storage_Tower storageTower;
    Dictionary<string, int> dictionary;

    private void Start(){
        storageTower = GameObject.Find("Storage").GetComponent<Storage_Tower>();

        CheckTowerUpgrade();
    }

    void InitDictionary()
    {
        dictionary = new Dictionary<string, int>();
        for (int i = 0; i < storageTower.TowerList.Count; i++)
        {
            string towerName = storageTower.TowerList[i].GetComponent<TowerStatus>().towerName;
            for (int j = 0; j < 3; j++)
            {
                dictionary.Add(j + towerName, 0);
                Debug.Log("init" + j+towerName);
            }
        }
    }

    public void CheckTowerUpgrade(){
        InitDictionary();

        GameObject[] towerArr = GameObject.FindGameObjectsWithTag("Tower");

        if (towerArr.Length == 0)
            return;

        for (int i = 0; i < towerArr.Length; i++)
        {
            int grade = towerArr[i].GetComponent<TowerStatus>().grade;
            string towerName = towerArr[i].GetComponent<TowerStatus>().towerName;
            Debug.Log("towerUpgrade"  + towerName);
            if (grade < 3 && ++dictionary[grade + towerName] == 3)
            {
                List<GameObject> uTowers = new List<GameObject>();
                for (int j = 0; j < towerArr.Length; j++) 
                {
                    if (towerArr[j].GetComponent<TowerStatus>().towerName.CompareTo(towerName) == 0)
                    {
                        uTowers.Add(towerArr[j]);
                    }
                }

                TowerUpgrade(uTowers);
            }
        }
    }

    void TowerUpgrade(List<GameObject> uTower)
    {
        Debug.Log("와우 2성 타워!");

        // 업그레이드 될 타워
        // EnergyField5_0 이펙트
        Instantiate(Effect, uTower[0].transform.position, Quaternion.identity);
        uTower[0].GetComponent<TowerStatus>().UpgradeTower();


        // 재료 타워
        // 1초동안 연해짐
        // 1초뒤 Destroy
        uTower[1].GetComponent<TowerStatus>().DestroyTower();
        uTower[2].GetComponent<TowerStatus>().DestroyTower();
    }
}
