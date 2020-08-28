using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller_Tile : MonoBehaviour
{
    public List<GameObject> InvasionRoute;  // 공격로
    public List<GameObject> ArrangedTower;  // 배치된 타워들
    public List<GameObject> Tile;           // 모든 타일

    public GameObject StartTile;
    public GameObject EndTile;

    public GameObject StartHand;
    public GameObject EndHand;

    private Controller_Hand controllerHand;
    private readonly int tileCount = 45;

    private void Awake()
    {
        // Init All Tile
        for (int i = 1; i <= tileCount; i++)
        {
            Tile.Add(GameObject.Find("Tile" + i));
            ArrangedTower.Add(null);
        }
        controllerHand = GameObject.Find("Hand").GetComponent<Controller_Hand>();
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

        int result = -1;
        float min = 10f;

        // 필드 안
        if(t.position.x > StartTile.transform.position.x && t.position.x < EndTile.transform.position.x &&
            t.position.y < StartTile.transform.position.y && t.position.y > EndTile.transform.position.y)
        {
            for (int i = 0; i < Tile.Count; i++)
            {
                offset = Tile[i].transform.position - t.position;

                if (min > offset.sqrMagnitude && CheckInvasionRoute(Tile[i]) && ArrangedTower[i] == null)
                {
                    min = offset.sqrMagnitude;
                    result = i;
                }
            }
        }
        // 핸드영역
        else if(t.position.x > StartHand.transform.position.x && t.position.x < EndHand.transform.position.x &&
        t.position.y < StartHand.transform.position.y && t.position.y > EndHand.transform.position.y)
        {
           
            for (int i = 0; i < controllerHand.handSlots.Length; i++)
            {
                offset = controllerHand.handSlots[i].transform.position - t.position;

                if (min > offset.sqrMagnitude)
                {
                    min = offset.sqrMagnitude;
                    result = -2 - i;
                }
            }
        }
        return result;
    }
}
