using Map;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapItemBase : MonoBehaviour, IMapItem
{
    protected ItemData _itemData;
    protected SpriteRenderer _itemSprite;
    protected Vector3 _itemPosition;

    private Sprite _sprite;
    private Vector3 _itemScale = new Vector3(0.2f, 0.2f, 0);

    #region Properties

    public Vector2 ItemPosition 
    {
        get => _itemPosition;       
    }
    public ItemData ItemData 
    {
        get => _itemData;
    }

    public SpriteRenderer ItemSprite 
    {
        get => _itemSprite;
    }

    #endregion

    public void Init(ItemData itemData, Sprite sprite)
    {
        _itemData = itemData;
        _sprite = sprite;

        SetSprite();
        SetPosition();
        SetScale();
    }

    private void SetSprite()
    {
        _itemSprite = GetComponentInChildren<SpriteRenderer>();
        _itemSprite.sprite = _sprite;
    }

    private void SetPosition()
    {
        _itemPosition = new Vector3(_itemData.PosX, _itemData.PosY, transform.position.z);
        transform.position = _itemPosition;
    }

    private void SetScale()
    {
        transform.localScale = new Vector3(_itemScale.x, _itemScale.y, _itemScale.z);
    }

    public void SetLayer(int layer)
    {
        _itemSprite.sortingOrder = layer;
    }

    public virtual void OnDestroyComponent()
    {
        Destroy(gameObject);
    }
}
