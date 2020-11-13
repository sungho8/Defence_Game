using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller_Tile : MonoBehaviour
{
    public List<List<GameObject>> Tile;           // 모든 타일
    public List<GameObject> route;

    // Dir(left > up = down > right)
    int[] dirx = { -1, 0, 0, 1 };
    int[] diry = { 0, -1, 1, 0 };

    public GameObject StartTile;
    public GameObject EndTile;

    public GameObject StartHand;
    public GameObject EndHand;

    private Controller_Hand controllerHand;
    private Storage_Tower storageTower;
    public readonly int row = 5;
    public readonly int col = 9;
    int min;
    bool isok;

    private void Awake()
    {
        storageTower = GameObject.Find("Storage").GetComponent<Storage_Tower>();
        controllerHand = GameObject.Find("Hand").GetComponent<Controller_Hand>();
        tInit();
    }

    private void Start()
    {
        CheckInvasionRoute();
    }

    void tInit()
    {
        Tile = new List<List<GameObject>>();
        // Init All Tile
        for (int i = 1; i <= row; i++)
        {
            GameObject r = GameObject.Find("Row" + i);
            List<GameObject> list = new List<GameObject>();
            for (int j = 0; j < r.transform.childCount; j++)
            {
                list.Add(r.transform.GetChild(j).gameObject);
            }
            Tile.Add(list);
        }
    }

    void go(int cr, int cc, bool[,] visit, string sroute)
    {
        visit[cr, cc] = true;
        sroute += cr + "" + cc;
        if (cr == 0 && cc == 0 && sroute.Length < min)
        {
            min = sroute.Length;
            
            isok = true;
            route = new List<GameObject>();
            for (int i = 0; i < sroute.Length; i+=2)
            {
                int r = sroute[i] - '0';
                int c = sroute[i + 1] - '0';
                route.Add(Tile[r][c]);
            }
            return;
        }

        for (int i = 0; i < dirx.Length; i++)
        {
            int nr = cr + diry[i];
            int nc = cc + dirx[i];
            if (nr < row && nc < col && nr >= 0 && nc >= 0 && !visit[nr, nc] && storageTower.Tower_Field[nr * col + nc] == null)
            {
                go(nr, nc, visit, sroute);
                visit[cr, cc] = false;
            }
        }
    }

    public bool CheckInvasionRoute()
    {
        min = 99;
        bool[,] visit = new bool[5, 9];
        isok = false;
        string sroute = "";
        go(row - 1, col - 1, visit, sroute);

        for(int i =0; i < Tile.Count; i++)
        {
            for(int j = 0; j < Tile[0].Count; j++)
            {
                Tile[i][j].GetComponent<SpriteRenderer>().color = new Color(1, 1, 1,67/255f);
            }
        }

        if (isok)
        {
            for(int i = 0; i < route.Count; i++)
            {
                route[i].GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 20/255f);
            }
        }

        return isok;
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
                for(int j = 0; j < Tile[0].Count; j++)
                {
                    offset = Tile[i][j].transform.position - t.position;
                    //&& CheckInvasionRoute(Tile[i][j])
                    if (min > offset.sqrMagnitude && storageTower.Tower_Field[i] == null)
                    {
                        min = offset.sqrMagnitude;
                        result = i * col + j;
                    }
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
                    result = - 2 - i;
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
