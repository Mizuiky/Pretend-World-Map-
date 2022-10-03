using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core;

public class GameManager : Singleton<GameManager>
{
    [SerializeField]
    private MapController _mapController;

    private void Start()
    {
        LoadMapData();
    }

    public void Update()
    {
        //if (Input.GetKeyDown(KeyCode.M))
        //    LoadMapData();
    }

    private void LoadMapData()
    {
        //Game Manager is Calling DataManager to load Map Data
        var map = DataManager.LoadMapData();

        if (map != null)
            _mapController.SetItems(map);       
    }
}
