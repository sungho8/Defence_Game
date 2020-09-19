using System.Collections;
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
            dictionary.Add(towerName, 0);
        }
    }

    public void CheckTowerUpgrade(){
        InitDictionary();

        GameObject[] towerArr = GameObject.FindGameObjectsWithTag("Tower");
        

        if (towerArr.Length == 0)
            return;

        for (int i = 0; i < towerArr.Length; i++)
        {
            string towerName = towerArr[i].GetComponent<TowerStatus>().towerName;

            if (++dictionary[towerName] == 3)
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
