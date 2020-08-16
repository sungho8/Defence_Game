using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller_Tile : MonoBehaviour
{
    public List<GameObject> InvasionRoute;  // 공격로
    public List<GameObject> Tile;           // 모든 타일

    private readonly int tileCount = 45;

    private void Awake()
    {
        // Init All Tile
        for (int i = 1; i <= tileCount; i++) 
            Tile.Add(GameObject.Find("Tile" + i));
    }
}
