using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Map;
using System.Linq;
using System;

public class MapController : MonoBehaviour
{
    private List<MapItemBase> _mapItemList;

    private Dictionary<int, Sprite> _mapImages;

    private List<Sprite> _mapSprites;

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        _mapItemList = new List<MapItemBase>();
        _mapImages = new Dictionary<int, Sprite>();
    }

    public void SetMapItems(MapData mapData)
    {
        LoadSpriteResorces();
        InitializeMapImagesDictionary(mapData.ImageDatas);
    }
    public void InitializeMapImagesDictionary(List<MapImageData> imageData)
    {
        foreach(MapImageData data in imageData)
        {
            var newimage = GetMapSprite(data.Name);

            if(newimage != null)
                _mapImages.Add(data.ImageID, newimage);

            Debug.LogFormat("ImageID: {0}  SpriteName: {1} ", data.ImageID, newimage.name);
        }
    }

    public void InitializeMapItemList(MapData mapData)
    {
        var mapList = mapData;

    }

    public void LoadSpriteResorces()
    {
        _mapSprites = Resources.LoadAll("Props", typeof(Sprite)).Cast<Sprite>().ToList();
        Debug.Log("Sprites are Loaded");
    }

    private Sprite GetMapSprite(string spriteFileName)
    {
        return _mapSprites.Find(x => x.name.Equals(spriteFileName, StringComparison.OrdinalIgnoreCase));
    }
}

