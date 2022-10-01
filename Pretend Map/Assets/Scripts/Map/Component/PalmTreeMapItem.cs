using Map;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PalmTreeMapItem : MapItemBase
{
    public PalmTreeMapItem(MapItemData itemData, MapImageData imageData) : base(itemData, imageData)
    {
        _itemData = itemData;
        _imageData = imageData;
    }
}
