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
    int[,] cost;

    public GameObject StartTile;
    public GameObject EndTile;

    public GameObject StartHand;
    public GameObject EndHand;

    private Controller_Hand controllerHand;
    private Controller_Enemy controllerEnemy;
    private Storage_Tower storageTower;
    private GameObject message;
    public readonly int row = 5;
    public readonly int col = 9;
    int min;
    bool isok;

    int startRow = 4;
    int endRow = 0;

    private void Awake()
    {
        storageTower = GameObject.Find("Storage").GetComponent<Storage_Tower>();
        controllerHand = GameObject.Find("Hand").GetComponent<Controller_Hand>();
        controllerEnemy = GameObject.Find("BG_Field").GetComponent<Controller_Enemy>();
        message = Resources.Load<GameObject>("Message");
        cost = new int[row, col];
        tInit();
    }

    private void Start()
    {
        ShuffleRoute();
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

    void cInit()
    {
        for(int i =0; i < row; i++)
        {
            for(int j =0; j < col; j++)
            {
                cost[i,j] = 500;
            }
        }
    }

    void go(int depth, int cr, int cc, string sroute)
    {
        cost[cr, cc] = depth;
        sroute += cr + "" + cc;
        if (cr == endRow && cc == 0 && sroute.Length < min)
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
            // row,col 이내에 cost값 판단하면서 경로 이동
            if (nr < row && nc < col && nr >= 0 && nc >= 0 && cost[nr, nc] >= depth && storageTower.Tower_Field[nr * col + nc] == null)
            {
                go(depth + 1, nr, nc, sroute);
            }
        }
    }

    public void ShuffleRoute()
    {
        startRow = Random.Range(0, 5);
        endRow = Random.Range(0, 5);

        controllerEnemy.Sponer.transform.position = Tile[startRow][col - 1].transform.position;
        controllerEnemy.Goal.transform.position = Tile[endRow][0].transform.position;
        CheckInvasionRoute();
    }

    public bool CheckInvasionRoute()
    {
        //bool[,] visit = new bool[row, col];
        string sroute = "";
        cInit();
        min = 99;
        isok = false;
        if (storageTower.Tower_Field[startRow * col + col - 1] == null) 
            go(0, startRow, col - 1,  sroute);

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
        else{
            Instantiate(message).GetComponent<Message>().MessageRoute();
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
                    if (min > offset.sqrMagnitude && storageTower.Tower_Field[i * col + j] == null)
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

        //Debug.Log(result);
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
