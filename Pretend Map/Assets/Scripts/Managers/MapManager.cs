using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Map;

public class MapManager
{
    private MapData _mapData;

    public void SetMapData(MapData mapData)
    {
        _mapData = mapData;
        Debug.Log("Added Map to MapManager");
    }
}
