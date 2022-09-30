using System;
using System.Collections.Generic;

namespace Map
{
    [Serializable]
    public class MapData
    {
        public List<MapItemData> mapItems;
        public List<MapImageData> mapImages;

        public MapData(List<MapItemData> items, List<MapImageData> images)
        {
            mapItems = new List<MapItemData>(items);
            mapImages = new List<MapImageData>(images);
        }
    }
}
