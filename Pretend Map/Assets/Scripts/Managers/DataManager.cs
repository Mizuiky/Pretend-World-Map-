using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Map;

public static class DataManager
{
    #region File Settings

    //let this data visible using a new class or scriptable object because this can change later

    //To find the %AppData% use windown + R key to open execute and search for the following path: C:..\AppData\LocalLow\DefaultCompany\PretendMap\MapSaveData
    public static string directory = Application.persistentDataPath + "/MapSaveData/";

    public static string mapFileName = "WorldMapData.json";

    #endregion

    public static MapData LoadMapData()
    {
        Debug.Log("load World Map Data");

        string fullPath = directory + mapFileName;

        Debug.Log("Directory path " + fullPath);

        List<MapItemData> mapItems = new List<MapItemData>();
        List<MapImageData> mapImages = new List<MapImageData>();

        MapData mapData = new MapData(mapItems, mapImages);

        if(mapData != null)
        {
            Debug.Log("Data != null");

            if (File.Exists(fullPath))
            {
                Debug.Log("Full path exists " + fullPath);

                string json = File.ReadAllText(fullPath);
                mapData = JsonUtility.FromJson<MapData>(json);

                if(mapData != null)
                {
                    Debug.Log("Loaded data from WorldMapData.json");
                    return mapData;
                }            
            }

            Debug.Log("Couldn't find WorldMapDataFile.json");
        }

        Debug.Log("Data NULL");
        return null;
    }

    public static void SaveMapData()
    {

    }
}
