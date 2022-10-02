using Map;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapItemBase : MonoBehaviour, IMapItem
{
    protected ItemData _itemData;
    protected SpriteRenderer _itemSpriteRenderer;
    protected Vector3 _itemPosition;

    private Sprite _sprite;
    private Vector3 _itemScale = new Vector3(1f, 1f, 0);
    private PolygonCollider2D _collider2D;

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
        get => _itemSpriteRenderer;
    }

    public PolygonCollider2D Collider2D
    {
        get => _collider2D;
    }

    #endregion

    public void Init(ItemData itemData, Sprite sprite, int layer)
    {
        _itemData = itemData;
        _sprite = sprite;

        SetSprite();
        SetPosition();
        SetScale();
        UpdateCollider();
        SetLayer(layer);      
    }

    private void SetSprite()
    {
        _itemSpriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _itemSpriteRenderer.sprite = _sprite;
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
        _itemSpriteRenderer.sortingOrder = layer;
    }

    public void UpdateCollider()
    {
        _collider2D = GetComponent<PolygonCollider2D>();
        UpdateColliderFormat();
    }

    public void UpdateColliderFormat()
    {
        //By Unity: PhysicsShape are a sequence of line segments between points that define the outline of the Sprite
        _collider2D.pathCount = _sprite.GetPhysicsShapeCount();

        //By Unity: A path is a cyclic sequence of line segments between points that define the outline of the polygon
        List<Vector2> path = new List<Vector2>();

        //Getting the line segments of the new sprite and adding into the polygon collider 2D to make it;
        for(int i = 0; i < _collider2D.pathCount; i++)
        {
            path.Clear();

            _sprite.GetPhysicsShape(i, path);
            _collider2D.SetPath(i, path.ToArray());
        }
    }

    public virtual void OnDestroyComponent()
    {
        Destroy(gameObject);
    }
}
