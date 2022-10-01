using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Map;

public interface IMapItem
{
    public MapItemData MapItem { get; }
    public MapImageData MapImage { get; }

    public SpriteRenderer ItemSprite { get; }

    void OnDestroyComponent();
}
