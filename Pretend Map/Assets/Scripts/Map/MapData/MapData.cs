using System;
using System.Collections.Generic;
using UnityEngine;

namespace Map
{
    [System.Serializable]
    public class MapData
    {
        [SerializeField]
        public List<MapItemData> SpriteNodes;
        [SerializeField]
        public List<MapImageData> ImageDatas;

        public MapData(List<MapItemData> items, List<MapImageData> images)
        {
            SpriteNodes = new List<MapItemData>(items);
            ImageDatas = new List<MapImageData>(images);
        }
    }
}
