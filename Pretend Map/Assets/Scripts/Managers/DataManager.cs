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
        string fullPath = directory + mapFileName;
        Debug.Log("Directory path " + fullPath);

        List<MapItemData> mapItems = new List<MapItemData>();
        List<MapImageData> mapImages = new List<MapImageData>();

        MapData mapData = new MapData(mapItems, mapImages);

        if(mapData != null)
        {
            if (File.Exists(fullPath))
            {
                string json = File.ReadAllText(fullPath);
                mapData = JsonUtility.FromJson<MapData>(json);

                if(mapData != null)
                    return mapData;          
            }  
        }
        return null;
    }

    public static void SaveMapData() { }
}
