using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Map;
using System.Linq;
using System;

public class MapController : MonoBehaviour
{
    [Header("Spawn Settings")]

    [SerializeField]
    private Spawner _spawner;

    [SerializeField]
    private GameObject _itemPrefab;

    private List<MapItem> _mapItemList;
    private List<Sprite> _mapSprites;
    private MapItem _mapItemData;

    private Dictionary<int, Sprite> _mapImages;

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        _mapItemList = new List<MapItem>();
        _mapImages = new Dictionary<int, Sprite>();
    }

    public void SetItems(MapData mapData)
    {
        LoadSpriteResorces();
        ItemInitialization(mapData);

        LoadMap();
    }

    public void ItemInitialization(MapData mapData)
    {
        InitializeImageDictionary(mapData.ImageDatas);
        InitializeItemList(mapData.SpriteNodes);
    }

    public void InitializeImageDictionary(List<ItemImage> imageData)
    {
        foreach(ItemImage data in imageData)
        {
            var nameWithoutExtension = RemoveExtensionFileFromName(data.Name);
            data.Name = nameWithoutExtension;

            var newimage = GetSprite(data.Name);

            if(newimage != null)
                _mapImages.Add(data.ImageID, newimage);
        }
    }

    private void InitializeItemList(List<ItemData> mapData)
    {
        foreach(ItemData data in mapData)
        {
            var item = CreateItem(data);
            
            if(item != null)
                _mapItemList.Add(item);
        }
    }

    private MapItem CreateItem(ItemData data)
    {
        var itemSprite = _mapImages[data.ImageID];

        if(itemSprite != null)
        {
            _mapItemData = new MapItem(data, itemSprite);
            return _mapItemData;
        }
    
        return null;
    }

    public void LoadMap()
    {
        if (_mapItemList.Count > 0)
            _spawner.SpawnItems(_mapItemList, _itemPrefab);
    }

    public void LoadSpriteResorces()
    {
        _mapSprites = Resources.LoadAll("Props", typeof(Sprite)).Cast<Sprite>().ToList();
        //Debug.Log("Sprites are Loaded");
    }

    private Sprite GetSprite(string spriteName)
    {
        var sprite = _mapSprites.Find(x => x.name.Equals(spriteName, StringComparison.OrdinalIgnoreCase));
        return sprite != null ? sprite : null;
    }

    private string RemoveExtensionFileFromName(string name)
    {
        string[] subs = name.Split('.');
        return subs[0];
    }
}

