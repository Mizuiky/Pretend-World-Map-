using System;
using System.Collections.Generic;
using UnityEngine;

namespace Map
{
    [System.Serializable]
    public class MapData
    {
        [SerializeField]
        public List<ItemData> SpriteNodes;
        [SerializeField]
        public List<ItemImage> ImageDatas;

        public MapData(List<ItemData> items, List<ItemImage> images)
        {
            SpriteNodes = new List<ItemData>(items);
            ImageDatas = new List<ItemImage>(images);
        }
    }
}
