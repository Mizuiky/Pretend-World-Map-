using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Map
{
    [Serializable]
    public class MapItemData
    {
        public int id;
        public float posX;
        public float porY;
        public int imageId;
    }

    [Serializable]
    public class MapImageData
    {
        public int imageId;
        public string name;
    }
}


