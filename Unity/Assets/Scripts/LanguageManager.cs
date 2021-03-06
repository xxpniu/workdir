﻿using Assets.Scripts.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using UnityEngine;

namespace Assets.Scripts
{
    public class LanguageManager : Tools.XSingleton<LanguageManager>
    {
        public const string _LANGUAGE_ = "/language.xml";
        private Dictionary<string, string> values;
        public string this[string key]
        {
            get
            {
                if (values == null) InitLanauage();
                string value;
                if (values.TryGetValue(key, out value))
                    return ReplaceEc(value);
                return key;
            }
        }
        public void InitLanauage()
        {
            var file = App.GameAppliaction.ReadStreamingFile(_LANGUAGE_);
			var list = new List<LanguageKey> ();
            list = XmlParser.DeSerialize<List<LanguageKey>>(file);
            values = new Dictionary<string, string>();
			GameDebug.Log ("Loaded:"+list.Count);
			foreach(var i in list)
            {
                if (values.ContainsKey(i.Key))
                {
                    Debug.LogError(string.Format("Language key:{0} is exists!",i.Key));
                    continue;
                }
                values.Add(i.Key, i.Value);
            }
        }

        public static string ReplaceEc(string str)
        {
            return str.Replace("\\n", "\n");
        }
    }

    public class LanguageKey
    {
		[XmlAttribute]
        public string Key { set; get; }

        [XmlText]
        public string Value { set; get; }
    }
}
