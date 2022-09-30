using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private MapManager _mapManager;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
            LoadMapData();
    }

    private void LoadMapData()
    {
        Debug.Log("Game Manager is Calling DataManager to load Map Data");
        var map = DataManager.LoadMapData();

        if (map != null)
        {
            Debug.Log("Map Loaded and != null");
            //_mapManager.SetMapData(map);
        }           
    }
}
