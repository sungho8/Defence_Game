using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Storage_Tower : MonoBehaviour
{

    public List<GameObject> TowerList;  // 상점에 사용되는 전체 타워 리스트

    // 상점, 핸드, 필드에 있는 타워
    public List<GameObject> Tower_Shop;     // controllerShop 에 shopSlotTowers
    public List<GameObject> Tower_Hand;     // controllerHand 에 towers
    public List<GameObject> Tower_Field;    // controllerTile 에 arranged Tower
}
