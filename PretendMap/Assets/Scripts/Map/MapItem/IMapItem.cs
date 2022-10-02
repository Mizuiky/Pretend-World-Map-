using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Map;

public interface IMapItem
{
    public ItemData ItemData { get; }

    public SpriteRenderer ItemSprite { get; }

    public PolygonCollider2D Collider2D { get; }

    void OnDestroyComponent();
}
