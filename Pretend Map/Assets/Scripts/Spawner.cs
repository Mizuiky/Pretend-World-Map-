using Map;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private Transform _itemContainer;

    public void SpawnItems<T> (List<T> spawnerList, GameObject prefab) where T : MapItem
    {
        foreach (T data in spawnerList)
        {
            var item = Instantiate(prefab, _itemContainer);

            if (item != null)
            {
                var mapItem = item.GetComponent<MapItemBase>();

                if (mapItem != null)
                    mapItem.Init(data._itemData, data._itemSprite);
            }
        }
    }
}
