using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Map;
using System.IO;

public static class DataManager
{
    #region File Settings

    //let this data visible using a new class or scriptable object because this can change later

    //To find the %AppData% use windown + R key to open execute and search for the following path: C:..\AppData\LocalLow\DefaultCompany\PretendMap\MapSaveData
    public static string directory = Application.persistentDataPath + "/MapSaveData/";

    public static string mapFileName = "WorldMapData.txt";

    #endregion

    public static MapData LoadMapData()
    {
        Debug.Log("load World Map Data");

        string fullPath = directory + mapFileName;

        List<MapItemData> mapItems = new List<MapItemData>();
        List<MapImageData> mapImages = new List<MapImageData>();

        MapData data = new MapData(mapItems, mapImages);

        if (File.Exists(fullPath))
        {
            string json = File.ReadAllText(fullPath);
            data = JsonUtility.FromJson<MapData>(json);

            Debug.Log("Loaded data from WorldMapData.txt");

            return data;
        }

        Debug.Log("Couldn't find WorldMapDataFile.txt");

        return null;
    }

    public static void SaveData()
    {

    }
}
