﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.PersistStructs
{
    public class Map:ScriptableObject
    {
        public int MapIndex;
        public int Height;
        public int Width;

        public List<MapGrid> Grids;
    }

    [System.Serializable]
    public class MapGrid
    {
        public int PosX;
        public int PosY;
        public int ConfigID;
    }
}
