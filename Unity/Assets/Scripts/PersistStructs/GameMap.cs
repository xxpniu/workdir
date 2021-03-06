﻿using UnityEngine;
using System.Collections;
using Assets.Scripts.PersistStructs;
using System.Collections.Generic;
using Assets.Scripts.DataManagers;
using Assets.Scripts.Tools;

[ExecuteInEditMode]
[RequireComponent(typeof(BoxCollider))]
public class GameMap : MonoBehaviour {

	public delegate bool EachWithBreak<T>(T item) where T: Component;

	// Use this for initialization
    void Start()
    {
		if (MapCamera != null) {
			MapCamera.clearFlags = CameraClearFlags.Color;
			MapCamera.backgroundColor = Color.black;
			MapCamera.orthographic = true;
			var listener = MapCamera.GetComponent<AudioListener> ();
			if (listener != null) {
				Destroy (listener);
			}
		}
    }
	
	// Update is called once per frame
    void Update()
    {
        if (MapCamera == null) return;
        MapCamera.transform.position = Vector3.Lerp(MapCamera.transform.position, TargetPos, Time.deltaTime * 4);
        MapCamera.orthographicSize = Mathf.Lerp(MapCamera.orthographicSize, targetZone, Time.deltaTime * 4);

    }

    public float LookAt(Vector2 grid, bool nodelay = false)
    {
		gridGrid = grid;
        var center = (Vector3)grid * OneGridSize
            + new Vector3(0, 0, -20);
        TargetPos = center;
        if (nodelay)
        {
            MapCamera.transform.position = TargetPos;
            return 0;
        }
        return 0.3f;
    }

	private Vector2 gridGrid;

	public Vector2 GetCurrent()
	{
		return  gridGrid - Orgin;
	}

    public void SetTarget(float zone)
    {
        targetZone = zone;
    }

    public Vector3 TargetPos;
    public float targetZone = 4;

    public Map CurrentMap;

    [SerializeField]
    public Camera MapCamera;

    public void SetZone(float zone,bool noDelay = false)
    {
        targetZone = zone;
        if(noDelay)
        {
            MapCamera.orthographicSize = targetZone;
        }
    }




    public bool IsOrgin(Vector2 pos) 
    {
        if (AllPosition == null) return false;
        int index = GamePlayerManager.PosXYToIndex((int)pos.x, (int)pos.y);
        MapPosition posData;
        if (AllPosition.TryGetValue(index, out posData))
        {
            return posData.DataType == Proto.MapEventType.BronPos;
        }
        return false;
    }

    [SerializeField]
    public float OneGridSize = 1f;

    private Dictionary<int, MapPosition> AllPosition;

    public void InitForExploreState()
    {
        var pos = GameObject.FindObjectsOfType<MapPosition>();
        AllPosition = new Dictionary<int, MapPosition>();
        foreach (var i in pos)
        {
            if (AllPosition.ContainsKey(i.ToIndex())) continue;
            AllPosition.Add(i.ToIndex(), i);

			if (i.DataType == Proto.MapEventType.None || i.DataType == Proto.MapEventType.RandomEvnetPos)
            {
                var renderer = i.GetComponent<SpriteRenderer>();
                if (renderer != null)
                    GameObject.Destroy(renderer);
            }

            if (i.DataType == Proto.MapEventType.BronPos)
                Orgin = new Vector2(i.X, i.Y);
			
        }
		CreateBound ();
    }

	private void CreateBound()
	{
		int size = 24;
		var rect = new Rect (0, 0, this.CurrentMap.Width, this.CurrentMap.Height);
		int x = -size;
		int y = -size;
		var spArr= new string[]{"mask","mask"};
		var bound = new GameObject ("Bound");
		bound.transform.parent = this.transform;
		bound.transform.localPosition = Vector3.zero;
		bound.transform.localScale = Vector3.one;
		int yMax = this.CurrentMap.Height + size;
		int xMax = this.CurrentMap.Width + size;
		for (var _x = x; _x < xMax; _x++) {
			for (var _y = y; _y < yMax; _y++) {
				if (rect.Contains (new Vector2 (_x, _y))) {
					continue;
				}
				var temp = new GameObject(string.Format("Bound_{0}_{1}",_x,_y));
				temp.transform.parent = bound.transform;
				temp.transform.localPosition = new Vector3 (_x * OneGridSize, _y * OneGridSize, 0);
				var sp = temp.AddComponent<SpriteRenderer> ();
				//bound

				sp.sprite = ResourcesManager.Singleton.LoadResources<Sprite> (GRandomer.RandomArray (spArr));;
				sp.sortingOrder = 3;
			}
		}
	}

	public void EachAllPosition<T>(EachWithBreak<T> cond) where T:Component
	{
		foreach (var i in AllPosition) {
			var item = i.Value as T;
			if (item == null)
				continue;
			if (cond (item))
				break;
		}
	}


    public Vector2 Orgin { get; private set; }

    public Vector3 GetPositionOfGrid(Vector2 grid)
    {
        var pos = new Vector3(grid.x * OneGridSize, grid.y * OneGridSize, 0);
        return pos;
    }

    internal bool HaveIndex(Vector2 target)
    {
        //int index = GamePlayerManager.PosXYToIndex((int)target.x, (int)target.y);
		return AllPosition.ContainsKey(target.ToIndex());
    }

#if UNITY_EDITOR

    public void SetWH(int w, int h, float gridSize)
    {
        CurrentMap = new Map();
        CurrentMap.Height = w;
        CurrentMap.Width = h;
        OneGridSize = gridSize;
        var box = this.GetComponent<BoxCollider>();
        box.size = new Vector3(w * gridSize, h * gridSize, 0f);
        offset = new Vector3((w * gridSize / 2f), (h * gridSize / 2f), 0) - new Vector3(0.5f * gridSize, 0.5f * gridSize, 0f);
        box.center = offset;
        
    }
    [HideInInspector]
    public Vector2 CurrentGrid =Vector2.zero;
    [HideInInspector]
    public Vector3 offset = Vector3.zero;

    public void OnDrawGizmos()
    {
        if (!ShowGrid) return;
        if (CurrentMap == null) return;
        for (var x = 0; x < this.CurrentMap.Width; x++)
        {
            for (var z = 0; z < this.CurrentMap.Height; z++)
            {
                var pos = new Vector3(x * OneGridSize, z * OneGridSize, 0);
                Gizmos.color = Color.white;
                Gizmos.DrawWireCube(pos, new Vector3(1f * OneGridSize, 1f * OneGridSize, 0));
            }
        }
    }
    [HideInInspector]
    public bool ShowGrid = false;

#endif




}
