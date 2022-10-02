using Map;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Map
{
    public class MapItem
    {
        public ItemData _itemData;
        public Sprite _itemSprite;

        public MapItem(ItemData itemData, Sprite sprite)
        {
            _itemData = itemData;
            _itemSprite = sprite;
        }
    }
}

