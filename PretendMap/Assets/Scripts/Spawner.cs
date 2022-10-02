using Map;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private Transform _itemContainer;

    private int _layerCount;

    public void SpawnItems<T> (List<T> spawnerList, GameObject prefab) where T : MapItem
    {
        //The first object will receive the higher layer and each next item are behind it.
        _layerCount = spawnerList.Count - 1;

        foreach (T data in spawnerList)
        {
            var item = Instantiate(prefab, _itemContainer);

            if (item != null)
            {
                var mapItem = item.GetComponent<MapItemBase>();

                if (mapItem != null)
                {
                    mapItem.Init(data._itemData, data._itemSprite, _layerCount);
                    _layerCount--;
                }               
            }
        }
    }
}
