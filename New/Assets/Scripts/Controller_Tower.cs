using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 시너지 및 타워 업그레이드 관리 스크립트

public class Controller_Tower : MonoBehaviour
{
    public GameObject Effect;
    public Dictionary<string, int> dictionaryUpgrade;
    public Dictionary<string, int> dictionarySynergy;

    Storage_Tower storageTower;

    private void Start()
    {
        storageTower = GameObject.Find("Storage").GetComponent<Storage_Tower>();
        CheckTower();
    }

    void InitDictionary()
    {
        dictionaryUpgrade = new Dictionary<string, int>();
        dictionarySynergy = new Dictionary<string, int>();
        for (int i = 0; i < storageTower.TowerList.Count; i++)
        {
            TowerStatus t = storageTower.TowerList[i].GetComponent<TowerStatus>();
            string towerName = t.towerName;
            string towerElement = t.element;
            string towerType = t.type;

            for (int j = 0; j < 3; j++)
            {
                dictionaryUpgrade.Add(j + towerName, 0);
            }

            if(!dictionarySynergy.ContainsKey(towerElement))
                dictionarySynergy.Add(towerElement, 0);

            if (!dictionarySynergy.ContainsKey(towerType))
                dictionarySynergy.Add(towerType, 0);
        }
    }

    public void CheckTower()
    {
        InitDictionary();

        GameObject[] towerArr = GameObject.FindGameObjectsWithTag("Tower");
        List<GameObject> field = new List<GameObject>();
        for (int i = 0; i < towerArr.Length; i++) 
        {
            if(towerArr[i].GetComponent<TowerStatus>().currentState == "Field")
            {
                field.Add(towerArr[i]);
            }
        }

        if (towerArr.Length == 0)
            return;

        CheckTowerUpgrade(towerArr);
        CheckTowerSynergy(field);
    }

    void CheckTowerUpgrade(GameObject[] towerArr)
    {
        for (int i = 0; i < towerArr.Length; i++)
        {
            int grade = towerArr[i].GetComponent<TowerStatus>().grade;
            string towerName = towerArr[i].GetComponent<TowerStatus>().towerName;

            if (grade < 3 && ++dictionaryUpgrade[grade + towerName] == 3)
            {
                List<GameObject> uTowers = new List<GameObject>();
                for (int j = 0; j < towerArr.Length; j++)
                {
                    if (towerArr[j].GetComponent<TowerStatus>().towerName.CompareTo(towerName) == 0 &&
                        towerArr[j].GetComponent<TowerStatus>().grade == grade)
                    {
                        uTowers.Add(towerArr[j]);
                    }
                }

                TowerUpgrade(uTowers);
                CheckTower();
            }
        }
    }

    void TowerUpgrade(List<GameObject> uTower)
    {
        // 업그레이드 될 타워
        // EnergyField5_0 이펙트
        Instantiate(Effect, uTower[0].transform.position, Quaternion.identity);
        uTower[0].GetComponent<TowerStatus>().UpgradeTower();

        // 재료 타워
        uTower[1].GetComponent<TowerStatus>().DestroyTower();
        uTower[2].GetComponent<TowerStatus>().DestroyTower();
    }

    public void CheckTowerSynergy(List<GameObject> towerArr)
    {
        List<string> list = new List<string>();

        for (int i = 0; i < towerArr.Count; i++) 
        {
            TowerStatus t = towerArr[i].GetComponent<TowerStatus>();

            if (!list.Contains(t.towerName))
            {
                string towerElement = t.element;
                string towerType = t.type;

                ++dictionarySynergy[towerElement];
                ++dictionarySynergy[towerType];
                list.Add(t.towerName);
            }
        }
    }
}