﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Combat.Simulate
{
    public abstract class GState
    {
        public GState() { _elements = new Dictionary<int, GObject>(); }
        public GPerception Perception { protected set; get; }

        public abstract void OnEnter();

        public abstract void OnExit();

        public virtual void OnTick()
        {
            while (_addTemp.Count > 0)
            {

                var item = _addTemp.Dequeue();
                if (_elements.ContainsKey(item.Index))
                {
                    Debug.LogWarning("INDEX:"+item.Index+" HAD IN DICT!");
                    continue;
                }
                item.Enable = true;
                item.OnJoinState();
                _elements.Add(item.Index, item);
            }
            foreach(var i in _elements)
            {
                if (!i.Value.Enable)
                {
                    //RemoveElement(i.Value);
                    _delTemp.Enqueue(i.Value);
                    continue;
                }

                i.Value.Controllor.GetAction(i.Value).DoAction();
            }

            while (_delTemp.Count > 0) 
            {
                var item = _delTemp.Dequeue();
                item.OnDestory();
                _elements.Remove(item.Index);
            }
        }

        private Dictionary<int, GObject> _elements;

        public T FindObjectByIndex<T>(int id) where T : GObject 
        {
            GObject item;
            if (_elements.TryGetValue(id, out item))
            {
                return item as T;
            }
            return null;
        }
        public GObject this[int index] { get { return FindObjectByIndex<GObject>(index); } }

        public delegate bool FindCondtion<T>(T el) where T : GObject;

        public void Each<T>(FindCondtion<T> cond) where T : GObject 
        {
            foreach (var i in _elements) 
            {
                var el = i.Value as T;
                if (el == null) continue;
                if (cond(el)) break;
            }
        }
        private Queue<GObject> _addTemp = new Queue<GObject>();
        private Queue<GObject> _delTemp = new Queue<GObject>();
        public void AddElement<T>(T el) where T : GObject {
            _addTemp.Enqueue(el);
        }
        public void RemoveElement<T>(T el) where T : GObject 
        {
            el.Enable = false;
        }
    }
}
