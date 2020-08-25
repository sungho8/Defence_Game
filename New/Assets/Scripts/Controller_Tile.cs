using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller_Tile : MonoBehaviour
{
    public List<GameObject> InvasionRoute;  // 공격로
    public List<GameObject> Tile;           // 모든 타일
    public List<GameObject> ArrangedTower;  // 배치된 타워들

    private readonly int tileCount = 45;

    private void Awake()
    {
        // Init All Tile
        for (int i = 1; i <= tileCount; i++)
        {
            Tile.Add(GameObject.Find("Tile" + i));
            ArrangedTower.Add(null);
        }
    }

    // 해당타일이 공격로인지 확인
    public bool CheckInvasionRoute(GameObject t)
    {
        for(int i =0; i < InvasionRoute.Count; i++)
        {
            if(InvasionRoute[i] == t)
            {
                return false;   // 공격로라면 return false
            }
        }
        return true;
    }

    public int CheckClosedTile(Transform t)
    {
        Vector2 offset;

        int result = 0;
        float min = 10f;

        for (int i =0; i < Tile.Count; i++)
        {
            offset = Tile[i].transform.position - t.position;

            if (min > offset.sqrMagnitude && CheckInvasionRoute(Tile[i]) && ArrangedTower[i] == null)
            {
                min = offset.sqrMagnitude;
                result = i;
            }
        }
        return result;
    }
}
