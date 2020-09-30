using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller_Tile : MonoBehaviour
{
    public List<GameObject> InvasionRoute;  // 공격로
    public List<GameObject> Tile;           // 모든 타일

    public GameObject StartTile;
    public GameObject EndTile;

    public GameObject StartHand;
    public GameObject EndHand;

    private Controller_Hand controllerHand;
    private Storage_Tower storageTower;
    private readonly int tileCount = 45;

    private void Awake()
    {
        storageTower = GameObject.Find("Storage").GetComponent<Storage_Tower>();
        controllerHand = GameObject.Find("Hand").GetComponent<Controller_Hand>();
        
        // Init All Tile
        for (int i = 1; i <= tileCount; i++)
        {
            Tile.Add(GameObject.Find("Tile" + i));
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

        int result = -1;
        float min = 10f;

        // 필드 안
        if(IsField(t))
        {
            for (int i = 0; i < Tile.Count; i++)
            {
                offset = Tile[i].transform.position - t.position;
                if (min > offset.sqrMagnitude && CheckInvasionRoute(Tile[i]) && storageTower.Tower_Field[i] == null)
                {
                    min = offset.sqrMagnitude;
                    result = i;
                }
            }
        }
        // 핸드영역
        else if(IsHand(t))
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

    bool IsField(Transform t)
    {
        float StartX = StartTile.transform.position.x - 1;
        float StartY = StartTile.transform.position.y + 1;
        float EndX = EndTile.transform.position.x + 1;
        float EndY = EndTile.transform.position.y - 1;

        if (t.position.x > StartX && t.position.x < EndX && t.position.y < StartY && t.position.y > EndY){
            return true;
        }
        return false;
    }
    bool IsHand(Transform t)
    {
        float StartX = StartHand.transform.position.x - 1;
        float StartY = StartHand.transform.position.y + 1;
        float EndX = EndHand.transform.position.x + 1;
        float EndY = EndHand.transform.position.y - 1;

        if (t.position.x > StartX && t.position.x < EndX && t.position.y < StartY && t.position.y > EndY){
            return true;
        }
        return false;
    } 
}
