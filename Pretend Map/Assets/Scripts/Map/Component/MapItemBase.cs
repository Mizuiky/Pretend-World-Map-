using Map;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapItemBase : MonoBehaviour, IMapItem
{
    protected Transform _position;

    protected MapItemData _itemData;
    protected MapImageData _imageData;
    protected SpriteRenderer _itemSprite;

    #region Properties

    public Transform ComponentPosition 
    {
        get => _position;       
    }
    public MapItemData MapItem 
    {
        get => _itemData;
    }

    public MapImageData MapImage 
    {
        get => _imageData;
    }

    public SpriteRenderer ItemSprite 
    {
        get => _itemSprite;
    }

    #endregion

    public MapItemBase(MapItemData itemData, MapImageData imageData)
    {
        _itemData = itemData;
        _imageData = imageData;
    }

    public virtual void OnDestroyComponent()
    {
        Destroy(gameObject);
    }
}
