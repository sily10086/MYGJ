using System.IO;
using cfg;
using SimpleJSON;
using UnityEngine;

namespace GameScript.Luban
{
    public static class LuBanData
    {
        private static string gameConfDir { get; } = Application.dataPath + "/LubanData/Data";
        public static Tables tables { get;  } 
            = new cfg.Tables(file => JSON.Parse(File.ReadAllText($"{gameConfDir}/{file}.json")));
    }
}